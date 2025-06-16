# 清理旧版 NuGet 发布方案脚本
# 安全移除包含敏感信息的旧文件

param(
  [switch]$DryRun = $false,
  [switch]$Force = $false
)

# 颜色输出函数
function Write-ColorOutput($ForegroundColor) {
  $fc = $host.UI.RawUI.ForegroundColor
  $host.UI.RawUI.ForegroundColor = $ForegroundColor
  if ($args) { Write-Output $args }
  $host.UI.RawUI.ForegroundColor = $fc
}

function Write-Success { Write-ColorOutput Green $args }
function Write-Warning { Write-ColorOutput Yellow $args }
function Write-Error { Write-ColorOutput Red $args }
function Write-Info { Write-ColorOutput Cyan $args }

Write-Host "=== MySvc.Framework 旧版发布方案清理工具 ===" -ForegroundColor Magenta

# 检查是否发现敏感信息
$publishBatPath = "src\nuget\publish.bat"
$packBatPath = "src\nuget\pack.bat"

$sensitiveFiles = @()
$backupFiles = @()

# 检查 publish.bat 中的敏感信息
if (Test-Path $publishBatPath) {
  $content = Get-Content $publishBatPath -Raw
  if ($content -match "setapikey\s+([a-zA-Z0-9]+)") {
    $apiKey = $matches[1]
    Write-Error "🚨 发现敏感信息！"
    Write-Error "文件: $publishBatPath"
    Write-Error "API Key: $($apiKey.Substring(0, 8))...(已隐藏)"
    $sensitiveFiles += @{
      Path      = $publishBatPath
      Type      = "API Key"
      Sensitive = $true
    }
  }
}

# 检查其他旧文件
$legacyFiles = @(
  @{ Path = $publishBatPath; Type = "发布脚本"; Sensitive = $true },
  @{ Path = $packBatPath; Type = "打包脚本"; Sensitive = $false }
)

Write-Info "`n📋 发现的旧版文件:"
foreach ($file in $legacyFiles) {
  if (Test-Path $file.Path) {
    $icon = if ($file.Sensitive) { "🔴" } else { "🟡" }
    $status = if ($file.Sensitive) { "包含敏感信息" } else { "可安全删除" }
    Write-Host "$icon $($file.Path) - $($file.Type) ($status)"
  }
}

# 安全建议
Write-Warning "`n⚠️ 安全建议:"
Write-Warning "1. 立即撤销泄露的 API Key"
Write-Warning "2. 生成新的 API Key"
Write-Warning "3. 检查 Git 历史记录"
Write-Warning "4. 删除包含敏感信息的文件"

if ($sensitiveFiles.Count -gt 0) {
  Write-Error "`n🚨 发现 $($sensitiveFiles.Count) 个包含敏感信息的文件！"
  Write-Error "建议立即执行以下操作："
  Write-Error "1. 登录 NuGet.org 撤销 API Key"
  Write-Error "2. 生成新的 API Key"
  Write-Error "3. 运行此脚本清理文件"
}

# 如果是预览模式，显示将要执行的操作
if ($DryRun) {
  Write-Info "`n🔍 预览模式 - 将要执行的操作:"
    
  foreach ($file in $legacyFiles) {
    if (Test-Path $file.Path) {
      Write-Info "  - 备份: $($file.Path) -> $($file.Path).backup"
      Write-Info "  - 删除: $($file.Path)"
    }
  }
    
  Write-Info "`n要实际执行清理，请运行："
  Write-Info ".\scripts\cleanup-legacy-nuget.ps1 -Force"
  return
}

# 确认操作
if (-not $Force) {
  Write-Warning "`n⚠️ 此操作将删除旧版发布文件！"
  Write-Warning "包括包含 API Key 的 publish.bat 文件"
    
  $confirm = Read-Host "确认继续? (输入 'DELETE' 确认)"
  if ($confirm -ne "DELETE") {
    Write-Info "操作已取消"
    return
  }
}

# 执行清理操作
Write-Info "`n🧹 开始清理旧版文件..."

$cleanupResults = @{
  BackedUp = @()
  Deleted  = @()
  Errors   = @()
}

foreach ($file in $legacyFiles) {
  if (Test-Path $file.Path) {
    try {
      # 创建备份
      $backupPath = "$($file.Path).backup.$(Get-Date -Format 'yyyyMMdd-HHmmss')"
      Copy-Item $file.Path $backupPath
      $cleanupResults.BackedUp += $backupPath
      Write-Success "✅ 已备份: $($file.Path) -> $backupPath"
            
      # 删除原文件
      Remove-Item $file.Path -Force
      $cleanupResults.Deleted += $file.Path
      Write-Success "✅ 已删除: $($file.Path)"
            
    }
    catch {
      $cleanupResults.Errors += @{
        File  = $file.Path
        Error = $_.Exception.Message
      }
      Write-Error "❌ 删除失败: $($file.Path) - $($_.Exception.Message)"
    }
  }
}

# 创建清理报告
$cleanupReport = @{
  CleanupTime   = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
  BackedUpFiles = $cleanupResults.BackedUp
  DeletedFiles  = $cleanupResults.Deleted
  Errors        = $cleanupResults.Errors
  Summary       = @{
    TotalBackedUp = $cleanupResults.BackedUp.Count
    TotalDeleted  = $cleanupResults.Deleted.Count
    TotalErrors   = $cleanupResults.Errors.Count
  }
}

$reportPath = "legacy-cleanup-report-$(Get-Date -Format 'yyyyMMdd-HHmmss').json"
$cleanupReport | ConvertTo-Json -Depth 5 | Set-Content $reportPath -Encoding UTF8

# 显示清理结果
Write-Info "`n📊 清理结果:"
Write-Success "备份文件: $($cleanupResults.BackedUp.Count) 个"
Write-Success "删除文件: $($cleanupResults.Deleted.Count) 个"
if ($cleanupResults.Errors.Count -gt 0) {
  Write-Error "错误: $($cleanupResults.Errors.Count) 个"
}

Write-Info "`n📄 清理报告已保存: $reportPath"

# 后续操作建议
Write-Info "`n📋 后续操作清单:"
Write-Info "✅ 1. 旧版文件已清理"
Write-Warning "⚠️ 2. 请立即撤销 NuGet API Key"
Write-Warning "⚠️ 3. 生成新的 API Key 并设置环境变量"
Write-Info "✅ 4. 使用新版发布脚本: .\scripts\nuget-release-v2.ps1"

Write-Info "`n🔗 NuGet API Key 管理:"
Write-Info "- 管理页面: https://www.nuget.org/account/apikeys"
Write-Info "- 撤销旧 Key 并生成新 Key"

Write-Info "`n🔐 设置新 API Key:"
Write-Info '$env:NUGET_API_KEY = "your-new-api-key"'

if ($cleanupResults.Errors.Count -eq 0) {
  Write-Success "`n🎉 清理完成！旧版发布方案已安全移除"
}
else {
  Write-Warning "`n⚠️ 清理完成，但有部分错误，请检查详细报告"
} 