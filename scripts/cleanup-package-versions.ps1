# 清理项目文件中的包版本信息，启用中央包管理
param(
  [string]$ProjectPath = ".",
  [switch]$DryRun = $false,
  [switch]$Backup = $true
)

Write-Host "🧹 清理 MySvc.Framework 项目文件中的包版本信息..." -ForegroundColor Cyan
Write-Host "📍 模式: $(if($DryRun) { "预览模式（不修改文件）" } else { "执行模式" })" -ForegroundColor Yellow

$rootPath = Resolve-Path $ProjectPath
$packagesPropsPath = Join-Path $rootPath "Directory.Packages.props"

# 检查 Directory.Packages.props 是否存在
if (-not (Test-Path $packagesPropsPath)) {
  Write-Host "❌ 未找到 Directory.Packages.props 文件，请先创建该文件" -ForegroundColor Red
  exit 1
}

# 读取中央包管理的包列表
Write-Host "`n📦 读取中央包管理配置..." -ForegroundColor Yellow
[xml]$packagesXml = Get-Content $packagesPropsPath
$centralPackages = @{}

foreach ($itemGroup in $packagesXml.Project.ItemGroup) {
  if ($itemGroup.PackageReference) {
    foreach ($packageRef in $itemGroup.PackageReference) {
      if ($packageRef.Update) {
        $centralPackages[$packageRef.Update] = $packageRef.Version
        Write-Host "  ✓ $($packageRef.Update): $($packageRef.Version)" -ForegroundColor Green
      }
    }
  }
}

Write-Host "  📊 共找到 $($centralPackages.Count) 个中央管理的包" -ForegroundColor Cyan

# 查找所有项目文件
$projectFiles = Get-ChildItem -Path $rootPath -Recurse -Filter "*.csproj" | Where-Object {
  $_.FullName -notlike "*\bin\*" -and $_.FullName -notlike "*\obj\*"
}

Write-Host "`n📁 处理项目文件..." -ForegroundColor Yellow
$totalCleaned = 0
$filesModified = 0

foreach ($projectFile in $projectFiles) {
  Write-Host "  📝 处理: $($projectFile.Name)" -ForegroundColor Gray
    
  try {
    [xml]$projectXml = Get-Content $projectFile.FullName
    $modified = $false
    $cleanedInThisFile = 0
        
    # 备份原文件
    if ($Backup -and -not $DryRun) {
      $backupPath = "$($projectFile.FullName).backup"
      Copy-Item $projectFile.FullName $backupPath -Force
      Write-Host "    💾 已备份到: $($projectFile.Name).backup" -ForegroundColor DarkGray
    }
        
    # 处理所有 ItemGroup
    foreach ($itemGroup in $projectXml.Project.ItemGroup) {
      if ($itemGroup.PackageReference) {
        $packagesInGroup = @($itemGroup.PackageReference)
                
        for ($i = $packagesInGroup.Count - 1; $i -ge 0; $i--) {
          $packageRef = $packagesInGroup[$i]
                    
          if ($packageRef.Include -and $packageRef.Version) {
            $packageName = $packageRef.Include
            $currentVersion = $packageRef.Version
                        
            # 检查是否在中央管理列表中
            if ($centralPackages.ContainsKey($packageName)) {
              $centralVersion = $centralPackages[$packageName]
                            
              Write-Host "    🔄 ${packageName}: ${currentVersion} → 中央管理(${centralVersion})" -ForegroundColor Yellow
                            
              if (-not $DryRun) {
                # 移除 Version 属性
                $packageRef.RemoveAttribute("Version")
                $modified = $true
                $cleanedInThisFile++
                $totalCleaned++
              }
              else {
                $cleanedInThisFile++
                $totalCleaned++
              }
            }
          }
        }
      }
    }
        
    # 保存修改后的文件
    if ($modified -and -not $DryRun) {
      $projectXml.Save($projectFile.FullName)
      $filesModified++
      Write-Host "    ✅ 已保存，清理了 $cleanedInThisFile 个包版本" -ForegroundColor Green
    }
    elseif ($cleanedInThisFile -gt 0) {
      Write-Host "    📋 预览：将清理 $cleanedInThisFile 个包版本" -ForegroundColor Cyan
    }
    else {
      Write-Host "    ⚪ 无需处理" -ForegroundColor DarkGray
    }
        
  }
  catch {
    Write-Host "    ❌ 处理失败: $($_.Exception.Message)" -ForegroundColor Red
  }
}

# 输出统计信息
Write-Host "`n📊 处理统计:" -ForegroundColor Yellow
Write-Host "  📁 处理的项目文件: $($projectFiles.Count)" -ForegroundColor White
Write-Host "  📦 清理的包版本: $totalCleaned" -ForegroundColor White
Write-Host "  📝 修改的文件: $filesModified" -ForegroundColor White

if ($DryRun) {
  Write-Host "`n💡 这是预览模式，未实际修改文件" -ForegroundColor Cyan
  Write-Host "   使用不带 -DryRun 参数执行实际清理" -ForegroundColor Cyan
}
else {
  Write-Host "`n✅ 清理完成！" -ForegroundColor Green
  Write-Host "💡 建议运行以下命令验证配置:" -ForegroundColor Cyan
  Write-Host "   dotnet restore" -ForegroundColor Gray
  Write-Host "   dotnet build" -ForegroundColor Gray
}

if ($Backup -and $filesModified -gt 0 -and -not $DryRun) {
  Write-Host "`n🔄 恢复备份文件命令:" -ForegroundColor Yellow
  Write-Host "   Get-ChildItem -Recurse -Filter '*.csproj.backup' | ForEach-Object { Move-Item `$_.FullName (`$_.FullName -replace '\.backup$', '') -Force }" -ForegroundColor Gray
} 