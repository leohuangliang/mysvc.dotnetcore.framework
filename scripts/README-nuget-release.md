# MySvc.Framework NuGet 包发布工具

## 概述

这是一个现代化的 NuGet 包发布工具，解决了原有发布流程中的安全性、版本管理和自动化问题。

## 🚨 原有问题

### 安全风险
- ❌ API Key 明文存储在 `publish.bat` 中
- ❌ API Key 被提交到 Git 仓库
- ❌ 存在严重的安全泄露风险

### 版本管理问题
- ❌ 需要手动更新 20+ 个 `.nuspec` 文件
- ❌ 版本不一致风险
- ❌ 依赖版本需要手动同步

### 流程效率问题
- ❌ 需要手动执行多个批处理文件
- ❌ 缺乏错误处理和回滚机制
- ❌ 没有发布报告和状态跟踪

## ✅ 解决方案

### 1. 安全性改进
- ✅ 使用环境变量存储 API Key
- ✅ 支持 Azure Key Vault / GitHub Secrets
- ✅ API Key 不再出现在代码中

### 2. 版本管理优化
- ✅ 集中配置文件管理所有包信息
- ✅ 自动更新所有 `.nuspec` 文件
- ✅ 依赖版本自动同步

### 3. 流程自动化
- ✅ 一键发布所有包
- ✅ 完整的错误处理
- ✅ 详细的发布报告
- ✅ DryRun 模式预览

## 📁 文件结构

```
scripts/
├── nuget-release-v2.ps1          # 现代化发布脚本
└── README-nuget-release.md       # 使用说明

src/nuget/
├── nuget-config.json             # 集中配置文件
├── nuspecs/                      # nuspec 文件目录
│   ├── Domain.Core.nuspec
│   ├── Infrastructure.*.nuspec
│   └── ...
└── nuget-packages/               # 打包输出目录
```

## 🔧 安装和配置

### 0. 安装 NuGet CLI（如果需要）

如果遇到 `nuget 命令未找到` 的错误，请运行自动安装脚本：

```powershell
# 自动安装 NuGet CLI
.\scripts\install-nuget-cli.ps1

# 或者手动安装
winget install Microsoft.NuGet
```

### 1. 设置 API Key 环境变量

**Windows (PowerShell):**
```powershell
$env:NUGET_API_KEY = "your-nuget-api-key"
# 或者设置永久环境变量
[Environment]::SetEnvironmentVariable("NUGET_API_KEY", "your-api-key", "User")
```

**Linux/macOS:**
```bash
export NUGET_API_KEY="your-nuget-api-key"
# 添加到 ~/.bashrc 或 ~/.zshrc 使其永久生效
echo 'export NUGET_API_KEY="your-api-key"' >> ~/.bashrc
```

### 2. 验证配置
```powershell
# 检查环境变量
echo $env:NUGET_API_KEY

# 检查配置文件
Get-Content src\nuget\nuget-config.json | ConvertFrom-Json
```

## 🚀 使用方法

### 基本用法

```powershell
# 发布当前配置文件中的版本
.\scripts\nuget-release-v2.ps1

# 发布指定版本
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5"

# 预览模式（不实际执行）
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5" -DryRun

# 更新配置文件版本并发布
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5" -UpdateVersion
```

### 高级选项

```powershell
# 跳过构建和测试（快速发布）
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5" -SkipBuild -SkipTests

# 使用自定义配置文件
.\scripts\nuget-release-v2.ps1 -ConfigFile "custom-config.json"

# 使用自定义 API Key 环境变量
.\scripts\nuget-release-v2.ps1 -ApiKeyEnvVar "MY_NUGET_KEY"
```

### 完整发布流程

```powershell
# 1. 预览发布（推荐）
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5" -DryRun

# 2. 实际发布
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5" -UpdateVersion

# 3. 创建 Git 标签
git tag v8.0.0-beta5
git push origin v8.0.0-beta5
```

## 📊 发布报告

脚本会自动生成详细的发布报告：

```json
{
  "Version": "8.0.0-beta5",
  "Timestamp": "2024-01-15 14:30:25",
  "TotalPackages": 17,
  "SuccessCount": 17,
  "FailCount": 0,
  "FailedPackages": []
}
```

## 🔍 故障排除

### 常见问题

**1. API Key 未找到**
```
❌ 未找到 NuGet API Key 环境变量: NUGET_API_KEY
```
**解决方案：** 设置环境变量 `$env:NUGET_API_KEY = "your-api-key"`

**2. 配置文件格式错误**
```
❌ 配置文件格式错误: Invalid JSON
```
**解决方案：** 检查 `src\nuget\nuget-config.json` 的 JSON 格式

**3. 构建失败**
```
❌ 构建失败
```
**解决方案：** 检查项目编译错误，或使用 `-SkipBuild` 跳过构建

**4. 包已存在**
```
❌ 发布失败: Package already exists
```
**解决方案：** 更新版本号，NuGet 不允许覆盖已发布的版本

### 调试模式

```powershell
# 启用详细输出
$VerbosePreference = "Continue"
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5" -DryRun -Verbose
```

## 🔐 安全最佳实践

### 1. API Key 管理
- ✅ 使用环境变量存储 API Key
- ✅ 定期轮换 API Key
- ✅ 限制 API Key 权限范围
- ❌ 不要将 API Key 提交到代码仓库

### 2. CI/CD 集成
```yaml
# GitHub Actions 示例
- name: Set NuGet API Key
  env:
    NUGET_API_KEY: ${{ secrets.NUGET_API_KEY }}
  run: |
    .\scripts\nuget-release-v2.ps1 -Version "${{ github.ref_name }}"
```

### 3. Azure DevOps 集成
```yaml
# Azure Pipelines 示例
- task: PowerShell@2
  displayName: 'Publish NuGet Packages'
  env:
    NUGET_API_KEY: $(NuGetApiKey)
  inputs:
    filePath: 'scripts/nuget-release-v2.ps1'
    arguments: '-Version "$(Build.BuildNumber)"'
```

## 📈 版本管理策略

### 语义化版本控制
- **主版本号 (Major)**: 不兼容的 API 更改
- **次版本号 (Minor)**: 向后兼容的功能新增
- **修订号 (Patch)**: 向后兼容的问题修正
- **预发布标识**: alpha, beta, rc

### 示例版本号
```
8.0.0          # 正式版本
8.0.1          # 修复版本
8.1.0          # 功能版本
9.0.0          # 重大版本
8.0.0-beta1    # 测试版本
8.0.0-alpha1   # 内测版本
```

## 🎯 最佳实践

### 发布前检查清单
- [ ] 代码已提交并推送
- [ ] 所有测试通过
- [ ] 版本号符合语义化版本控制
- [ ] 更新日志已准备
- [ ] API Key 环境变量已设置

### 发布后操作
- [ ] 验证 NuGet.org 上的包状态
- [ ] 更新项目文档
- [ ] 创建 Git 标签
- [ ] 发布 Release Notes
- [ ] 通知团队成员

## 🔄 迁移指南

### 从旧版本迁移

1. **备份现有脚本**
   ```powershell
   Copy-Item src\nuget\pack.bat src\nuget\pack.bat.backup
   Copy-Item src\nuget\publish.bat src\nuget\publish.bat.backup
   ```

2. **设置环境变量**
   ```powershell
   $env:NUGET_API_KEY = "从 publish.bat 中复制的 API Key"
   ```

3. **测试新脚本**
   ```powershell
   .\scripts\nuget-release-v2.ps1 -DryRun
   ```

4. **执行首次发布**
   ```powershell
   .\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5"
   ```

## 📞 支持和反馈

如果遇到问题或有改进建议，请：
1. 检查本文档的故障排除部分
2. 查看发布报告中的错误信息
3. 联系开发团队获取支持

## 📊 NuGet 包状态监控工具

### 包状态检查工具

检查所有包在 NuGet.org 上的发布状态和版本信息：

```powershell
# 检查配置文件中指定版本的包状态
.\scripts\check-nuget-status.ps1

# 检查特定版本
.\scripts\check-nuget-status.ps1 -Version "8.0.0-beta5"

# 检查最新版本状态
.\scripts\check-nuget-status.ps1 -CheckLatest

# 显示详细信息
.\scripts\check-nuget-status.ps1 -ShowDetails

# 输出为 JSON 格式
.\scripts\check-nuget-status.ps1 -OutputFormat Json
```

**功能特性**：
- ✅ 实时检查包发布状态
- 📊 版本存在性验证
- 📋 详细状态报告
- 🔗 直接链接到 NuGet.org
- 📄 自动生成 JSON 报告

### 包分析工具

获取包的下载统计和使用分析：

```powershell
# 基本分析
.\scripts\nuget-analytics.ps1

# 显示文本图表
.\scripts\nuget-analytics.ps1 -OutputFormat Chart

# 分析 Top 10 版本
.\scripts\nuget-analytics.ps1 -TopVersions 10

# 输出为 CSV
.\scripts\nuget-analytics.ps1 -OutputFormat Csv
```

**分析内容**：
- 📈 下载量统计
- 🏆 包排行榜
- 📦 版本分布分析
- 💡 优化建议
- 📊 文本图表显示

### 包健康检查工具

全面检查包的健康状态：

```powershell
# 完整健康检查
.\scripts\nuget-health-check.ps1

# 检查依赖关系
.\scripts\nuget-health-check.ps1 -CheckDependencies

# 检查过时版本
.\scripts\nuget-health-check.ps1 -CheckOutdated

# JSON 格式输出
.\scripts\nuget-health-check.ps1 -OutputFormat Json
```

**检查项目**：
- 🏥 健康评分系统
- 📊 依赖关系分析
- ⏰ 发布频率检查
- 🔍 版本分布评估
- 💡 改进建议

### 监控工作流程

建议的包监控工作流程：

```powershell
# 1. 发布前检查
.\scripts\check-nuget-status.ps1 -Version "8.0.0-beta5"

# 2. 执行发布
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5" -UpdateVersion

# 3. 发布后验证
.\scripts\check-nuget-status.ps1 -Version "8.0.0-beta5"

# 4. 定期健康检查
.\scripts\nuget-health-check.ps1

# 5. 定期分析统计
.\scripts\nuget-analytics.ps1 -OutputFormat Chart
```

### 监控工具故障排除

**1. API 限制**
```
Too Many Requests (429)
```
**解决方案**：
- 等待几分钟后重试
- 减少并发请求
- 使用 -Verbose 参数查看详细信息

**2. 网络连接问题**
```
Unable to connect to NuGet API
```
**解决方案**：
- 检查网络连接
- 确认防火墙设置
- 尝试使用代理设置

**3. 包未找到**
```
Package not found (404)
```
**解决方案**：
- 确认包名拼写正确
- 检查包是否已发布
- 验证配置文件中的包名

---

**注意：** 请确保在生产环境中使用前充分测试新的发布流程。建议先在测试环境中验证所有功能。 