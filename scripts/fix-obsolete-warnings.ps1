#!/usr/bin/env pwsh

<#
.SYNOPSIS
    批量修复MySvc.Framework项目中的过时API警告
.DESCRIPTION
    自动修复CS0618等过时API警告，更新到最新的API调用方式
.PARAMETER ProjectPath
    项目根目录路径
.PARAMETER DryRun
    预览模式，不实际修改文件
#>

param(
  [Parameter(Mandatory = $false)]
  [string]$ProjectPath = ".",
    
  [Parameter(Mandatory = $false)]
  [switch]$DryRun
)

Write-Host "🔧 开始修复过时API警告..." -ForegroundColor Green

function Fix-HangfireObsoleteApis {
  param([string]$FilePath)
    
  $content = Get-Content $FilePath -Raw
  $modified = $false
    
  # 修复 RecurringJob.AddOrUpdate 过时API
  $oldPattern1 = 'RecurringJob\.AddOrUpdate<([^>]+)>\(\s*([^,]+),\s*([^,]+),\s*([^,]+),\s*([^,]+),\s*([^)]+)\)'
  $newPattern1 = 'RecurringJob.AddOrUpdate<$1>($2, $3, $4, new RecurringJobOptions { TimeZone = $5 })'
    
  if ($content -match $oldPattern1) {
    $content = $content -replace $oldPattern1, $newPattern1
    $modified = $true
  }
    
  # 修复 RecurringJob.Trigger 过时API
  $oldPattern2 = 'RecurringJob\.Trigger\(([^)]+)\)'
  $newPattern2 = 'BackgroundJob.TriggerJob($1)'
    
  if ($content -match $oldPattern2) {
    $content = $content -replace $oldPattern2, $newPattern2
    $modified = $true
  }
    
  # 添加必要的using语句
  if ($modified -and $content -notmatch 'using Hangfire;') {
    $content = "using Hangfire;`n" + $content
  }
    
  if ($modified) {
    if (-not $DryRun) {
      Set-Content $FilePath $content -NoNewline
      Write-Host "✅ 已修复Hangfire过时API: $FilePath" -ForegroundColor Green
    }
    else {
      Write-Host "🔍 [预览] 将修复Hangfire过时API: $FilePath" -ForegroundColor Yellow
    }
  }
    
  return $modified
}

function Fix-NLogObsoleteApis {
  param([string]$FilePath)
    
  $content = Get-Content $FilePath -Raw
  $modified = $false
    
  # 修复 NLogBuilder 过时API
  $oldPattern = 'NLogBuilder\.ConfigureNLog\(([^)]+)\)'
  $newPattern = 'LogManager.Setup().LoadConfigurationFromAppSettings()'
    
  if ($content -match $oldPattern) {
    $content = $content -replace $oldPattern, $newPattern
    $modified = $true
        
    # 添加必要的using语句
    if ($content -notmatch 'using NLog;') {
      $content = "using NLog;`n" + $content
    }
  }
    
  if ($modified) {
    if (-not $DryRun) {
      Set-Content $FilePath $content -NoNewline
      Write-Host "✅ 已修复NLog过时API: $FilePath" -ForegroundColor Green
    }
    else {
      Write-Host "🔍 [预览] 将修复NLog过时API: $FilePath" -ForegroundColor Yellow
    }
  }
    
  return $modified
}

function Fix-AspNetCoreObsoleteApis {
  param([string]$FilePath)
    
  $content = Get-Content $FilePath -Raw
  $modified = $false
    
  # 修复 CompatibilityVersion 过时API
  $oldPattern1 = '\.SetCompatibilityVersion\([^)]+\)'
  if ($content -match $oldPattern1) {
    $content = $content -replace $oldPattern1, ''
    $modified = $true
  }
    
  # 删除 CompatibilityVersion 相关代码
  $oldPattern2 = ',\s*CompatibilityVersion\.[^,\)]*'
  if ($content -match $oldPattern2) {
    $content = $content -replace $oldPattern2, ''
    $modified = $true
  }
    
  if ($modified) {
    if (-not $DryRun) {
      Set-Content $FilePath $content -NoNewline
      Write-Host "✅ 已修复ASP.NET Core过时API: $FilePath" -ForegroundColor Green
    }
    else {
      Write-Host "🔍 [预览] 将修复ASP.NET Core过时API: $FilePath" -ForegroundColor Yellow
    }
  }
    
  return $modified
}

function Fix-RepositoryObsoleteApis {
  param([string]$FilePath)
    
  $content = Get-Content $FilePath -Raw
  $modified = $false
    
  # 修复过时的Repository方法
  $oldPattern = '\.FindInPageAsync\('
  $newPattern = '.FindPagedAsync('
    
  if ($content -match $oldPattern) {
    $content = $content -replace $oldPattern, $newPattern
    $modified = $true
  }
    
  if ($modified) {
    if (-not $DryRun) {
      Set-Content $FilePath $content -NoNewline
      Write-Host "✅ 已修复Repository过时API: $FilePath" -ForegroundColor Green
    }
    else {
      Write-Host "🔍 [预览] 将修复Repository过时API: $FilePath" -ForegroundColor Yellow
    }
  }
    
  return $modified
}

# 执行修复
$fixedCount = 0

# 修复Hangfire相关文件
$hangfireFiles = Get-ChildItem -Path (Join-Path $ProjectPath "src") -Recurse -Filter "*Hangfire*.cs" | Where-Object { $_.Name -notmatch "obj|bin" }
foreach ($file in $hangfireFiles) {
  if (Fix-HangfireObsoleteApis $file.FullName) {
    $fixedCount++
  }
}

# 修复NLog相关文件
$programFiles = Get-ChildItem -Path (Join-Path $ProjectPath "samples") -Recurse -Filter "Program.cs" | Where-Object { $_.Name -notmatch "obj|bin" }
foreach ($file in $programFiles) {
  if (Fix-NLogObsoleteApis $file.FullName) {
    $fixedCount++
  }
}

# 修复ASP.NET Core相关文件
$mvcExtensionFiles = Get-ChildItem -Path (Join-Path $ProjectPath "samples") -Recurse -Filter "*Extension*.cs" | Where-Object { $_.Name -notmatch "obj|bin" }
foreach ($file in $mvcExtensionFiles) {
  if (Fix-AspNetCoreObsoleteApis $file.FullName) {
    $fixedCount++
  }
}

# 修复Repository相关文件
$queryFiles = Get-ChildItem -Path (Join-Path $ProjectPath "samples") -Recurse -Filter "*Queries.cs" | Where-Object { $_.Name -notmatch "obj|bin" }
foreach ($file in $queryFiles) {
  if (Fix-RepositoryObsoleteApis $file.FullName) {
    $fixedCount++
  }
}

Write-Host "🎉 过时API警告修复完成! 共修复 $fixedCount 个文件" -ForegroundColor Green

if ($DryRun) {
  Write-Host "💡 这是预览模式，要实际执行请运行: .\scripts\fix-obsolete-warnings.ps1" -ForegroundColor Cyan
} 