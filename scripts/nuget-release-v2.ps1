# MySvc.Framework NuGet 包发布脚本 v2.0
# 基于配置文件的现代化版本管理和发布系统

param(
  [string]$Version = "",
  [switch]$DryRun = $false,
  [switch]$SkipBuild = $false,
  [switch]$SkipTests = $false,
  [switch]$UpdateVersion = $false,
  [string]$ConfigFile = "src\nuget\nuget-config.json",
  [string]$ApiKeyEnvVar = "NUGET_API_KEY"
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

# 验证版本格式
function Test-VersionFormat($version) {
  return $version -match '^\d+\.\d+\.\d+(-[a-zA-Z0-9\-\.]+)?$'
}

# 获取 API Key
function Get-NuGetApiKey($envVar) {
  $apiKey = [Environment]::GetEnvironmentVariable($envVar)
  if ([string]::IsNullOrWhiteSpace($apiKey)) {
    Write-Error "❌ 未找到 NuGet API Key 环境变量: $envVar"
    Write-Info "请设置环境变量: `$env:$envVar = 'your-api-key'"
    Write-Info "或者使用 Azure Key Vault / GitHub Secrets 等安全存储"
    return $null
  }
  return $apiKey
}

# 加载配置文件
function Get-NuGetConfig($configPath) {
  if (-not (Test-Path $configPath)) {
    Write-Error "❌ 配置文件不存在: $configPath"
    return $null
  }
    
  try {
    $config = Get-Content $configPath -Raw | ConvertFrom-Json
    return $config
  }
  catch {
    Write-Error "❌ 配置文件格式错误: $($_.Exception.Message)"
    return $null
  }
}

# 更新配置文件版本
function Update-ConfigVersion($configPath, $newVersion) {
  $config = Get-NuGetConfig $configPath
  if (-not $config) { return $false }
    
  $config.version = $newVersion
  $config.releaseNotes = "版本 $newVersion 发布"
    
  try {
    $config | ConvertTo-Json -Depth 10 | Set-Content $configPath -Encoding UTF8
    Write-Success "✅ 配置文件版本已更新: $newVersion"
    return $true
  }
  catch {
    Write-Error "❌ 更新配置文件失败: $($_.Exception.Message)"
    return $false
  }
}

# 生成 nuspec 文件
function Update-NuspecFile($package, $config) {
  $nuspecPath = "src\nuget\nuspecs\$($package.nuspecFile)"
    
  if (-not (Test-Path $nuspecPath)) {
    Write-Warning "⚠️ nuspec 文件不存在: $nuspecPath"
    return $false
  }
    
  try {
    [xml]$nuspec = Get-Content $nuspecPath
        
    # 更新基本信息
    $nuspec.package.metadata.version = $config.version
    $nuspec.package.metadata.releaseNotes = $config.releaseNotes
    $nuspec.package.metadata.authors = $config.author
    $nuspec.package.metadata.owners = $config.author
    $nuspec.package.metadata.copyright = $config.copyright
    $nuspec.package.metadata.tags = $config.tags
    $nuspec.package.metadata.description = $package.description
        
    # 更新依赖版本
    if ($nuspec.package.metadata.dependencies -and $package.dependencies) {
      foreach ($group in $nuspec.package.metadata.dependencies.group) {
        if ($group.dependency) {
          foreach ($dep in $group.dependency) {
            $configDep = $package.dependencies | Where-Object { $_.id -eq $dep.id }
            if ($configDep) {
              $depVersion = $configDep.version -replace '\{\{VERSION\}\}', $config.version
              $dep.version = $depVersion
            }
          }
        }
      }
    }
        
    $nuspec.Save($nuspecPath)
    Write-Info "  ✅ 已更新: $($package.nuspecFile)"
    return $true
  }
  catch {
    Write-Error "  ❌ 更新失败: $($package.nuspecFile) - $($_.Exception.Message)"
    return $false
  }
}

# 主程序开始
Write-Info "=== MySvc.Framework NuGet 包发布工具 v2.0 ==="

# 加载配置
$config = Get-NuGetConfig $ConfigFile
if (-not $config) {
  exit 1
}

# 确定版本
if ([string]::IsNullOrWhiteSpace($Version)) {
  $Version = $config.version
  Write-Info "使用配置文件中的版本: $Version"
}
else {
  Write-Info "使用指定版本: $Version"
}

# 验证版本格式
if (-not (Test-VersionFormat $Version)) {
  Write-Error "❌ 版本格式无效: $Version"
  Write-Info "正确格式: 1.0.0 或 1.0.0-beta1"
  exit 1
}

# 更新配置文件版本
if ($UpdateVersion -and $Version -ne $config.version) {
  if (-not (Update-ConfigVersion $ConfigFile $Version)) {
    exit 1
  }
  # 重新加载配置
  $config = Get-NuGetConfig $ConfigFile
}

Write-Info "DryRun 模式: $DryRun"
Write-Info "跳过构建: $SkipBuild"
Write-Info "跳过测试: $SkipTests"
Write-Info "包数量: $($config.packages.Count)"

# 获取 API Key
if (-not $DryRun) {
  $apiKey = Get-NuGetApiKey $ApiKeyEnvVar
  if (-not $apiKey) {
    exit 1
  }
  Write-Success "✅ API Key 已获取"
}

try {
  # 1. 更新所有 nuspec 文件
  Write-Info "`n🔄 步骤 1: 更新 nuspec 文件..."
  $updateResults = @()
  foreach ($package in $config.packages) {
    Write-Info "更新 $($package.name)..."
    if (-not $DryRun) {
      $success = Update-NuspecFile $package $config
      $updateResults += @{ Package = $package.name; Success = $success }
    }
    else {
      Write-Info "  [DryRun] 将更新: $($package.nuspecFile)"
    }
  }

  # 2. 构建解决方案
  if (-not $SkipBuild) {
    Write-Info "`n🔨 步骤 2: 构建解决方案..."
    if (-not $DryRun) {
      $buildResult = & dotnet build -c Release mysvc.dotnetcore.framework.sln
      if ($LASTEXITCODE -ne 0) {
        Write-Error "❌ 构建失败"
        exit 1
      }
    }
    Write-Success "✅ 构建完成"
  }

  # 3. 运行测试
  if (-not $SkipTests) {
    Write-Info "`n🧪 步骤 3: 运行测试..."
    if (-not $DryRun) {
      $testResult = & dotnet test mysvc.dotnetcore.framework.sln --no-build -c Release --logger console
      if ($LASTEXITCODE -ne 0) {
        Write-Warning "⚠️ 测试失败，但继续打包"
      }
    }
    Write-Success "✅ 测试完成"
  }

  # 4. 打包
  Write-Info "`n📦 步骤 4: 打包..."
  Push-Location "src\nuget"
    
  $packResults = @()
  foreach ($package in $config.packages) {
    $nuspecPath = "nuspecs\$($package.nuspecFile)"
    $outputDir = "nuget-packages\$($package.name)"
        
    Write-Info "打包 $($package.name)..."
    if (-not $DryRun) {
      $packResult = & nuget pack $nuspecPath -OutputDirectory $outputDir
      if ($LASTEXITCODE -eq 0) {
        $packResults += @{ 
          Package     = $package.name
          Success     = $true
          Path        = "$outputDir\$($package.packageName).$Version.nupkg"
          PackageName = $package.packageName
        }
        Write-Success "  ✅ 成功"
      }
      else {
        $packResults += @{ Package = $package.name; Success = $false; Error = $packResult }
        Write-Error "  ❌ 失败: $($package.name)"
      }
    }
    else {
      Write-Info "  [DryRun] 将打包到: $outputDir\$($package.packageName).$Version.nupkg"
    }
  }

  # 5. 发布到 NuGet
  Write-Info "`n🚀 步骤 5: 发布到 NuGet..."
  if (-not $DryRun) {
    # 设置 API Key
    & nuget setapikey $apiKey -Source $config.nugetSource
        
    $publishResults = @()
    foreach ($result in $packResults) {
      if ($result.Success) {
        Write-Info "发布 $($result.Package)..."
        $publishResult = & nuget push $result.Path -Source $config.nugetSource
        if ($LASTEXITCODE -eq 0) {
          $publishResults += @{ Package = $result.Package; Success = $true }
          Write-Success "  ✅ 发布成功"
        }
        else {
          $publishResults += @{ Package = $result.Package; Success = $false; Error = $publishResult }
          Write-Error "  ❌ 发布失败: $($result.Package)"
        }
      }
    }
  }
  else {
    Write-Info "[DryRun] 将发布到: $($config.nugetSource)"
  }

  Pop-Location

  # 6. 总结报告
  Write-Info "`n📊 发布总结:"
  Write-Info "版本: $Version"
  Write-Info "包数量: $($config.packages.Count)"
  Write-Info "发布源: $($config.nugetSource)"
    
  if (-not $DryRun) {
    $successCount = ($publishResults | Where-Object { $_.Success }).Count
    $failCount = ($publishResults | Where-Object { -not $_.Success }).Count
        
    Write-Success "✅ 成功发布: $successCount 个包"
    if ($failCount -gt 0) {
      Write-Error "❌ 发布失败: $failCount 个包"
            
      # 显示失败的包
      $failedPackages = $publishResults | Where-Object { -not $_.Success }
      Write-Error "`n失败的包:"
      foreach ($failed in $failedPackages) {
        Write-Error "  - $($failed.Package)"
      }
    }
        
    # 生成发布报告
    $report = @{
      Version        = $Version
      Timestamp      = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
      TotalPackages  = $config.packages.Count
      SuccessCount   = $successCount
      FailCount      = $failCount
      FailedPackages = ($failedPackages | ForEach-Object { $_.Package })
    }
        
    $reportPath = "nuget-release-report-$Version.json"
    $report | ConvertTo-Json -Depth 3 | Set-Content $reportPath -Encoding UTF8
    Write-Info "📄 发布报告已保存: $reportPath"
  }

}
catch {
  Write-Error "❌ 发布过程中发生错误: $($_.Exception.Message)"
  exit 1
}

Write-Success "`n🎉 发布流程完成!"

# 提供后续建议
Write-Info "`n💡 后续建议:"
Write-Info "1. 检查 NuGet.org 上的包状态"
Write-Info "2. 更新项目文档和 CHANGELOG"
Write-Info "3. 创建 Git 标签: git tag v$Version"
Write-Info "4. 推送标签: git push origin v$Version" 