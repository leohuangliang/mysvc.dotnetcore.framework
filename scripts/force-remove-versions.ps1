# 强力移除所有项目文件中的版本信息
param(
  [string]$ProjectPath = ".",
  [switch]$DryRun = $false
)

Write-Host "💪 强力清理所有项目文件中的版本信息..." -ForegroundColor Cyan

$rootPath = Resolve-Path $ProjectPath

# 获取所有项目文件
$projectFiles = Get-ChildItem -Path $rootPath -Recurse -Filter "*.csproj" | Where-Object {
  $_.FullName -notlike "*\bin\*" -and $_.FullName -notlike "*\obj\*"
}

$totalFixed = 0
$filesModified = 0

Write-Host "`n📁 强力清理项目文件..." -ForegroundColor Yellow

foreach ($projectFile in $projectFiles) {
  Write-Host "  📝 处理: $($projectFile.Name)" -ForegroundColor Gray
    
  try {
    $content = Get-Content $projectFile.FullName -Raw
    $originalContent = $content
    $fixedInThisFile = 0
        
    # 使用正则表达式移除所有 Version 属性
    $pattern = '\s+Version="[^"]*"'
    $matches = [regex]::Matches($content, $pattern)
        
    if ($matches.Count -gt 0) {
      foreach ($match in $matches) {
        Write-Host "    🔄 移除版本属性: $($match.Value.Trim())" -ForegroundColor Yellow
        $fixedInThisFile++
        $totalFixed++
      }
            
      if (-not $DryRun) {
        $content = [regex]::Replace($content, $pattern, '')
        Set-Content -Path $projectFile.FullName -Value $content -NoNewline
        $filesModified++
        Write-Host "    ✅ 已修复 $fixedInThisFile 个版本属性" -ForegroundColor Green
      }
      else {
        Write-Host "    📋 预览：将修复 $fixedInThisFile 个版本属性" -ForegroundColor Cyan
      }
    }
    else {
      Write-Host "    ✓ 无需修复" -ForegroundColor DarkGreen
    }
        
  }
  catch {
    Write-Host "    ❌ 处理失败: $($_.Exception.Message)" -ForegroundColor Red
  }
}

# 输出统计信息
Write-Host "`n📊 强力清理统计:" -ForegroundColor Yellow
Write-Host "  📁 检查的项目文件: $($projectFiles.Count)" -ForegroundColor White
Write-Host "  🔧 移除的版本属性: $totalFixed" -ForegroundColor White
Write-Host "  📝 修改的文件: $filesModified" -ForegroundColor White

if ($DryRun) {
  Write-Host "`n💡 这是预览模式，未实际修改文件" -ForegroundColor Cyan
}
else {
  Write-Host "`n✅ 强力清理完成！" -ForegroundColor Green
} 