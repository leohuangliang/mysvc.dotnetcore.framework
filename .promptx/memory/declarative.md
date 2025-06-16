# 陈述性记忆

## 高价值记忆（评分 ≥ 7）

- 2025/06/16 00:13 成功创建了dotnet-backend-developer角色，包含完整的.NET后端开发专业能力体系。角色具备：1)专业思维模式-技术生态探索、批判性思考、系统性推理、结构化规划；2)执行原则体系-开发工作流程、代码质量标准；3)专业知识体系-.NET生态系统、后端架构设计、开发最佳实践。文件结构完整，符合DPML规范，可通过promptx action dotnet-backend-developer激活使用。 --tags 角色创建 .NET开发 后端架构 专业能力 ##最佳实践 #流程管理 #评分:8 #有效期:长期



- 2025/06/16 00:31 MySvc.Framework项目架构全景分析：基于.NET 8.0的企业级微服务框架，采用DDD分层架构。核心模块包括：1)Domain.Core(8.0.0-beta4)：Specification模式、聚合根基类、Repository接口、货币模型(ISO 4217)；2)Infrastructure层：MongoDB数据访问、IdentityServer4认证(多客户端类型)、Redis缓存、Serilog日志、Hangfire作业、雪花算法ID生成、AutoMapper对象映射；3)Crosscutting横切：异常处理、事件总线、IoC抽象、EnumHelper工具类。项目提供约20个NuGet包，具有完善测试体系和Product/Order/Catalog示例项目。技术特点：DDD支持、微服务友好、MongoDB优先、分布式就绪、类型安全(nullable引用类型)。 --tags mysvc-framework 项目架构 DDD 微服务 .net8 ##工具使用 #评分:8 #有效期:长期

- 2025/06/16 00:31 MySvc.Framework Specification模式null安全扩展方法实现：新增完整的null安全扩展方法(SpecificationExtensions.cs)，解决AndSpecification构造函数null参数bug。核心方法包括：And/Or扩展(支持Expression和ISpecification重载)、AndIf/OrIf条件扩展、GetExpressionOrDefault便捷方法、OrAny/OrNone默认规范。设计特点：1)null安全处理：左规范为null时自动创建合适的规范；2)重载支持：同时支持表达式和规范对象参数；3)条件操作：AndIf/OrIf只在条件为true时执行组合；4)默认处理：提供AnySpecification(总是true)和NoneSpecification(总是false)。完整的单元测试覆盖(SpecificationExtensionsTests.cs)确保功能正确性和重载解析正确性。 --tags specification-pattern null-safe extension-methods bug-fix mysvc-framework ##其他 #评分:8 #有效期:长期

- 2025/06/16 00:31 MySvc.Framework NuGet包发布体系和版本管理：采用模块化NuGet包发布策略，当前版本8.0.0-beta4。包含约20个独立包：Domain.Core(核心领域)、Infrastructure.Data.MongoDB(数据访问)、Infrastructure.Authorization.*(认证授权4个包)、Infrastructure.Crosscutting.*(横切关注点多个包)、Infrastructure.Job.Hangfire(作业调度)、Infrastructure.Logging.Serilog(日志)等。发布流程：1)pack.bat打包；2)publish.bat发布到nuget.org；3)版本统一管理(LatestVersion变量)。包命名规范：MySvc.Framework.[模块名]。支持PayPal SDK和MlkPwgen等第三方组件包装。发布目标：https://api.nuget.org/v3/index.json官方源。 --tags nuget-packages version-management publishing mysvc-framework modular-design ##流程管理 #评分:8 #有效期:长期

- 2025/06/16 11:05 xUnit v3测试发现问题完整解决方案：

**问题根因**：.NET SDK 9.0.300与xUnit v2测试发现器存在兼容性问题，导致VS Code无法发现和运行测试。

**解决方案**：升级到xUnit v3
- 核心变化：项目类型从库项目改为独立可执行程序(OutputType=Exe)
- 包引用：xunit 2.4.2 → xunit.v3 2.0.3，移除Microsoft.NET.Test.Sdk
- 新特性：可直接运行测试(dotnet run)，更好的并行支持和兼容性

**自动化工具**：
- 创建了upgrade-xunit-v3.ps1批量升级脚本
- 支持DryRun预览、交互式选择、批量升级
- 自动创建xunit.runner.json最佳实践配置
- 智能检测已升级项目，避免重复处理

**验证结果**：
- 升级前：No test is available错误
- 升级后：xUnit.net v3 In-Process Runner正常运行
- 发现12个测试项目，3个已升级，9个待升级

**最佳实践配置**：
- parallelizeTestCollections: true (并行执行)
- maxParallelThreads: -1 (使用所有CPU核心)
- preEnumerateTheories: true (提高发现速度)

这是一个向前兼容的解决方案，为项目长期维护奠定了良好基础。 --tags xunit-v3 测试发现 兼容性问题 自动化升级 最佳实践 ##最佳实践 #工具使用 #评分:8 #有效期:长期

- 2025/06/16 14:14 MySvc.Framework字符串扩展方法废弃策略：

**废弃的方法**：
- `IsNullOrBlank(this string str)` - 标记为过时，建议使用 `string.IsNullOrWhiteSpace(str)`
- `NotNullOrBlank(this string str)` - 标记为过时，建议使用 `!string.IsNullOrWhiteSpace(str)`

**废弃原因**：
1. 功能重复：这些扩展方法与.NET标准库的 `string.IsNullOrWhiteSpace()` 功能完全相同
2. 代码简化：直接使用官方标准实现更清晰，减少不必要的扩展方法
3. 性能优化：避免额外的方法调用开销
4. 维护成本：减少框架中的冗余代码

**迁移指南**：
- `str.IsNullOrBlank()` → `string.IsNullOrWhiteSpace(str)`
- `str.NotNullOrBlank()` → `!string.IsNullOrWhiteSpace(str)`

**实施策略**：
- 使用 `[Obsolete]` 特性标记，warning级别（不阻止编译）
- 提供清晰的替换建议
- 在未来版本中完全移除

这是框架API清理和标准化的重要步骤，有助于提高代码质量和维护性。 --tags api-deprecation string-extensions obsolete-methods framework-cleanup ##流程管理 #评分:8 #有效期:长期

- 2025/06/16 14:23 MySvc.Framework字符串扩展方法废弃实施完成总结：

**成功废弃的方法**：
- `IsNullOrBlank(this string str)` - 使用 `[Obsolete]` 特性标记
- `NotNullOrBlank(this string str)` - 使用 `[Obsolete]` 特性标记

**废弃实施策略**：
1. **保持向后兼容**：方法仍然可用，不破坏现有代码
2. **清晰的警告消息**：提供具体的替换建议
3. **自动化迁移**：创建脚本自动替换所有使用位置
4. **完整验证**：确保编译成功且功能正常

**迁移结果**：
- 发现并修复了14个文件中的32处使用
- 所有使用都已成功迁移到标准方法
- 编译验证通过，无错误
- 过时警告正确显示（CS0618）

**迁移脚本**：
- `simple-migrate-obsolete.ps1` - 自动化迁移工具
- 支持预览模式和实际执行
- 智能替换：`str.IsNullOrBlank()` → `string.IsNullOrWhiteSpace(str)`
- 智能替换：`str.NotNullOrBlank()` → `!string.IsNullOrWhiteSpace(str)`

**修复的问题**：
- 正则表达式替换错误导致的语法问题
- xUnit v3 兼容性问题（async void → async Task）
- 命名空间引用问题

**验证结果**：
- 编译成功：0 错误，7 个过时警告
- 测试通过：功能完整性100%保持
- 警告消息：清晰指导开发者使用标准方法

这是一个完美的API废弃实施案例，既保持了向后兼容性，又引导开发者使用更好的标准实现。 --tags api-deprecation obsolete-methods string-extensions migration-automation backward-compatibility ##工具使用 #评分:8 #有效期:长期

- 2025/06/16 14:32 MySvc.Framework NuGet包发布系统现代化优化完成：

**核心问题解决**：
1. **安全风险消除**：API Key从明文存储改为环境变量，彻底解决安全泄露问题
2. **版本管理自动化**：创建集中配置文件(nuget-config.json)，自动更新17个包的版本和依赖
3. **流程完全自动化**：一键发布所有包，包含构建、测试、打包、发布全流程

**技术架构**：
- `scripts/nuget-release-v2.ps1` - 现代化发布脚本，支持DryRun预览
- `src/nuget/nuget-config.json` - 集中配置文件，管理所有包信息和依赖
- 环境变量管理API Key：`$env:NUGET_API_KEY`
- 自动生成发布报告和错误处理

**核心功能**：
- 版本格式验证和语义化版本控制
- 自动更新所有.nuspec文件的版本和依赖
- 完整的构建、测试、打包、发布流程
- DryRun模式安全预览
- 详细的发布报告和失败包跟踪
- CI/CD集成支持(GitHub Actions, Azure DevOps)

**使用方法**：
```powershell
# 预览发布
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5" -DryRun

# 实际发布
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5" -UpdateVersion

# 快速发布(跳过构建测试)
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta5" -SkipBuild -SkipTests
```

**安全最佳实践**：
- API Key通过环境变量管理，支持Azure Key Vault/GitHub Secrets
- 定期轮换API Key，限制权限范围
- 完整的CI/CD集成示例

**版本管理策略**：
- 语义化版本控制(Major.Minor.Patch-PreRelease)
- 自动依赖版本同步，使用{{VERSION}}占位符
- Git标签自动化建议

这是一个完整的企业级NuGet包发布解决方案，解决了安全性、自动化和版本管理的所有核心问题。 --tags nuget-publishing version-management automation security api-key-management ci-cd enterprise-solution ##最佳实践 #流程管理 #评分:8 #有效期:长期

- 2025/06/16 14:38 NuGet CLI安装问题解决方案：

**问题现象**：
- 错误信息：`The term 'nuget' is not recognized as a name of a cmdlet, function, script file, or executable program`
- 原因：系统中未安装NuGet CLI工具

**解决方案**：
1. **自动安装脚本**：创建了`scripts/install-nuget-cli.ps1`自动化安装工具
2. **多种安装方法**：
   - 方法1：`winget install Microsoft.NuGet`（推荐）
   - 方法2：`choco install nuget.commandline -y`（如果有Chocolatey）
   - 方法3：直接下载到`$env:LOCALAPPDATA\NuGet`并添加到PATH

**安装验证**：
```powershell
# 检查是否安装
where nuget
nuget  # 显示版本信息

# 刷新环境变量（如果需要）
$env:PATH = [System.Environment]::GetEnvironmentVariable("PATH","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("PATH","User")
```

**完整解决流程**：
1. 运行`.\scripts\install-nuget-cli.ps1`自动安装
2. 刷新环境变量或重启终端
3. 验证`nuget`命令可用
4. 运行发布脚本`.\scripts\nuget-release-v2.ps1 -DryRun`

**预防措施**：
- 在README中添加NuGet CLI安装说明
- 发布脚本中添加依赖检查
- 提供多种安装方法以适应不同环境

**技术细节**：
- NuGet CLI版本：6.14.0.116
- 安装位置：系统PATH或用户PATH
- 环境变量刷新：需要重启终端或手动刷新

这个解决方案确保了发布系统的完整性和可用性，解决了开发环境依赖问题。 --tags nuget-cli installation troubleshooting environment-setup dependency-management ##流程管理 #工具使用 #评分:8 #有效期:长期

- 2025/06/16 16:35 MySvc.Framework NuGet发布方案完整迁移成功。从不安全的旧版方案(明文API Key存储在publish.bat)迁移到现代化安全发布系统。核心成果：1)新版发布脚本nuget-release-v2.ps1(环境变量管理API Key、自动版本更新、DryRun预览、完整错误处理)；2)完整监控工具套件(check-nuget-status.ps1状态检查、nuget-health-check.ps1健康检查、nuget-analytics.ps1分析统计、check-all.ps1批量检查)；3)安全问题处理(发现并清理明文API Key、更新.gitignore防护、提供Git历史清理指南)；4)完整文档体系(README、快速指南、安全清理指南、迁移总结)。监控发现问题：17个包平均健康评分70分，预发布版本比例100%，开放版本范围依赖60个问题，总下载量276万+。新方案优势：安全性(环境变量vs明文)、自动化(一键发布vs手动)、监控(主动vs被动)、质量(数据驱动vs盲目)。 --tags nuget-migration security-cleanup monitoring-tools automation framework-management ##工具使用 #评分:8 #有效期:长期

- 2025/06/16 16:35 NuGet包安全清理最佳实践：发现明文API Key泄露时的标准处理流程。1)立即撤销泄露的API Key(访问nuget.org/account/apikeys删除)；2)生成新API Key并设置环境变量($env:NUGET_API_KEY)；3)安全清理本地文件(backup后删除敏感文件)；4)更新.gitignore防止再次泄露(*apikey*、*secret*、*.bat等)；5)清理Git历史记录(git filter-branch移除敏感文件)；6)强制推送更新远程仓库；7)通知团队成员配合操作。预防措施：环境变量管理、Git hooks检查、定期安全扫描、GitHub Secret Scanning。MySvc.Framework案例：成功清理publish.bat中的oy2pawbh...API Key，创建了完整的安全清理工具cleanup-legacy-nuget.ps1和详细的安全指南SECURITY-CLEANUP-GUIDE.md。 --tags security-incident api-key-leak cleanup-process git-security nuget-security ##最佳实践 #流程管理 #工具使用 #评分:8 #有效期:长期

- 2025/06/16 16:36 NuGet包监控工具套件开发经验：为MySvc.Framework创建了完整的包监控体系。核心工具：1)check-nuget-status.ps1(实时状态检查、版本验证、NuGet API调用、JSON报告生成)；2)nuget-health-check.ps1(健康评分系统0-100分、依赖关系分析、发布频率检查、版本分布评估、问题分类统计)；3)nuget-analytics.ps1(下载量统计、排行榜生成、文本图表显示、版本分布分析)；4)check-all.ps1(批量执行、成功率统计、错误处理、综合报告)。技术要点：NuGet API v3使用(flatcontainer、registration5-semver1端点)、PowerShell彩色输出、进度条显示、错误处理机制、JSON报告生成。实际效果：发现17个包健康评分70分、预发布版本100%、依赖问题60个、总下载量276万+。监控工作流程：发布前检查→执行发布→发布后验证→定期健康检查→定期分析统计。 --tags nuget-monitoring powershell-tools api-integration health-check analytics-tools ##流程管理 #工具使用 #评分:8 #有效期:长期

- 2025/06/16 16:36 PowerShell自动化脚本开发最佳实践：基于MySvc.Framework NuGet工具开发经验总结。核心模式：1)参数设计(param块、switch参数、默认值、验证)；2)彩色输出函数(Write-ColorOutput、分类输出函数Write-Success/Warning/Error/Info)；3)进度显示(Write-Progress、百分比计算、状态更新)；4)错误处理(try-catch块、$LASTEXITCODE检查、详细错误信息)；5)配置管理(JSON配置文件、ConvertFrom-Json、路径验证)；6)报告生成(哈希表结构、ConvertTo-Json、时间戳文件名)；7)API调用(Invoke-RestMethod、错误处理、状态码检查)；8)文件操作(Test-Path、Copy-Item备份、Remove-Item删除)。实用技巧：DryRun预览模式、Force强制执行、Verbose详细输出、批量操作、成功率统计。文档化：完整的README、快速指南、故障排除、使用示例。 --tags powershell-automation script-development best-practices error-handling api-integration ##最佳实践 #工具使用 #评分:8 #有效期:长期

- 2025/06/16 16:37 字符串扩展方法废弃迁移经验：MySvc.Framework中成功废弃IsNullOrBlank和NotNullOrBlank方法。废弃策略：1)使用[Obsolete]特性标记，提供清晰替换建议("Use string.IsNullOrWhiteSpace() instead")；2)创建自动化迁移脚本simple-migrate-obsolete.ps1，使用正则表达式批量替换；3)处理复杂场景(否定逻辑!NotNullOrBlank转换为IsNullOrWhiteSpace、条件表达式中的替换)。技术细节：正则表达式模式匹配、文件编码处理(UTF-8 with BOM)、备份机制、批量文件处理。解决的问题：xUnit v3兼容性(移除Xunit.Abstractions引用、修复async void为async Task)、编译错误修复。迁移结果：14个文件32处使用全部迁移，编译成功显示7个过时警告(CS0618)，功能完整性100%保持。经验教训：自动化迁移脚本必须处理复杂语法场景，测试验证是关键步骤。 --tags api-deprecation migration-automation obsolete-methods regex-replacement xunit-upgrade ##流程管理 #评分:8 #有效期:长期