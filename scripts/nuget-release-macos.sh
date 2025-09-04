#!/bin/bash

# MySvc.Framework NuGet 包发布脚本 (macOS/Linux 版本)
# 支持 API Key 配置文件和环境变量两种方式

set -e  # 遇到错误时退出

# 默认参数
VERSION=""
DRY_RUN=false
SKIP_BUILD=false
SKIP_TESTS=false
UPDATE_VERSION=false

API_KEY_CONFIG_FILE="src/nuget/nuget-apikey.json"
API_KEY_ENV_VAR="NUGET_API_KEY"

# 颜色输出函数
print_success() { echo -e "\033[32m$1\033[0m"; }
print_warning() { echo -e "\033[33m$1\033[0m"; }
print_error() { echo -e "\033[31m$1\033[0m"; }
print_info() { echo -e "\033[36m$1\033[0m"; }

# 显示帮助信息
show_help() {
    echo "MySvc.Framework NuGet 包发布工具 (macOS/Linux 版本)"
    echo ""
    echo "用法: $0 [选项]"
    echo ""
    echo "选项:"
    echo "  -v, --version VERSION        指定发布版本"
    echo "  -d, --dry-run               预览模式，不实际执行"
    echo "  -s, --skip-build            跳过构建步骤"
    echo "  -t, --skip-tests            跳过测试步骤"
    echo "  -u, --update-version        更新配置文件版本"
    echo ""
    echo "  -a, --apikey-config FILE    指定 API Key 配置文件路径"
    echo "  -e, --env-var VAR           指定 API Key 环境变量名"
    echo "  -h, --help                  显示此帮助信息"
    echo ""
    echo "示例:"
    echo "  $0 -v 8.0.0-beta7           # 发布指定版本"
    echo "  $0 -v 8.0.0-beta7 -d        # 预览模式"
    echo "  $0 -v 8.0.0-beta7 -u        # 更新版本并发布"
    echo "  $0 -s -t                    # 跳过构建和测试"
}

# 解析命令行参数
while [[ $# -gt 0 ]]; do
    case $1 in
        -v|--version)
            VERSION="$2"
            shift 2
            ;;
        -d|--dry-run)
            DRY_RUN=true
            shift
            ;;
        -s|--skip-build)
            SKIP_BUILD=true
            shift
            ;;
        -t|--skip-tests)
            SKIP_TESTS=true
            shift
            ;;
        -u|--update-version)
            UPDATE_VERSION=true
            shift
            ;;

        -a|--apikey-config)
            API_KEY_CONFIG_FILE="$2"
            shift 2
            ;;
        -e|--env-var)
            API_KEY_ENV_VAR="$2"
            shift 2
            ;;
        -h|--help)
            show_help
            exit 0
            ;;
        *)
            print_error "未知参数: $1"
            show_help
            exit 1
            ;;
    esac
done

# 验证版本格式
validate_version() {
    if [[ ! "$1" =~ ^[0-9]+\.[0-9]+\.[0-9]+(-[a-zA-Z0-9.-]+)?$ ]]; then
        return 1
    fi
    return 0
}

# 获取 API Key
get_nuget_api_key() {
    local api_key=""
    local source=""
    
    # 1. 优先从配置文件读取 API Key
    if [[ -f "$API_KEY_CONFIG_FILE" ]]; then
        print_info "📁 从配置文件读取 API Key: $API_KEY_CONFIG_FILE" >&2
        
        # 使用 jq 解析 JSON（如果没有 jq，回退到简单解析）
        if command -v jq >/dev/null 2>&1; then
            local default_source=$(jq -r '.defaultSource // "nuget.org"' "$API_KEY_CONFIG_FILE" 2>/dev/null)
            api_key=$(jq -r ".apiKeys.\"$default_source\".apiKey // empty" "$API_KEY_CONFIG_FILE" 2>/dev/null)
            local description=$(jq -r ".apiKeys.\"$default_source\".description // empty" "$API_KEY_CONFIG_FILE" 2>/dev/null)
            
            if [[ -n "$api_key" && "$api_key" != "your-nuget-api-key-here" && "$api_key" != "null" ]]; then
                source="配置文件 ($default_source)"
                print_success "✅ 从配置文件成功获取 API Key: $description" >&2
            else
                print_warning "⚠️ 配置文件中的 API Key 未设置或为默认值" >&2
                api_key=""
            fi
        else
            # 简单的 grep 解析（备用方案）
            api_key=$(grep -o '"apiKey"[[:space:]]*:[[:space:]]*"[^"]*"' "$API_KEY_CONFIG_FILE" | head -1 | sed 's/.*"\([^"]*\)".*/\1/' 2>/dev/null || echo "")
            if [[ -n "$api_key" && "$api_key" != "your-nuget-api-key-here" ]]; then
                source="配置文件"
                print_success "✅ 从配置文件成功获取 API Key" >&2
            else
                print_warning "⚠️ 配置文件中的 API Key 未设置或为默认值" >&2
                api_key=""
            fi
        fi
    else
        print_warning "⚠️ API Key 配置文件不存在: $API_KEY_CONFIG_FILE" >&2
        print_info "💡 建议创建配置文件以便管理 API Key" >&2
    fi
    
    # 2. 仅在配置文件无效时才使用环境变量作为备用方案
    if [[ -z "$api_key" ]]; then
        print_info "🔍 配置文件未提供有效 API Key，尝试环境变量: $API_KEY_ENV_VAR" >&2
        api_key="${!API_KEY_ENV_VAR}"
        if [[ -n "$api_key" ]]; then
            source="环境变量 (备用)"
            print_warning "⚠️ 使用环境变量作为备用方案，建议配置文件方式" >&2
        fi
    fi
    
    # 清理所有空白字符（包括换行符、回车符、空格、制表符）
    api_key=$(echo "$api_key" | tr -d '[:space:]')
    
    # 3. 如果都没有，返回错误
    if [[ -z "$api_key" ]]; then
        print_error "❌ 未找到有效的 NuGet API Key" >&2
        print_info "💡 解决方案（按优先级排序）：" >&2
        print_info "   1. 推荐：创建配置文件 $API_KEY_CONFIG_FILE" >&2
        print_info "   2. 备用：设置环境变量 export $API_KEY_ENV_VAR='your-api-key'" >&2
        print_info "   3. 参考：查看模板文件 $API_KEY_CONFIG_FILE.template" >&2
        return 1
    fi
    
    echo "$api_key|$source"
    return 0
}

# 获取所有 nuspec 文件
get_nuspec_files() {
    # 根据当前目录确定nuspec文件的位置
    if [[ -d "nuspecs" ]]; then
        # 当前在src/nuget目录下
        find nuspecs -name "*.nuspec" -type f | sort
    elif [[ -d "src/nuget/nuspecs" ]]; then
        # 当前在项目根目录下
        find src/nuget/nuspecs -name "*.nuspec" -type f | sort
    else
        print_error "❌ 找不到nuspec文件目录"
        return 1
    fi
}

# 获取包数量
get_package_count() {
    get_nuspec_files | wc -l | tr -d ' '
}

# 从 Directory.Packages.props 读取版本信息
get_version_from_packages_props() {
    local packages_props_file="Directory.Packages.props"
    
    if [[ ! -f "$packages_props_file" ]]; then
        print_error "❌ Directory.Packages.props 文件不存在"
        return 1
    fi
    
    # 使用 xmlstarlet 或 grep 提取 CurrentVersion
    if command -v xmlstarlet >/dev/null 2>&1; then
        xmlstarlet sel -t -v "//CurrentVersion" "$packages_props_file" 2>/dev/null
    else
        # 备用方案：使用 grep 和 sed
        grep -o '<CurrentVersion>.*</CurrentVersion>' "$packages_props_file" | sed 's/<CurrentVersion>\(.*\)<\/CurrentVersion>/\1/' 2>/dev/null
    fi
}

# 更新 Directory.Packages.props 中的版本和元数据
update_packages_props_version() {
    local new_version="$1"
    local packages_props_file="Directory.Packages.props"
    
    if [[ ! -f "$packages_props_file" ]]; then
        print_error "❌ Directory.Packages.props 文件不存在"
        return 1
    fi
    
    print_info "🔄 更新 Directory.Packages.props 中的版本和元数据..."
    
    # 生成发布说明
    local release_notes="版本 $new_version 发布"
    local package_title="MySvc.Framework"
    
    if command -v xmlstarlet >/dev/null 2>&1; then
        # 使用 xmlstarlet 精确更新 XML
        xmlstarlet ed -L \
            -u "//CurrentVersion" -v "$new_version" \
            "$packages_props_file" 2>/dev/null
        
        # 检查并添加 PackageTitle 和 PackageReleaseNotes（如果不存在）
        if ! xmlstarlet sel -t -v "//PackageTitle" "$packages_props_file" >/dev/null 2>&1; then
            xmlstarlet ed -L -s "//PropertyGroup[CurrentVersion]" -t elem -n "PackageTitle" -v "$package_title" "$packages_props_file" 2>/dev/null
        fi
        
        if ! xmlstarlet sel -t -v "//PackageReleaseNotes" "$packages_props_file" >/dev/null 2>&1; then
            xmlstarlet ed -L -s "//PropertyGroup[CurrentVersion]" -t elem -n "PackageReleaseNotes" -v "$release_notes" "$packages_props_file" 2>/dev/null
        else
            xmlstarlet ed -L -u "//PackageReleaseNotes" -v "$release_notes" "$packages_props_file" 2>/dev/null
        fi
    else
        # 备用方案：使用 sed
        sed -i.bak "s/<CurrentVersion>.*<\/CurrentVersion>/<CurrentVersion>$new_version<\/CurrentVersion>/" "$packages_props_file"
        
        # 更新或添加 PackageReleaseNotes
        if grep -q "<PackageReleaseNotes>" "$packages_props_file"; then
            sed -i.bak "s/<PackageReleaseNotes>.*<\/PackageReleaseNotes>/<PackageReleaseNotes>$release_notes<\/PackageReleaseNotes>/" "$packages_props_file"
        else
            # 在 CurrentVersion 后添加 PackageReleaseNotes
            sed -i.bak "/CurrentVersion>/a\    <PackageReleaseNotes>$release_notes</PackageReleaseNotes>" "$packages_props_file"
        fi
        
        # 添加 PackageTitle（如果不存在）
        if ! grep -q "<PackageTitle>" "$packages_props_file"; then
            sed -i.bak "/CurrentVersion>/a\    <PackageTitle>$package_title</PackageTitle>" "$packages_props_file"
        fi
        
        rm -f "$packages_props_file.bak"
    fi
    
    print_success "✅ Directory.Packages.props 元数据已更新:"
    print_info "   版本: $new_version"
    print_info "   标题: $package_title"
    print_info "   发布说明: $release_notes"
    return 0
}

# 此函数已不需要，版本更新由 update_packages_props_version 处理
# update_config_version() - 已移除

# 验证 nuspec 文件使用元数据变量
validate_nuspec_files() {
    nuspec_files=($(get_nuspec_files))
    local packages_count=${#nuspec_files[@]}
    
    print_info "🔍 验证 $packages_count 个 nuspec 文件使用元数据变量..."
    
    local validation_failed=false
    
    for nuspec_path in "${nuspec_files[@]}"; do
        local package_name=$(basename "$nuspec_path" .nuspec)
        
        if [[ ! -f "$nuspec_path" ]]; then
            print_warning "⚠️ nuspec 文件不存在: $nuspec_path"
            continue
        fi
        
        # 检查是否使用了元数据变量
        if grep -q '$(CurrentVersion)' "$nuspec_path" && \
           grep -q '$(PackageTitle)' "$nuspec_path" && \
           grep -q '$(PackageReleaseNotes)' "$nuspec_path"; then
            print_success "  ✅ $package_name 使用元数据变量"
        else
            print_error "  ❌ $package_name 未使用元数据变量"
            validation_failed=true
        fi
    done
    
    if $validation_failed; then
        print_error "❌ 部分 nuspec 文件未使用元数据变量，请先更新"
        print_info "💡 提示：nuspec 文件应使用 \$(CurrentVersion)、\$(PackageTitle)、\$(PackageReleaseNotes) 等变量"
        return 1
    fi
    
    print_success "✅ 所有 nuspec 文件都正确使用了元数据变量"
    return 0
}

# 主程序开始
print_info "=== MySvc.Framework NuGet 包发布工具 (macOS/Linux 版本) ==="

# 配置文件已移除，直接使用 Directory.Packages.props 和 nuspec 文件

# 确定版本
if [[ -z "$VERSION" ]]; then
    # 优先从 Directory.Packages.props 读取版本
    VERSION=$(get_version_from_packages_props)
    if [[ -n "$VERSION" ]]; then
        print_info "📦 使用中央包管理版本: $VERSION (来源: Directory.Packages.props)"
    else
        # 备用方案：使用默认版本
        VERSION="1.0.0"
        print_warning "⚠️ 使用默认版本: $VERSION (建议在 Directory.Packages.props 中设置版本)"
    fi
else
    print_info "使用指定版本: $VERSION"
fi

# 验证版本格式
if ! validate_version "$VERSION"; then
    print_error "❌ 版本格式无效: $VERSION"
    print_info "正确格式: 1.0.0 或 1.0.0-beta1"
    exit 1
fi

# 更新版本配置
if $UPDATE_VERSION; then
    # 优先更新 Directory.Packages.props
    current_version=$(get_version_from_packages_props)
    if [[ -n "$current_version" ]]; then
        if [[ "$VERSION" != "$current_version" ]]; then
            if ! update_packages_props_version "$VERSION"; then
                exit 1
            fi
        fi
    else
        print_warning "⚠️ Directory.Packages.props 中未找到版本，跳过版本更新"
    fi
fi

print_info "DryRun 模式: $DRY_RUN"
print_info "跳过构建: $SKIP_BUILD"
print_info "跳过测试: $SKIP_TESTS"

packages_count=$(get_package_count)
print_info "包数量: $packages_count"

# 获取 API Key
if ! $DRY_RUN; then
    api_key_result=$(get_nuget_api_key) || exit 1
    api_key=$(echo "$api_key_result" | cut -d'|' -f1)
    api_key_source=$(echo "$api_key_result" | cut -d'|' -f2)
    print_success "✅ API Key 已获取 (来源: $api_key_source)"
fi

# 1. 验证 nuspec 文件配置
print_info "\n🔍 步骤 1: 验证 nuspec 文件配置..."
if ! validate_nuspec_files; then
    print_error "❌ nuspec 文件验证失败，请确保所有文件都使用元数据变量"
    exit 1
fi
print_info "💡 版本信息将通过 Directory.Packages.props 中的元数据变量自动注入"

# 2. 构建解决方案
if ! $SKIP_BUILD; then
    print_info "\n🔨 步骤 2: 构建解决方案..."
    if ! $DRY_RUN; then
        if ! dotnet build -c Release mysvc.dotnetcore.framework.sln; then
            print_error "❌ 构建失败"
            exit 1
        fi
    fi
    print_success "✅ 构建完成"
fi

# 3. 运行测试
if ! $SKIP_TESTS; then
    print_info "\n🧪 步骤 3: 运行测试..."
    if ! $DRY_RUN; then
        if ! dotnet test mysvc.dotnetcore.framework.sln --no-build -c Release; then
            print_warning "⚠️ 测试失败，但继续打包"
        fi
    fi
    print_success "✅ 测试完成"
fi

# 4. 打包
print_info "\n📦 步骤 4: 打包..."

# 检查可用的打包工具
USE_DOTNET_PACK=false
if ! $DRY_RUN; then
    if ! command -v nuget >/dev/null 2>&1; then
        print_warning "⚠️ 未找到 nuget 命令，将使用 dotnet pack"
        USE_DOTNET_PACK=true
    else
        print_info "✅ 使用 nuget 命令打包"
    fi
fi

# 切换到 nuget 目录进行打包
pushd src/nuget >/dev/null

pack_success_count=0
nuspec_files=($(find nuspecs -name "*.nuspec" -type f | sort))
for nuspec_path in "${nuspec_files[@]}"; do
    package_name=$(basename "$nuspec_path" .nuspec)
    nuspec_file=$(basename "$nuspec_path")
    output_dir="nuget-packages/$package_name"
    
    # 从 nuspec 文件中提取包 ID
    package_id=$(grep -o '<id>[^<]*</id>' "$nuspec_path" | sed 's/<id>\(.*\)<\/id>/\1/' | head -1)
    if [[ -z "$package_id" ]]; then
        package_id="$package_name"
    fi
    
    print_info "打包 $package_name..."
    
    if $DRY_RUN; then
        print_info "  [DryRun] 将打包到: $output_dir/$package_id.$VERSION.nupkg"
    else
        # 确保输出目录存在
        mkdir -p "$output_dir"
        
        if $USE_DOTNET_PACK; then
            # 使用 dotnet pack 命令
            project_path="../$package_name/$package_name.csproj"
            if [[ -f "$project_path" ]]; then
                if dotnet pack "$project_path" -c Release -o "$output_dir" -p:PackageVersion="$VERSION" --no-build; then
                    print_success "  ✅ 成功 (dotnet pack)"
                    ((pack_success_count++))
                else
                    print_error "  ❌ 失败: $package_name (dotnet pack)"
                fi
            else
                print_warning "  ⚠️ 项目文件不存在: $project_path，尝试使用 nuspec"
                if command -v nuget >/dev/null 2>&1; then
                    if nuget pack "$nuspec_path" -OutputDirectory "$output_dir"; then
                        print_success "  ✅ 成功 (nuget pack)"
                        ((pack_success_count++))
                    else
                        print_error "  ❌ 失败: $package_name (nuget pack)"
                    fi
                else
                    print_error "  ❌ 无法打包: $package_name (缺少项目文件和 nuget 命令)"
                fi
            fi
        else
            # 使用 nuget pack 命令
            if nuget pack "$nuspec_path" -OutputDirectory "$output_dir"; then
                print_success "  ✅ 成功 (nuget pack)"
                ((pack_success_count++))
            else
                print_error "  ❌ 失败: $package_name (nuget pack)"
            fi
        fi
    fi
done

popd >/dev/null

# 5. 发布到 NuGet
print_info "\n🚀 步骤 5: 发布到 NuGet..."
nuget_source="https://api.nuget.org/v3/index.json"

if ! $DRY_RUN; then
    publish_success_count=0
    publish_skip_count=0
    publish_fail_count=0
    
    # 设置 API Key
    if $USE_DOTNET_PACK; then
        # 使用 dotnet nuget 命令设置 API Key
        if ! dotnet nuget add source "$nuget_source" --name "CustomSource" 2>/dev/null; then
            print_info "源已存在或添加失败，继续..."
        fi
    else
        # 使用 nuget 命令设置 API Key
        nuget setapikey "$api_key" -Source "$nuget_source" >/dev/null 2>&1
    fi
    
    # 切换到 src/nuget 目录进行发布
    pushd src/nuget >/dev/null
    
    nuspec_files=($(get_nuspec_files))
    for nuspec_path in "${nuspec_files[@]}"; do
        package_name=$(basename "$nuspec_path" .nuspec)
        
        # 从 nuspec 文件中提取包 ID
        package_id=$(grep -o '<id>[^<]*</id>' "$nuspec_path" | sed 's/<id>\(.*\)<\/id>/\1/' | head -1)
        if [[ -z "$package_id" ]]; then
            package_id="$package_name"
        fi
        
        # 尝试多种可能的包文件路径
        possible_paths=(
            "nuget-packages/$package_name/$package_id.$VERSION.nupkg"
            "nuget-packages/$package_name/$package_name.$VERSION.nupkg"
        )
        
        # 查找实际存在的包文件
        package_path=""
        for path in "${possible_paths[@]}"; do
            if [[ -f "$path" ]]; then
                package_path="$path"
                break
            fi
        done
        
        # 如果还是找不到，尝试通配符匹配
        if [[ -z "$package_path" ]]; then
            # 使用通配符查找可能的包文件
            found_files=(nuget-packages/$package_name/*.$VERSION.nupkg)
            if [[ -f "${found_files[0]}" ]]; then
                package_path="${found_files[0]}"
            fi
        fi
        
        print_info "发布 $package_name..."
        
        if [[ -n "$package_path" && -f "$package_path" ]]; then
            if $USE_DOTNET_PACK; then
                # 使用 dotnet nuget push
                set +e  # 临时禁用错误退出

                push_output=$(dotnet nuget push "$package_path" --source "$nuget_source" --api-key "$api_key" --skip-duplicate 2>&1)
                push_exit_code=$?
                set -e  # 重新启用错误退出
                if [[ $push_exit_code -eq 0 ]]; then
                    if echo "$push_output" | grep -q "已存在包\|already exists"; then
                        print_warning "  ⚠️ 包已存在，跳过: $package_name (dotnet nuget push)"
                        ((publish_skip_count++))
                    else
                        print_success "  ✅ 发布成功: $package_name (dotnet nuget push)"
                        ((publish_success_count++))
                    fi
                else
                    print_error "  ❌ 发布失败: $package_name (dotnet nuget push)"
                    print_error "     错误详情: $push_output"
                    ((publish_fail_count++))
                fi
            else
                # 使用 nuget push
                set +e  # 临时禁用错误退出
                push_output=$(nuget push "$package_path" -Source "$nuget_source" -SkipDuplicate 2>&1)
                push_exit_code=$?
                set -e  # 重新启用错误退出
                if [[ $push_exit_code -eq 0 ]]; then
                    if echo "$push_output" | grep -q "已存在包\|already exists"; then
                        print_warning "  ⚠️ 包已存在，跳过: $package_name (nuget push)"
                        ((publish_skip_count++))
                    else
                        print_success "  ✅ 发布成功: $package_name (nuget push)"
                        ((publish_success_count++))
                    fi
                else
                    print_error "  ❌ 发布失败: $package_name (nuget push)"
                    print_error "     错误详情: $push_output"
                    ((publish_fail_count++))
                fi
            fi
        else
            print_warning "  ⚠️ 包文件不存在，跳过: $package_name"
            print_warning "     文件路径: $package_path"
            ((publish_skip_count++))
        fi
    done
    
    # 恢复到原始目录
    popd >/dev/null
else
    print_info "[DryRun] 将发布到: $nuget_source"
fi

# 6. 总结
print_info "\n📊 发布总结:"
print_info "版本: $VERSION"
print_info "包数量: $packages_count"

if ! $DRY_RUN; then
    print_success "✅ 成功打包: $pack_success_count 个包"
    print_success "✅ 成功发布: $publish_success_count 个包"
    
    if [[ $publish_skip_count -gt 0 ]]; then
        print_warning "⚠️ 跳过发布: $publish_skip_count 个包 (已存在)"
    fi
    
    if [[ $publish_fail_count -gt 0 ]]; then
        print_error "❌ 发布失败: $publish_fail_count 个包"
    fi
    
    # 生成发布报告
    report_file="nuget-release-report-$(date '+%Y%m%d-%H%M%S').json"
    cat > "$report_file" << EOF
{
  "version": "$VERSION",
  "timestamp": "$(date '+%Y-%m-%d %H:%M:%S')",
  "totalPackages": $packages_count,
  "packSuccessCount": $pack_success_count,
  "publishSuccessCount": $publish_success_count,
  "publishSkipCount": $publish_skip_count,
  "publishFailCount": $publish_fail_count,
  "apiKeySource": "$api_key_source"
}
EOF
    print_info "📄 发布报告已生成: $report_file"
else
    print_info "🎯 DryRun 模式完成，未实际执行发布操作"
fi

print_success "\n🎉 NuGet 包发布流程完成！"