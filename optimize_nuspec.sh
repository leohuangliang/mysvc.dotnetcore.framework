#!/bin/bash

# 批量优化nuspec文件的脚本
# 将硬编码版本替换为变量引用

echo "开始批量优化nuspec文件..."

# 定义nuspec文件列表
files=(
    "src/nuget/nuspecs/Infrastructure.Authorization.Client.nuspec"
    "src/nuget/nuspecs/Infrastructure.Authorization.InternalClient.nuspec"
    "src/nuget/nuspecs/Infrastructure.Authorization.Merchant.nuspec"
    "src/nuget/nuspecs/Infrastructure.Crosscutting.Cache.StackExchangeRedis.nuspec"
    "src/nuget/nuspecs/Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.nuspec"
    "src/nuget/nuspecs/Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.Redis.nuspec"
    "src/nuget/nuspecs/Infrastructure.Crosscutting.Json.NewtonsoftJson.nuspec"
    "src/nuget/nuspecs/Infrastructure.IntegrationEventService.nuspec"
    "src/nuget/nuspecs/Infrastructure.Job.Hangfire.nuspec"
)

# 对每个文件进行优化
for file in "${files[@]}"; do
    if [ -f "$file" ]; then
        echo "优化文件: $file"
        
        # 备份原文件
        cp "$file" "$file.bak"
        
        # 替换版本号
        sed -i '' 's/8\.0\.0-beta[0-9]/$(CurrentVersion)/g' "$file"
        sed -i '' 's/version="8\.0\.0-beta[0-9]"/version="$(CurrentVersion)"/g' "$file"
        
        # 替换常见包版本
        sed -i '' 's/version="13\.0\.3"/version="$(NewtonsoftJsonVersion)"/g' "$file"
        sed -i '' 's/version="12\.4\.1"/version="$(MediatRVersion)"/g' "$file"
        sed -i '' 's/version="13\.0\.1"/version="$(AutoMapperVersion)"/g' "$file"
        sed -i '' 's/version="1\.8\.14"/version="$(HangfireCoreVersion)"/g' "$file"
        sed -i '' 's/version="4\.0\.2"/version="$(SerilogVersion)"/g' "$file"
        sed -i '' 's/version="4\.8\.0"/version="$(MailKitVersion)"/g' "$file"
        sed -i '' 's/version="8\.2\.5"/version="$(MassTransitVersion)"/g' "$file"
        sed -i '' 's/version="4\.1\.2"/version="$(IdentityServer4Version)"/g' "$file"
        
        # 更新XML命名空间
        sed -i '' 's|http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd|http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd|g' "$file"
        sed -i '' 's|http://schemas.microsoft.com/packaging/2013/01/nuspec.xsd|http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd|g' "$file"
        sed -i '' 's|http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd|http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd|g' "$file"
        
        echo "完成优化: $file"
    else
        echo "文件不存在: $file"
    fi
done

echo "批量优化完成！"