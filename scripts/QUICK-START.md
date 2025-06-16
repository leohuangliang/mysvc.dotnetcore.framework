# MySvc.Framework NuGet 工具快速使用指南

## 🚀 快速开始

### 1. 环境准备
```powershell
# 安装 NuGet CLI（如果需要）
.\scripts\install-nuget-cli.ps1

# 设置 API Key
$env:NUGET_API_KEY = "your-nuget-api-key"
```

### 2. 常用命令

#### 📦 发布包
```powershell
# 预览发布（推荐先执行）
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta6" -DryRun

# 实际发布
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta6" -UpdateVersion
```

#### 🔍 检查状态
```powershell
# 检查包状态
.\scripts\check-nuget-status.ps1 -Version "8.0.0-beta6"

# 检查最新版本
.\scripts\check-nuget-status.ps1 -CheckLatest
```

#### 🏥 健康检查
```powershell
# 完整健康检查
.\scripts\nuget-health-check.ps1

# 查看详细信息
.\scripts\nuget-health-check.ps1 -ShowDetails
```

#### 📊 分析统计
```powershell
# 基本分析
.\scripts\nuget-analytics.ps1

# 图表显示
.\scripts\nuget-analytics.ps1 -OutputFormat Chart
```

## 🔄 完整工作流程

### 发布新版本
```powershell
# 1. 健康检查
.\scripts\nuget-health-check.ps1

# 2. 预览发布
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta6" -DryRun

# 3. 实际发布
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta6" -UpdateVersion

# 4. 验证发布
.\scripts\check-nuget-status.ps1 -Version "8.0.0-beta6"

# 5. 创建 Git 标签
git tag v8.0.0-beta6
git push origin v8.0.0-beta6
```

### 定期维护
```powershell
# 每周执行：健康检查
.\scripts\nuget-health-check.ps1

# 每月执行：分析统计
.\scripts\nuget-analytics.ps1 -OutputFormat Chart

# 随时执行：状态检查
.\scripts\check-nuget-status.ps1 -CheckLatest
```

## 📋 工具对比

| 工具                     | 用途     | 输出     | 频率 |
|--------------------------|----------|----------|------|
| `nuget-release-v2.ps1`   | 发布包   | 发布报告 | 按需 |
| `check-nuget-status.ps1` | 检查状态 | 状态报告 | 随时 |
| `nuget-health-check.ps1` | 健康检查 | 健康报告 | 每周 |
| `nuget-analytics.ps1`    | 分析统计 | 分析报告 | 每月 |

## ⚡ 快捷命令

### 一键检查所有状态
```powershell
# 创建批量检查脚本
@"
Write-Host "=== 包状态检查 ===" -ForegroundColor Cyan
.\scripts\check-nuget-status.ps1 -CheckLatest

Write-Host "`n=== 健康检查 ===" -ForegroundColor Cyan  
.\scripts\nuget-health-check.ps1

Write-Host "`n=== 分析统计 ===" -ForegroundColor Cyan
.\scripts\nuget-analytics.ps1 -OutputFormat Chart
"@ | Out-File -FilePath "scripts\check-all.ps1" -Encoding UTF8

# 执行批量检查
.\scripts\check-all.ps1
```

## 🔧 故障排除

### 常见错误
1. **API Key 未设置**: `$env:NUGET_API_KEY = "your-key"`
2. **NuGet CLI 未找到**: `.\scripts\install-nuget-cli.ps1`
3. **网络连接问题**: 检查防火墙和代理设置
4. **版本已存在**: 使用新的版本号

### 调试模式
```powershell
# 启用详细输出
$VerbosePreference = "Continue"
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta6" -DryRun -Verbose
```

## 📊 报告文件

所有工具都会生成 JSON 格式的详细报告：
- `nuget-release-report-*.json` - 发布报告
- `nuget-status-report-*.json` - 状态报告  
- `nuget-health-report-*.json` - 健康报告
- `nuget-analytics-report-*.json` - 分析报告

## 🔗 相关链接

- [完整文档](README-nuget-release.md)
- [NuGet.org 包管理](https://www.nuget.org/account/Packages)
- [MySvc.Framework 搜索](https://www.nuget.org/packages?q=MySvc.Framework)

---

💡 **提示**: 建议将这些命令添加到你的开发工作流程中，定期执行以确保包的健康状态。 