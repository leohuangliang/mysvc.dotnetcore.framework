# NuSpec 文件优化方案

## 发现的问题

### 1. 版本号不一致问题
- **MediatR**: nuspec中使用 `12.4.1`，Directory.Packages.props中定义 `12.5.0`
- **Newtonsoft.Json**: nuspec中使用 `13.0.1`，Directory.Packages.props中定义 `13.0.3`
- **AutoMapper**: nuspec中使用 `13.0.1`，Directory.Packages.props中定义 `13.0.1` (一致)
- **Hangfire.Core**: 已修复，使用变量 `$(HangfireCoreVersion)`

### 2. XML格式不一致
- 不同的nuspec文件使用不同的XML命名空间：
  - `http://schemas.microsoft.com/packaging/2013/01/nuspec.xsd`
  - `http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd`
  - `http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd`
  - 有些文件没有命名空间声明

### 3. 内部包版本不一致
- 大部分内部包依赖使用 `8.0.0-beta6`，但当前版本应该是 `8.0.0-beta7`

### 4. 格式化问题
- Infrastructure.Data.MongoDB.nuspec 格式化混乱，标签换行不规范

## 优化方案

### 1. 版本变量化
创建版本变量映射，在nuspec文件中使用变量替换硬编码版本：

```xml
<!-- 替换前 -->
<dependency id="MediatR" version="12.4.1" />
<dependency id="Newtonsoft.Json" version="13.0.1" />

<!-- 替换后 -->
<dependency id="MediatR" version="$(MediatRVersion)" />
<dependency id="Newtonsoft.Json" version="$(NewtonsoftJsonVersion)" />
```

### 2. 统一XML格式
使用最新的nuspec schema：
```xml
<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd">
```

### 3. 内部包版本统一
将所有内部包依赖版本更新为当前版本 `8.0.0-beta7`，并使用变量：
```xml
<dependency id="MySvc.Framework.Infrastructure.Crosscutting" version="$(CurrentVersion)" />
```

### 4. 需要更新的包版本映射

| 包名 | nuspec当前版本 | Directory.Packages.props版本 | 建议操作 |
|------|----------------|------------------------------|----------|
| MediatR | 12.4.1 | 12.5.0 | 使用$(MediatRVersion) |
| Newtonsoft.Json | 13.0.1 | 13.0.3 | 使用$(NewtonsoftJsonVersion) |
| AutoMapper | 13.0.1 | 13.0.1 | 使用$(AutoMapperVersion) |
| MongoDB.Driver | 2.29.0 | 2.29.0 | 使用$(MongoDBDriverVersion) |
| Serilog | 4.0.2 | 4.0.2 | 使用$(SerilogVersion) |
| MailKit | 4.8.0 | 4.8.0 | 使用$(MailKitVersion) |
| MassTransit | 8.2.5 | 8.2.5 | 使用$(MassTransitVersion) |
| IdentityServer4 | 4.1.2 | 4.1.2 | 使用$(IdentityServer4Version) |

### 5. 标准化模板

```xml
<?xml version="1.0" encoding="utf-8"?>
<package xmlns="http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd">
  <metadata>
    <id>MySvc.Framework.[PackageName]</id>
    <version>$(CurrentVersion)</version>
    <title>MySvc.Framework.[PackageName]</title>
    <authors>liang.huang</authors>
    <owners>liang.huang</owners>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <description>[Package Description]</description>
    <releaseNotes>版本 $(CurrentVersion) 发布</releaseNotes>
    <copyright>Copyright 2018 (c) MySVC.cc All rights reserved.</copyright>
    <tags>MySVC.cc</tags>
    <serviceable>true</serviceable>
    <dependencies>
      <group targetFramework="net8.0">
        <!-- 使用变量引用依赖版本 -->
      </group>
    </dependencies>
  </metadata>
  <files>
    <!-- 标准化文件路径 -->
  </files>
</package>
```

## 实施步骤

1. 在Directory.Packages.props中添加CurrentVersion变量
2. 批量更新所有nuspec文件的XML格式
3. 替换硬编码版本为变量引用
4. 统一内部包版本
5. 验证所有nuspec文件格式正确性

## 预期收益

- **版本一致性**: 确保nuspec文件与中央包管理版本同步
- **维护性**: 版本更新只需修改Directory.Packages.props
- **标准化**: 统一的XML格式和结构
- **减少错误**: 避免手动维护版本号导致的不一致