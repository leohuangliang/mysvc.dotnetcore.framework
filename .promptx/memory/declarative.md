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