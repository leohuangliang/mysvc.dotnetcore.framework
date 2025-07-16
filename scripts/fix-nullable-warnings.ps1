#!/usr/bin/env pwsh

<#
.SYNOPSIS
    批量修复MySvc.Framework项目中的可空引用类型警告
.DESCRIPTION
    自动修复CS8618、CS8600、CS8602、CS8604等可空引用类型警告
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

Write-Host "🔧 开始修复可空引用类型警告..." -ForegroundColor Green

function Fix-NullableWarnings {
  param([string]$FilePath)
    
  $content = Get-Content $FilePath -Raw
  $modified = $false
  $originalContent = $content
    
  # CS8618: 非空属性未初始化 - 添加required修饰符或设为可空
  $cs8618Patterns = @{
    # 模式1: public string Property { get; set; }
    'public\s+string\s+(\w+)\s*{\s*get;\s*set;\s*}' = 'public string? $1 { get; set; }'
    # 模式2: private readonly Type _field;
    'private\s+readonly\s+(\w+)\s+(_\w+);'          = 'private readonly $1? $2;'
    # 模式3: private Type _field;
    'private\s+(\w+)\s+(_\w+);'                     = 'private $1? $2;'
  }
    
  foreach ($pattern in $cs8618Patterns.Keys) {
    $replacement = $cs8618Patterns[$pattern]
    if ($content -match $pattern) {
      $content = $content -replace $pattern, $replacement
      $modified = $true
    }
  }
    
  # CS8600: 将null文字或可能的null值转换为非空类型
  $cs8600Patterns = @{
    # 模式1: var variable = nullableValue;
    '(\w+)\s*=\s*(\w+\.SingleOrDefaultAsync\([^)]*\))' = '$1 = await $2'
    # 模式2: string value = possibleNull;
    '(\w+\s+\w+)\s*=\s*(null)'                         = '$1? = $2'
  }
    
  foreach ($pattern in $cs8600Patterns.Keys) {
    $replacement = $cs8600Patterns[$pattern]
    if ($content -match $pattern) {
      $content = $content -replace $pattern, $replacement
      $modified = $true
    }
  }
    
  # CS8602: 可能的null引用的取消引用 - 添加null检查
  $cs8602Patterns = @{
    # 模式1: variable.Method() -> variable?.Method()
    '(\w+)\.(\w+\([^)]*\))(?!\?)' = '$1?.$2'
    # 模式2: variable.Property -> variable?.Property
    '(\w+)\.(\w+)(?!\?)(?!\()'    = '$1?.$2'
  }
    
  # 注意：这些模式需要更精确的匹配，避免误修改
    
  # CS8604: 可能的null引用参数 - 添加null检查
  $nullCheckPattern = '(\w+)\s*\?\s*\.Add\(([^)]+)\)'
  if ($content -match $nullCheckPattern) {
    $content = $content -replace $nullCheckPattern, 'if ($1 != null) $1.Add($2)'
    $modified = $true
  }
    
  if ($modified -and $content -ne $originalContent) {
    if (-not $DryRun) {
      Set-Content $FilePath $content -NoNewline
      Write-Host "✅ 已修复: $FilePath" -ForegroundColor Green
    }
    else {
      Write-Host "🔍 [预览] 将修复: $FilePath" -ForegroundColor Yellow
    }
    return $true
  }
    
  return $false
}

function Fix-SpecificFiles {
  # 修复RedisOption.cs
  $redisOptionFile = Join-Path $ProjectPath "src\Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.Redis\RedisOption.cs"
  if (Test-Path $redisOptionFile) {
    $content = Get-Content $redisOptionFile -Raw
    $modified = $false
        
    # 添加required修饰符
    if ($content -match 'public string ConnectionString \{ get; set; \}') {
      $content = $content -replace 'public string ConnectionString \{ get; set; \}', 'public required string ConnectionString { get; set; }'
      $modified = $true
    }
        
    if ($content -match 'public string InstanceName \{ get; set; \}') {
      $content = $content -replace 'public string InstanceName \{ get; set; \}', 'public required string InstanceName { get; set; }'
      $modified = $true
    }
        
    if ($modified) {
      if (-not $DryRun) {
        Set-Content $redisOptionFile $content -NoNewline
        Write-Host "✅ 已修复: $redisOptionFile" -ForegroundColor Green
      }
      else {
        Write-Host "🔍 [预览] 将修复: $redisOptionFile" -ForegroundColor Yellow
      }
    }
  }
    
  # 修复ReadOnlyMongoDBRepository.cs中的nullable注释上下文
  $mongoRepoFile = Join-Path $ProjectPath "src\Infrastructure.Data.MongoDB\Impl\ReadOnlyMongoDBRepository.cs"
  if (Test-Path $mongoRepoFile) {
    $content = Get-Content $mongoRepoFile -Raw
    $modified = $false
        
    # 添加#nullable enable指令
    if ($content -notmatch '#nullable enable') {
      $content = "#nullable enable`n" + $content
      $modified = $true
    }
        
    if ($modified) {
      if (-not $DryRun) {
        Set-Content $mongoRepoFile $content -NoNewline
        Write-Host "✅ 已修复: $mongoRepoFile" -ForegroundColor Green
      }
      else {
        Write-Host "🔍 [预览] 将修复: $mongoRepoFile" -ForegroundColor Yellow
      }
    }
  }
}

function Fix-TestFiles {
  # 修复测试文件中的null检查
  $testFiles = Get-ChildItem -Path (Join-Path $ProjectPath "test") -Recurse -Filter "*.cs" | Where-Object { $_.Name -notmatch "obj|bin" }
    
  foreach ($testFile in $testFiles) {
    $content = Get-Content $testFile.FullName -Raw
    $modified = $false
        
    # 修复CS8625: Cannot convert null literal to non-nullable reference type
    if ($content -match 'new \w+\(null\)') {
      $content = $content -replace 'new (\w+)\(null\)', 'new $1(null!)'
      $modified = $true
    }
        
    # 修复xUnit1051: 使用TestContext.Current.CancellationToken
    if ($content -match '\.(\w+Async)\(([^)]*)\)' -and $content -notmatch 'TestContext.Current.CancellationToken') {
      # 这个需要更精确的匹配，暂时跳过自动修复
    }
        
    if ($modified) {
      if (-not $DryRun) {
        Set-Content $testFile.FullName $content -NoNewline
        Write-Host "✅ 已修复: $($testFile.FullName)" -ForegroundColor Green
      }
      else {
        Write-Host "🔍 [预览] 将修复: $($testFile.FullName)" -ForegroundColor Yellow
      }
    }
  }
}

# 执行修复
Fix-SpecificFiles
Fix-TestFiles

# 批量处理其他文件
$sourceFiles = Get-ChildItem -Path (Join-Path $ProjectPath "src") -Recurse -Filter "*.cs" | Where-Object { $_.Name -notmatch "obj|bin" }
$fixedCount = 0

foreach ($file in $sourceFiles) {
  if (Fix-NullableWarnings $file.FullName) {
    $fixedCount++
  }
}

Write-Host "🎉 可空引用类型警告修复完成! 共修复 $fixedCount 个文件" -ForegroundColor Green

if ($DryRun) {
  Write-Host "💡 这是预览模式，要实际执行请运行: .\scripts\fix-nullable-warnings.ps1" -ForegroundColor Cyan
} 