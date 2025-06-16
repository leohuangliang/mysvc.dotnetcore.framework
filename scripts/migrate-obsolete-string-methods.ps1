# 迁移过时字符串扩展方法脚本
# 将 IsNullOrBlank 和 NotNullOrBlank 替换为标准的 string.IsNullOrWhiteSpace

param(
  [switch]$DryRun = $false,
  [string]$Path = ".",
  [switch]$IncludeTests = $false
)

# 颜色输出函数
function Write-ColorOutput($ForegroundColor) {
  $fc = $host.UI.RawUI.ForegroundColor
  $host.UI.RawUI.ForegroundColor = $ForegroundColor
  if ($args) {
    Write-Output $args
  }
  $host.UI.RawUI.ForegroundColor = $fc
}

function Write-Success { Write-ColorOutput Green $args }
function Write-Warning { Write-ColorOutput Yellow $args }
function Write-Error { Write-ColorOutput Red $args }
function Write-Info { Write-ColorOutput Cyan $args }

# 获取需要处理的文件
function Get-CSharpFiles($basePath, $includeTests) {
  $files = @()
    
  # 获取所有 .cs 文件
  $allFiles = Get-ChildItem -Path $basePath -Recurse -Filter "*.cs"
    
  foreach ($file in $allFiles) {
    # 跳过 bin 和 obj 目录
    if ($file.FullName -match "\\(bin|obj)\\") {
      continue
    }
        
    # 如果不包含测试文件，跳过测试相关文件
    if (-not $includeTests -and ($file.FullName -match "\\test\\" -or $file.Name -match "Test\.cs$" -or $file.Name -match "Tests\.cs$")) {
      continue
    }
        
    $files += $file
  }
    
  return $files
}

# 检查文件是否包含过时方法
function Test-FileContainsObsoleteMethods($filePath) {
  try {
    $content = Get-Content $filePath -Raw
    return ($content -match '\.IsNullOrBlank\(' -or $content -match '\.NotNullOrBlank\(')
  }
  catch {
    return $false
  }
}

# 迁移单个文件
function Migrate-FileObsoleteMethods($filePath) {
  Write-Info "正在处理文件: $($filePath)"
    
  try {
    $content = Get-Content $filePath -Raw
    $originalContent = $content
    $changeCount = 0
        
    # 替换 .IsNullOrBlank() 为 string.IsNullOrWhiteSpace()
    $pattern1 = '(\w+)\.IsNullOrBlank\(\)'
    $replacement1 = 'string.IsNullOrWhiteSpace($1)'
    $newContent = $content -replace $pattern1, $replacement1
    if ($newContent -ne $content) {
      $matches = [regex]::Matches($content, $pattern1)
      $changeCount += $matches.Count
      Write-Info "  - 替换 $($matches.Count) 个 .IsNullOrBlank() 调用"
      $content = $newContent
    }
        
    # 替换 .NotNullOrBlank() 为 !string.IsNullOrWhiteSpace()
    $pattern2 = '(\w+)\.NotNullOrBlank\(\)'
    $replacement2 = '!string.IsNullOrWhiteSpace($1)'
    $newContent = $content -replace $pattern2, $replacement2
    if ($newContent -ne $content) {
      $matches = [regex]::Matches($content, $pattern2)
      $changeCount += $matches.Count
      Write-Info "  - 替换 $($matches.Count) 个 .NotNullOrBlank() 调用"
      $content = $newContent
    }
        
    if ($changeCount -gt 0) {
      if (-not $DryRun) {
        Set-Content -Path $filePath -Value $content -Encoding UTF8
        Write-Success "  ✅ 文件已更新，共 $changeCount 处修改"
      }
      else {
        Write-Info "  📋 DryRun 模式 - 将要进行 $changeCount 处修改"
      }
      return $changeCount
    }
    else {
      Write-Info "  ⏭️  文件无需修改"
      return 0
    }
  }
  catch {
    Write-Error "  ❌ 处理文件失败: $($_.Exception.Message)"
    return 0
  }
}

# 生成迁移报告
function Generate-MigrationReport($results) {
  Write-Info ""
  Write-Info "=== 迁移报告 ==="
    
  $totalFiles = $results.Count
  $modifiedFiles = ($results | Where-Object { $_.Changes -gt 0 }).Count
  $totalChanges = ($results | Measure-Object -Property Changes -Sum).Sum
    
  Write-Success "处理文件总数: $totalFiles"
  Write-Success "修改文件数量: $modifiedFiles"
  Write-Success "总修改次数: $totalChanges"
    
  if ($modifiedFiles -gt 0) {
    Write-Info ""
    Write-Info "修改的文件列表:"
    foreach ($result in $results | Where-Object { $_.Changes -gt 0 }) {
      Write-Info "  - $($result.File): $($result.Changes) 处修改"
    }
  }
    
  Write-Info ""
  Write-Info "=== 迁移指南 ==="
  Write-Info "已完成的替换:"
  Write-Info "  • variable.IsNullOrBlank() → string.IsNullOrWhiteSpace(variable)"
  Write-Info "  • variable.NotNullOrBlank() → !string.IsNullOrWhiteSpace(variable)"
  Write-Info ""
  Write-Info "后续步骤:"
  Write-Info "1. 编译项目验证语法正确性"
  Write-Info "2. 运行测试确保功能正常"
  Write-Info "3. 检查是否还有手动需要处理的复杂情况"
}

# 主函数
function Main {
  Write-Info "=== 过时字符串扩展方法迁移工具 ==="
  Write-Info "当前工作目录: $PWD"
  Write-Info "处理路径: $Path"
  Write-Info "包含测试文件: $IncludeTests"
    
  if ($DryRun) {
    Write-Warning "运行在 DryRun 模式，不会实际修改文件"
  }
    
  Write-Info ""
    
  # 获取所有需要处理的文件
  $files = Get-CSharpFiles $Path $IncludeTests
  $targetFiles = @()
    
  Write-Info "扫描包含过时方法的文件..."
  foreach ($file in $files) {
    if (Test-FileContainsObsoleteMethods $file.FullName) {
      $targetFiles += $file
    }
  }
    
  if ($targetFiles.Count -eq 0) {
    Write-Success "🎉 没有找到使用过时方法的文件！"
    return
  }
    
  Write-Info "找到 $($targetFiles.Count) 个需要迁移的文件"
  Write-Info ""
    
  # 处理每个文件
  $results = @()
  foreach ($file in $targetFiles) {
    $changes = Migrate-FileObsoleteMethods $file.FullName
    $result = New-Object PSObject -Property @{
      File     = $file.Name
      FullPath = $file.FullName
      Changes  = $changes
    }
    $results += $result
  }
    
  # 生成报告
  Generate-MigrationReport $results
    
  if (-not $DryRun -and $results.Count -gt 0) {
    Write-Info ""
    Write-Warning "建议运行以下命令验证修改:"
    Write-Info "dotnet build"
    Write-Info "dotnet test"
  }
}

# 执行主函数
Main 