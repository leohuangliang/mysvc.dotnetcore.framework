# xUnit v3 批量升级工具使用说明

## 📋 概述

这个 PowerShell 脚本可以帮助你自动将现有的 xUnit v2 测试项目升级到 xUnit v3，解决测试发现和兼容性问题。

## 🚀 快速开始

### 1. 预览模式（推荐首次使用）
```powershell
# 查看将要进行的更改，不实际修改文件
.\scripts\upgrade-xunit-v3.ps1 -DryRun
```

### 2. 交互式升级
```powershell
# 显示所有测试项目，让你选择要升级的项目
.\scripts\upgrade-xunit-v3.ps1
```

### 3. 升级所有项目
```powershell
# 自动升级所有找到的测试项目
.\scripts\upgrade-xunit-v3.ps1 -All
```

### 4. 升级指定项目
```powershell
# 升级特定的项目文件
.\scripts\upgrade-xunit-v3.ps1 -ProjectPath "test\Domain.Core.Tests\Domain.Core.Tests.csproj"
```

## 🔧 脚本功能

### 自动检测和升级
- ✅ 扫描 `test` 目录下的所有测试项目
- ✅ 识别 xUnit v2 和 v3 项目状态
- ✅ 自动跳过已升级的项目
- ✅ 彩色输出，清晰显示进度

### 项目文件更新
- ✅ 添加 `<OutputType>Exe</OutputType>`
- ✅ 移除旧的包引用（xunit, Microsoft.NET.Test.Sdk）
- ✅ 添加新的包引用（xunit.v3, xunit.runner.visualstudio v3）
- ✅ 添加 `xunit.runner.json` 配置引用
- ✅ 添加 `<Using Include="Xunit" />` 指令

### 配置文件创建
- ✅ 自动创建 `xunit.runner.json` 配置文件
- ✅ 包含最佳实践配置（并行执行、性能优化）

## 📊 升级前后对比

### 升级前的项目文件
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>
  
  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.4.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.5" />
  </ItemGroup>
</Project>
```

### 升级后的项目文件
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <IsTestProject>true</IsTestProject>
    <OutputType>Exe</OutputType>
  </PropertyGroup>
  
  <ItemGroup>
    <Content Include="xunit.runner.json" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>
  
  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>
  
  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.0" />
    <PackageReference Include="xunit.v3" Version="2.0.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.1" />
  </ItemGroup>
</Project>
```

## 🛡️ 安全特性

### DryRun 模式
- 🔍 预览所有将要进行的更改
- 🚫 不修改任何文件
- 📋 显示详细的操作清单

### 智能检测
- 🔄 自动跳过已升级的项目
- ⚠️ 检测并报告潜在问题
- 📊 提供升级统计信息

## 📝 使用示例

### 示例 1：首次使用（推荐流程）
```powershell
# 1. 预览模式查看将要升级的项目
PS> .\scripts\upgrade-xunit-v3.ps1 -DryRun
=== xUnit v3 批量升级工具 ===
运行在 DryRun 模式，不会实际修改文件
找到以下测试项目:
1. [v2] Domain.Core.Tests.csproj - Domain.Core.Tests
2. [v3] TestProject1.csproj - TestProject1
3. [v2] Infrastructure.Data.MongoDB.Tests.csproj - Infrastructure.Data.MongoDB.Tests

# 2. 选择性升级
PS> .\scripts\upgrade-xunit-v3.ps1
请选择要升级的项目 (输入数字，多个用逗号分隔，或 'all' 升级全部): 1,3

# 3. 验证升级结果
PS> dotnet restore
PS> dotnet build
```

### 示例 2：批量升级所有项目
```powershell
PS> .\scripts\upgrade-xunit-v3.ps1 -All
=== xUnit v3 批量升级工具 ===
找到 8 个测试项目
准备升级 8 个项目...
正在升级项目: D:\Code\test\Domain.Core.Tests\Domain.Core.Tests.csproj
项目升级完成: D:\Code\test\Domain.Core.Tests\Domain.Core.Tests.csproj
...
=== 升级完成 ===
成功: 7 个项目
跳过: 1 个项目（已是v3）
```

## 🔧 升级后验证步骤

### 1. 恢复包引用
```powershell
dotnet restore
```

### 2. 编译验证
```powershell
dotnet build
```

### 3. 测试运行验证
```powershell
# 进入测试项目目录
cd test\Domain.Core.Tests

# 直接运行测试（xUnit v3 特性）
dotnet run

# 传统方式运行测试
dotnet test
```

### 4. VS Code 集成验证
- 打开 VS Code
- 查看 Testing 面板
- 确认测试项目被正确发现
- 运行测试验证功能

## ⚠️ 注意事项

### 兼容性检查
- ✅ 确保项目使用 .NET 8.0 或更高版本
- ✅ 检查自定义测试属性的兼容性
- ✅ 验证测试数据源（Theory）正常工作

### 备份建议
- 💾 升级前建议提交当前更改到版本控制
- 🔄 可以使用 `git stash` 临时保存更改
- 📋 保留升级日志以便问题排查

### 常见问题
1. **编译错误**：检查包版本冲突，确保移除了旧的包引用
2. **测试未发现**：验证 `OutputType=Exe` 和 `xunit.runner.json` 配置
3. **运行失败**：检查 .NET SDK 版本兼容性

## 🔗 相关资源

- [xUnit v3 官方文档](https://xunit.net/docs/getting-started/v3/cmdline)
- [xUnit v3 迁移指南](https://xunit.net/docs/getting-started/v3/migration)
- [项目测试问题解决总结](../README-Testing.md)

## 📞 支持

如果在使用过程中遇到问题：
1. 查看脚本输出的错误信息
2. 检查项目文件的语法正确性
3. 参考 `README-Testing.md` 中的故障排除指南
4. 使用 `-DryRun` 模式预览更改 