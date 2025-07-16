# 验证包集中管理配置效果
param(
  [string]$ProjectPath = ".",
  [switch]$Detailed = $false
)

Write-Host "🔍 验证 MySvc.Framework 包集中管理配置..." -ForegroundColor Cyan

# 检查配置文件是否存在
$rootPath = Resolve-Path $ProjectPath
$buildPropsPath = Join-Path $rootPath "Directory.Build.props"
$packagesPropsPath = Join-Path $rootPath "Directory.Packages.props"
$testBuildPropsPath = Join-Path $rootPath "test\Directory.Build.props"

Write-Host "`n📁 配置文件检查:" -ForegroundColor Yellow
Write-Host "  ✓ Directory.Build.props: $(Test-Path $buildPropsPath)" -ForegroundColor $(if (Test-Path $buildPropsPath) { "Green" } else { "Red" })
Write-Host "  ✓ Directory.Packages.props: $(Test-Path $packagesPropsPath)" -ForegroundColor $(if (Test-Path $packagesPropsPath) { "Green" } else { "Red" })
Write-Host "  ✓ test/Directory.Build.props: $(Test-Path $testBuildPropsPath)" -ForegroundColor $(if (Test-Path $testBuildPropsPath) { "Green" } else { "Red" })

# 生成预处理文件进行验证
Write-Host "`n🔧 生成预处理文件验证配置..." -ForegroundColor Yellow

$testProject = Get-ChildItem -Path $rootPath -Recurse -Filter "*.csproj" | Where-Object { $_.Name -like "*Test*" } | Select-Object -First 1
$srcProject = Get-ChildItem -Path "$rootPath\src" -Recurse -Filter "*.csproj" | Select-Object -First 1

if ($testProject) {
  Write-Host "  📝 测试项目验证: $($testProject.Name)" -ForegroundColor Green
  $testOutput = Join-Path $testProject.DirectoryName "preprocessed-test.xml"
  try {
    & dotnet msbuild $testProject.FullName /pp:$testOutput /nologo /verbosity:quiet
    if (Test-Path $testOutput) {
      Write-Host "    ✓ 预处理文件已生成: $testOutput" -ForegroundColor Green
            
      # 检查关键配置
      $content = Get-Content $testOutput -Raw
      $hasTestConfig = $content -match 'IsTestProject.*true'
      $hasPackageManagement = $content -match 'ManagePackageVersionsCentrally.*true'
            
      Write-Host "    ✓ 测试项目标识: $hasTestConfig" -ForegroundColor $(if ($hasTestConfig) { "Green" } else { "Red" })
      Write-Host "    ✓ 中央包管理: $hasPackageManagement" -ForegroundColor $(if ($hasPackageManagement) { "Green" } else { "Red" })
    }
  }
  catch {
    Write-Host "    ❌ 预处理失败: $($_.Exception.Message)" -ForegroundColor Red
  }
}

if ($srcProject) {
  Write-Host "  📝 源码项目验证: $($srcProject.Name)" -ForegroundColor Green
  $srcOutput = Join-Path $srcProject.DirectoryName "preprocessed-src.xml"
  try {
    & dotnet msbuild $srcProject.FullName /pp:$srcOutput /nologo /verbosity:quiet
    if (Test-Path $srcOutput) {
      Write-Host "    ✓ 预处理文件已生成: $srcOutput" -ForegroundColor Green
            
      # 检查版本统一
      $content = Get-Content $srcOutput -Raw
      $hasVersionControl = $content -match 'LatestVersion.*8\.0\.0-beta4'
      $hasNullable = $content -match 'Nullable.*enable'
            
      Write-Host "    ✓ 版本统一管理: $hasVersionControl" -ForegroundColor $(if ($hasVersionControl) { "Green" } else { "Red" })
      Write-Host "    ✓ Nullable 启用: $hasNullable" -ForegroundColor $(if ($hasNullable) { "Green" } else { "Red" })
    }
  }
  catch {
    Write-Host "    ❌ 预处理失败: $($_.Exception.Message)" -ForegroundColor Red
  }
}

# 检查包版本一致性
Write-Host "`n📦 包版本一致性检查:" -ForegroundColor Yellow

$projectFiles = Get-ChildItem -Path $rootPath -Recurse -Filter "*.csproj"
$packageVersions = @{}

foreach ($proj in $projectFiles) {
  [xml]$xml = Get-Content $proj.FullName
  $packageRefs = $xml.Project.ItemGroup.PackageReference
    
  if ($packageRefs) {
    foreach ($ref in $packageRefs) {
      if ($ref.Include -and $ref.Version) {
        $packageName = $ref.Include
        if (-not $packageVersions.ContainsKey($packageName)) {
          $packageVersions[$packageName] = @()
        }
        $packageVersions[$packageName] += @{
          Version = $ref.Version
          Project = $proj.Name
        }
      }
    }
  }
}

# 查找版本冲突
$conflicts = @()
foreach ($package in $packageVersions.Keys) {
  $versions = $packageVersions[$package] | Group-Object Version
  if ($versions.Count -gt 1) {
    $conflicts += @{
      Package  = $package
      Versions = $versions
    }
  }
}

if ($conflicts.Count -gt 0) {
  Write-Host "  ⚠️  发现版本冲突:" -ForegroundColor Red
  foreach ($conflict in $conflicts) {
    Write-Host "    📦 $($conflict.Package):" -ForegroundColor Yellow
    foreach ($version in $conflict.Versions) {
      Write-Host "      - v$($version.Name): $($version.Group.Count) 个项目" -ForegroundColor Gray
      if ($Detailed) {
        foreach ($proj in $version.Group) {
          Write-Host "        * $($proj.Project)" -ForegroundColor DarkGray
        }
      }
    }
  }
}
else {
  Write-Host "  ✓ 未发现版本冲突" -ForegroundColor Green
}

# 清理临时文件
Write-Host "`n🧹 清理临时文件..." -ForegroundColor Yellow
Get-ChildItem -Path $rootPath -Recurse -Filter "preprocessed-*.xml" | Remove-Item -Force
Write-Host "  ✓ 临时文件已清理" -ForegroundColor Green

Write-Host "`n✅ 验证完成!" -ForegroundColor Green
Write-Host "💡 使用 -Detailed 参数查看详细信息" -ForegroundColor Cyan 