# MySvc.Framework 测试指南

## 测试状态

### ✅ 支持的测试环境
- **Visual Studio 2022**: 完全支持，推荐使用
- **Visual Studio Code**: 通过C# Dev Kit扩展支持
- **JetBrains Rider**: 完全支持

### ⚠️ 已知限制
- **命令行 `dotnet test`**: 由于.NET SDK 8.0.404与xUnit测试发现器的兼容性问题，无法在命令行中发现测试

## 测试项目配置

当前测试项目使用经过验证的稳定配置：

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.6.0" />
<PackageReference Include="xunit" Version="2.6.6" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.4.5" />
<PackageReference Include="coverlet.collector" Version="6.0.0" />
```

## 使用建议

### 开发阶段
1. 使用 **Visual Studio 2022** 进行测试开发和调试
2. 在测试资源管理器中运行和调试测试
3. 使用Live Unit Testing功能进行实时测试反馈

### CI/CD集成
如果需要在CI/CD流水线中运行测试，有以下选项：

#### 选项1：临时移除global.json
```bash
# 备份global.json
mv global.json global.json.bak
# 运行测试
dotnet test
# 恢复global.json
mv global.json.bak global.json
```

#### 选项2：使用Visual Studio Build Tools
在CI环境中安装Visual Studio Build Tools，使用MSTest运行器。

#### 选项3：等待SDK更新
等待.NET SDK的后续版本修复此兼容性问题。

## 测试覆盖

### Domain.Core.Tests
- ✅ SpecificationExtensions 扩展方法测试
- ✅ ValueObject 基类测试
- ✅ 17个测试方法，覆盖null安全处理和重载解析

### 测试内容
1. **Specification模式扩展方法**
   - And/Or 重载测试
   - AndIf/OrIf 条件扩展测试
   - Null安全处理验证
   - 编译器重载解析测试

2. **ValueObject基类**
   - 相等性比较测试
   - GetHashCode测试

## 故障排除

### 如果Visual Studio中测试不显示
1. 重新构建解决方案
2. 清理并重新构建测试项目
3. 重启Visual Studio
4. 检查测试资源管理器设置

### 如果需要命令行测试
参考上述CI/CD集成选项，或考虑升级到更新的.NET版本。

## 版本兼容性

- **.NET Target Framework**: net8.0
- **Global SDK Version**: 8.0.404 (在global.json中指定)
- **测试框架**: xUnit 2.6.6
- **测试运行器**: Visual Studio 2022 (推荐)

---

*最后更新: 2025-01-16*
*状态: Visual Studio测试正常，命令行测试存在已知兼容性问题*

# 🚀 xUnit 测试发现问题解决方案总结

## 📋 问题描述
- **症状**：VS Code 无法发现和运行 xUnit 测试，Testing 视图显示空白
- **错误信息**：`No test is available. Make sure that test discoverer & executors are registered...`
- **影响范围**：整个解决方案中的9个测试项目都无法被发现

## 🔍 根本原因分析

### 初始诊断
1. **.NET SDK 兼容性问题**：SDK 9.0.300 与 xUnit v2 测试发现器存在兼容性问题
2. **已采取的修复措施**：
   - ✅ 降级到 .NET SDK 8.0.411
   - ✅ 调整包版本组合
   - ✅ 配置 VS Code 设置
   - ❌ 问题仍然存在

### 最终发现
通过获取 [xUnit v3 官方文档](https://xunit.net/docs/getting-started/v3/cmdline)，发现关键问题：
- **xUnit v2 在新环境中存在测试发现问题**
- **xUnit v3 提供了全新的架构和更好的兼容性**

## ✅ 成功解决方案：升级到 xUnit v3

### 核心变化

| 项目                     | xUnit v2 (问题版本)               | xUnit v3 (解决方案)                   |
|--------------------------|-----------------------------------|---------------------------------------|
| **包引用**               | `xunit` 2.4.2                     | `xunit.v3` 2.0.3                      |
| **Visual Studio Runner** | `xunit.runner.visualstudio` 2.4.5 | `xunit.runner.visualstudio` 3.1.1     |
| **Test SDK**             | `Microsoft.NET.Test.Sdk` 17.8.0   | ❌ 移除（避免冲突）                    |
| **项目类型**             | 库项目                            | **独立可执行程序** (`OutputType=Exe`) |
| **运行方式**             | 仅通过测试运行器                  | 🆕 **直接运行** + 测试运行器          |

### 升级后的项目配置

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
    <OutputType>Exe</OutputType>  <!-- 🆕 xUnit v3 特色 -->
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

### xUnit v3 最佳配置 (xunit.runner.json)

```json
{
  "methodDisplay": "method",
  "methodDisplayOptions": "all",
  "parallelizeTestCollections": true,
  "maxParallelThreads": -1,
  "preEnumerateTheories": true,
  "diagnosticMessages": false,
  "internalDiagnosticMessages": false
}
```

**配置说明**：
- `parallelizeTestCollections`: 启用测试集合并行执行
- `maxParallelThreads`: -1 表示使用所有可用CPU核心
- `preEnumerateTheories`: 预枚举理论测试，提高发现速度
- `diagnosticMessages`: 关闭诊断消息，减少输出噪音

## 📊 测试结果对比

### 升级前
```
❌ dotnet test --list-tests
No test is available. Make sure that test discoverer & executors are registered...
```

### 升级后
```
✅ dotnet run
xUnit.net v3 In-Process Runner v2.0.3+216a74a292 (64-bit .NET 8.0.17)
=== TEST EXECUTION SUMMARY ===
TestProject1  Total: 5, Errors: 0, Failed: 0, Skipped: 0, Not Run: 0, Time: 0.071s
```

## 🛠️ 实施步骤

### 1. 安装 xUnit v3 模板
```powershell
dotnet new install xunit.v3.templates
```

### 2. 升级现有测试项目
- 更新项目文件配置
- 升级包引用到 v3 版本
- 添加 xunit.runner.json 配置文件

### 3. 更新 VS Code 任务
```json
{
    "label": "test-xunit-v3-run",
    "command": "dotnet",
    "type": "process",
    "args": ["run"],
    "options": {
        "cwd": "${workspaceFolder}/test/TestProject1"
    },
    "group": "test"
}
```

## 🎯 xUnit v3 的优势

1. **🚀 独立可执行**：测试项目可以直接运行，无需额外工具
2. **🔧 更好兼容性**：原生支持 .NET 8+ 和最新工具链
3. **⚡ 更快速度**：改进的测试发现和执行性能
4. **🛡️ 更稳定**：解决了 v2 在新环境中的兼容性问题
5. **📊 更好的并行支持**：改进的并行测试执行机制
6. **🔍 增强的诊断**：更详细的测试运行信息和错误报告

## 📋 xUnit v3 迁移检查清单

### ✅ 项目文件更新
- [ ] 添加 `<OutputType>Exe</OutputType>`
- [ ] 更新包引用到 xUnit v3 版本
- [ ] 移除 `Microsoft.NET.Test.Sdk`（如果存在）
- [ ] 添加 `xunit.runner.json` 配置文件

### ✅ 代码兼容性检查
- [ ] 验证所有测试特性仍然有效
- [ ] 检查自定义测试属性的兼容性
- [ ] 确认测试数据源（Theory）正常工作
- [ ] 验证测试集合和并行执行设置

### ✅ 工具集成验证
- [ ] VS Code Testing 面板显示测试
- [ ] `dotnet test` 命令正常工作
- [ ] `dotnet run` 直接执行测试
- [ ] CI/CD 管道兼容性测试

### ✅ 性能和配置优化
- [ ] 配置并行执行参数
- [ ] 设置适当的诊断级别
- [ ] 优化测试发现性能
- [ ] 配置代码覆盖率收集

## 📝 当前状态

### ✅ 已完成
- `TestProject1` 成功升级到 xUnit v3
- 测试发现和运行功能正常
- VS Code 任务配置完成
- 最佳实践配置已应用

### 🔄 待处理
需要升级的其他测试项目：
- `Domain.Core.Tests`
- `Infrastructure.Authorization.Admin.Tests`
- `Infrastructure.Authorization.Client.Tests`
- `Infrastructure.Crosscutting.Tests`
- `Infrastructure.Data.MongoDB.Tests`
- `Infrastructure.Logging.Serilog.Tests`
- `MongodbTransaction.Tests`
- 其他测试项目

## 💡 推荐下一步行动

1. **批量升级脚本**：创建 PowerShell 脚本自动化升级过程
2. **VS Code 集成验证**：确认升级后的项目在 VS Code Testing 面板中正确显示
3. **CI/CD 更新**：更新构建管道以支持 xUnit v3 的新运行模式
4. **团队培训**：分享 xUnit v3 的新特性和最佳实践

## 🔧 故障排除指南

### 常见问题
1. **测试仍未发现**：检查 `OutputType=Exe` 是否正确设置
2. **包冲突**：确保移除了 `Microsoft.NET.Test.Sdk`
3. **配置不生效**：验证 `xunit.runner.json` 的 `CopyToOutputDirectory` 设置
4. **并行执行问题**：调整 `maxParallelThreads` 参数

### 调试命令
```powershell
# 检查测试发现
dotnet run --verbosity detailed

# 列出所有测试
dotnet test --list-tests

# 运行特定测试
dotnet run --filter "FullyQualifiedName~TestMethodName"
```

## 🔗 参考资源

- [xUnit v3 官方文档](https://xunit.net/docs/getting-started/v3/cmdline)
- [xUnit v3 迁移指南](https://xunit.net/docs/getting-started/v3/migration)
- [Microsoft Testing Platform 文档](https://xunit.net/docs/getting-started/v3/microsoft-testing-platform)
- [xUnit v3 配置参考](https://xunit.net/docs/configuration-files)

---

**总结**：通过升级到 xUnit v3，我们不仅解决了测试发现问题，还获得了更现代、更强大的测试框架。这是一个向前兼容的解决方案，为项目的长期维护奠定了良好基础。配合最佳实践配置，可以显著提升测试执行效率和开发体验。 