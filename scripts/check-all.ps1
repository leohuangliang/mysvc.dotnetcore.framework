# MySvc.Framework 批量检查脚本
# 一键执行所有监控工具

param(
  [string]$Version = "",
  [switch]$SkipAnalytics = $false,
  [switch]$SkipHealth = $false,
  [switch]$SkipStatus = $false
)

Write-Host "=== MySvc.Framework 批量检查工具 ===" -ForegroundColor Magenta
Write-Host "检查时间: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')" -ForegroundColor Gray

$startTime = Get-Date
$results = @{
  Status    = @{ Success = $false; Error = $null }
  Health    = @{ Success = $false; Error = $null }
  Analytics = @{ Success = $false; Error = $null }
}

# 1. 包状态检查
if (-not $SkipStatus) {
  Write-Host "`n=== 📊 包状态检查 ===" -ForegroundColor Cyan
  try {
    if ($Version) {
      & ".\scripts\check-nuget-status.ps1" -Version $Version
    }
    else {
      & ".\scripts\check-nuget-status.ps1" -CheckLatest
    }
        
    if ($LASTEXITCODE -eq 0) {
      $results.Status.Success = $true
      Write-Host "✅ 状态检查完成" -ForegroundColor Green
    }
    else {
      $results.Status.Error = "退出代码: $LASTEXITCODE"
      Write-Host "⚠️ 状态检查发现问题" -ForegroundColor Yellow
    }
  }
  catch {
    $results.Status.Error = $_.Exception.Message
    Write-Host "❌ 状态检查失败: $($_.Exception.Message)" -ForegroundColor Red
  }
}
else {
  Write-Host "⏭️ 跳过状态检查" -ForegroundColor Gray
}

# 2. 健康检查
if (-not $SkipHealth) {
  Write-Host "`n=== 🏥 健康检查 ===" -ForegroundColor Cyan
  try {
    & ".\scripts\nuget-health-check.ps1"
        
    if ($LASTEXITCODE -eq 0) {
      $results.Health.Success = $true
      Write-Host "✅ 健康检查完成" -ForegroundColor Green
    }
    else {
      $results.Health.Error = "退出代码: $LASTEXITCODE"
      Write-Host "⚠️ 健康检查发现问题" -ForegroundColor Yellow
    }
  }
  catch {
    $results.Health.Error = $_.Exception.Message
    Write-Host "❌ 健康检查失败: $($_.Exception.Message)" -ForegroundColor Red
  }
}
else {
  Write-Host "⏭️ 跳过健康检查" -ForegroundColor Gray
}

# 3. 分析统计
if (-not $SkipAnalytics) {
  Write-Host "`n=== 📈 分析统计 ===" -ForegroundColor Cyan
  try {
    & ".\scripts\nuget-analytics.ps1" -OutputFormat Chart
        
    if ($LASTEXITCODE -eq 0) {
      $results.Analytics.Success = $true
      Write-Host "✅ 分析统计完成" -ForegroundColor Green
    }
    else {
      $results.Analytics.Error = "退出代码: $LASTEXITCODE"
      Write-Host "⚠️ 分析统计发现问题" -ForegroundColor Yellow
    }
  }
  catch {
    $results.Analytics.Error = $_.Exception.Message
    Write-Host "❌ 分析统计失败: $($_.Exception.Message)" -ForegroundColor Red
  }
}
else {
  Write-Host "⏭️ 跳过分析统计" -ForegroundColor Gray
}

# 生成总结报告
$endTime = Get-Date
$duration = $endTime - $startTime

Write-Host "`n=== 📋 检查总结 ===" -ForegroundColor Magenta
Write-Host "总耗时: $([math]::Round($duration.TotalSeconds, 1)) 秒" -ForegroundColor Gray

$successCount = 0
$totalCount = 0

if (-not $SkipStatus) {
  $totalCount++
  if ($results.Status.Success) {
    $successCount++
    Write-Host "✅ 状态检查: 成功" -ForegroundColor Green
  }
  else {
    Write-Host "❌ 状态检查: 失败 - $($results.Status.Error)" -ForegroundColor Red
  }
}

if (-not $SkipHealth) {
  $totalCount++
  if ($results.Health.Success) {
    $successCount++
    Write-Host "✅ 健康检查: 成功" -ForegroundColor Green
  }
  else {
    Write-Host "❌ 健康检查: 失败 - $($results.Health.Error)" -ForegroundColor Red
  }
}

if (-not $SkipAnalytics) {
  $totalCount++
  if ($results.Analytics.Success) {
    $successCount++
    Write-Host "✅ 分析统计: 成功" -ForegroundColor Green
  }
  else {
    Write-Host "❌ 分析统计: 失败 - $($results.Analytics.Error)" -ForegroundColor Red
  }
}

$successRate = if ($totalCount -gt 0) { [math]::Round(($successCount / $totalCount) * 100, 1) } else { 0 }

Write-Host "`n📊 成功率: $successCount/$totalCount ($successRate%)" -ForegroundColor $(if ($successRate -eq 100) { "Green" } elseif ($successRate -ge 66) { "Yellow" } else { "Red" })

# 保存批量检查报告
$batchReport = @{
  CheckTime     = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
  Duration      = $duration.TotalSeconds
  Version       = $Version
  Results       = $results
  Summary       = @{
    TotalChecks      = $totalCount
    SuccessfulChecks = $successCount
    SuccessRate      = $successRate
  }
  SkippedChecks = @{
    Status    = $SkipStatus
    Health    = $SkipHealth
    Analytics = $SkipAnalytics
  }
}

$reportPath = "nuget-batch-check-report-$(Get-Date -Format 'yyyyMMdd-HHmmss').json"
$batchReport | ConvertTo-Json -Depth 5 | Set-Content $reportPath -Encoding UTF8
Write-Host "`n📄 批量检查报告已保存: $reportPath" -ForegroundColor Cyan

# 提供建议
if ($successRate -lt 100) {
  Write-Host "`n💡 建议:" -ForegroundColor Yellow
  Write-Host "- 检查网络连接和 API Key 设置" -ForegroundColor Yellow
  Write-Host "- 查看详细错误信息进行故障排除" -ForegroundColor Yellow
  Write-Host "- 使用 -Verbose 参数获取更多调试信息" -ForegroundColor Yellow
}

Write-Host "`n🔗 有用的链接:" -ForegroundColor Cyan
Write-Host "- NuGet.org 搜索: https://www.nuget.org/packages?q=MySvc.Framework" -ForegroundColor Cyan
Write-Host "- 包管理页面: https://www.nuget.org/account/Packages" -ForegroundColor Cyan

if ($successRate -eq 100) {
  Write-Host "`n🎉 所有检查都成功完成！" -ForegroundColor Green
  exit 0
}
else {
  Write-Host "`n⚠️ 部分检查失败，请查看详细信息" -ForegroundColor Yellow
  exit 1
} 