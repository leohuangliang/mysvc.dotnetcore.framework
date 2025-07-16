# 修复中央包管理错误
param(
  [string]$ProjectPath = ".",
  [switch]$DryRun = $false
)

Write-Host "🔧 修复中央包管理错误..." -ForegroundColor Cyan

$rootPath = Resolve-Path $ProjectPath

# 获取所有项目文件
$projectFiles = Get-ChildItem -Path $rootPath -Recurse -Filter "*.csproj" | Where-Object {
  $_.FullName -notlike "*\bin\*" -and $_.FullName -notlike "*\obj\*"
}

$totalFixed = 0
$filesModified = 0

Write-Host "`n📁 扫描并修复项目文件..." -ForegroundColor Yellow

foreach ($projectFile in $projectFiles) {
  Write-Host "  📝 检查: $($projectFile.Name)" -ForegroundColor Gray
    
  try {
    [xml]$projectXml = Get-Content $projectFile.FullName
    $modified = $false
    $fixedInThisFile = 0
        
    # 处理所有 ItemGroup
    foreach ($itemGroup in $projectXml.Project.ItemGroup) {
      if ($itemGroup.PackageReference) {
        $packagesInGroup = @($itemGroup.PackageReference)
                
        for ($i = $packagesInGroup.Count - 1; $i -ge 0; $i--) {
          $packageRef = $packagesInGroup[$i]
                    
          # 移除任何残留的 Version 属性
          if ($packageRef.Include -and $packageRef.Version) {
            $packageName = $packageRef.Include
            $version = $packageRef.Version
                        
            Write-Host "    🔄 移除版本: ${packageName} v${version}" -ForegroundColor Yellow
                        
            if (-not $DryRun) {
              $packageRef.RemoveAttribute("Version")
              $modified = $true
              $fixedInThisFile++
              $totalFixed++
            }
            else {
              $fixedInThisFile++
              $totalFixed++
            }
          }
        }
      }
    }
        
    # 保存修改后的文件
    if ($modified -and -not $DryRun) {
      $projectXml.Save($projectFile.FullName)
      $filesModified++
      Write-Host "    ✅ 已修复 $fixedInThisFile 个版本引用" -ForegroundColor Green
    }
    elseif ($fixedInThisFile -gt 0) {
      Write-Host "    📋 预览：将修复 $fixedInThisFile 个版本引用" -ForegroundColor Cyan
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
Write-Host "`n📊 修复统计:" -ForegroundColor Yellow
Write-Host "  📁 检查的项目文件: $($projectFiles.Count)" -ForegroundColor White
Write-Host "  🔧 修复的版本引用: $totalFixed" -ForegroundColor White
Write-Host "  📝 修改的文件: $filesModified" -ForegroundColor White

if ($DryRun) {
  Write-Host "`n💡 这是预览模式，未实际修改文件" -ForegroundColor Cyan
}
else {
  Write-Host "`n✅ 修复完成！" -ForegroundColor Green
} 