#!/usr/bin/env pwsh

<#
.SYNOPSIS
    MySvc.Framework项目警告修复主脚本
.DESCRIPTION
    统一执行所有类型的警告修复，包括XML注释、可空引用类型、过时API等
.PARAMETER ProjectPath
    项目根目录路径
.PARAMETER DryRun
    预览模式，不实际修改文件
.PARAMETER SkipTests
    跳过测试项目的修复
.PARAMETER OnlyType
    只修复特定类型的警告 (xml|nullable|obsolete|all)
#>

param(
  [Parameter(Mandatory = $false)]
  [string]$ProjectPath = ".",
    
  [Parameter(Mandatory = $false)]
  [switch]$DryRun,
    
  [Parameter(Mandatory = $false)]
  [switch]$SkipTests,
    
  [Parameter(Mandatory = $false)]
  [ValidateSet("xml", "nullable", "obsolete", "all")]
  [string]$OnlyType = "all"
)

Write-Host "🚀 MySvc.Framework 项目警告修复工具" -ForegroundColor Cyan
Write-Host "=================================" -ForegroundColor Cyan

# 验证项目路径
if (-not (Test-Path $ProjectPath)) {
  Write-Error "项目路径不存在: $ProjectPath"
  exit 1
}

$scriptsPath = Join-Path $ProjectPath "scripts"
if (-not (Test-Path $scriptsPath)) {
  Write-Error "scripts目录不存在: $scriptsPath"
  exit 1
}

# 记录开始时间
$startTime = Get-Date

# 构建前的警告统计
Write-Host "📊 分析当前警告情况..." -ForegroundColor Yellow
$beforeWarnings = & dotnet build $ProjectPath --verbosity quiet 2>&1 | Select-String "warning" | Measure-Object | Select-Object -ExpandProperty Count

Write-Host "📋 当前警告数量: $beforeWarnings" -ForegroundColor Red

if ($beforeWarnings -eq 0) {
  Write-Host "🎉 恭喜！项目没有警告需要修复。" -ForegroundColor Green
  exit 0
}

# 执行修复
$totalFixed = 0

if ($OnlyType -eq "all" -or $OnlyType -eq "xml") {
  Write-Host "`n🔧 修复XML注释警告 (CS1591)..." -ForegroundColor Green
  $xmlScript = Join-Path $scriptsPath "fix-xml-comments.ps1"
  if (Test-Path $xmlScript) {
    if ($DryRun) {
      & $xmlScript -ProjectPath $ProjectPath -DryRun
    }
    else {
      & $xmlScript -ProjectPath $ProjectPath
    }
  }
  else {
    Write-Warning "XML注释修复脚本不存在: $xmlScript"
  }
}

if ($OnlyType -eq "all" -or $OnlyType -eq "nullable") {
  Write-Host "`n🔧 修复可空引用类型警告 (CS8xxx)..." -ForegroundColor Green
  $nullableScript = Join-Path $scriptsPath "fix-nullable-warnings.ps1"
  if (Test-Path $nullableScript) {
    if ($DryRun) {
      & $nullableScript -ProjectPath $ProjectPath -DryRun
    }
    else {
      & $nullableScript -ProjectPath $ProjectPath
    }
  }
  else {
    Write-Warning "可空引用类型修复脚本不存在: $nullableScript"
  }
}

if ($OnlyType -eq "all" -or $OnlyType -eq "obsolete") {
  Write-Host "`n🔧 修复过时API警告 (CS0618)..." -ForegroundColor Green
  $obsoleteScript = Join-Path $scriptsPath "fix-obsolete-warnings.ps1"
  if (Test-Path $obsoleteScript) {
    if ($DryRun) {
      & $obsoleteScript -ProjectPath $ProjectPath -DryRun
    }
    else {
      & $obsoleteScript -ProjectPath $ProjectPath
    }
  }
  else {
    Write-Warning "过时API修复脚本不存在: $obsoleteScript"
  }
}

if (-not $DryRun) {
  # 重新构建并统计警告
  Write-Host "`n📊 重新分析警告情况..." -ForegroundColor Yellow
  $afterWarnings = & dotnet build $ProjectPath --verbosity quiet 2>&1 | Select-String "warning" | Measure-Object | Select-Object -ExpandProperty Count
    
  $fixedWarnings = $beforeWarnings - $afterWarnings
  $endTime = Get-Date
  $duration = $endTime - $startTime
    
  Write-Host "`n🎯 修复结果统计:" -ForegroundColor Cyan
  Write-Host "  修复前警告: $beforeWarnings" -ForegroundColor Red
  Write-Host "  修复后警告: $afterWarnings" -ForegroundColor Yellow
  Write-Host "  已修复警告: $fixedWarnings" -ForegroundColor Green
  Write-Host "  修复耗时: $($duration.TotalSeconds.ToString('F2')) 秒" -ForegroundColor Blue
    
  if ($afterWarnings -eq 0) {
    Write-Host "`n🎉 恭喜！所有警告已修复完成！" -ForegroundColor Green
  }
  elseif ($fixedWarnings -gt 0) {
    Write-Host "`n✅ 部分警告已修复，剩余警告可能需要手动处理。" -ForegroundColor Yellow
  }
  else {
    Write-Host "`n⚠️  未能自动修复警告，请检查脚本或手动处理。" -ForegroundColor Red
  }
    
  # 显示剩余警告的详细信息
  if ($afterWarnings -gt 0) {
    Write-Host "`n📋 剩余警告详情:" -ForegroundColor Yellow
    & dotnet build $ProjectPath --verbosity normal 2>&1 | Select-String "warning" | Select-Object -First 10 | ForEach-Object {
      Write-Host "  $_" -ForegroundColor DarkYellow
    }
        
    if ($afterWarnings -gt 10) {
      Write-Host "  ... 还有 $($afterWarnings - 10) 个警告" -ForegroundColor DarkYellow
    }
  }
}
else {
  Write-Host "`n🔍 预览模式完成，要实际执行修复请运行:" -ForegroundColor Cyan
  Write-Host "  .\scripts\fix-all-warnings.ps1" -ForegroundColor White
}

# 提供后续建议
Write-Host "`n💡 后续建议:" -ForegroundColor Cyan
Write-Host "  1. 运行完整测试确保修复未破坏功能: dotnet test" -ForegroundColor White
Write-Host "  2. 检查代码差异确认修复正确: git diff" -ForegroundColor White
Write-Host "  3. 考虑在CI/CD中添加警告检查: dotnet build --warnaserror" -ForegroundColor White

if ($afterWarnings -gt 0 -and -not $DryRun) {
  Write-Host "  4. 手动处理剩余警告或调整警告级别" -ForegroundColor White
} 