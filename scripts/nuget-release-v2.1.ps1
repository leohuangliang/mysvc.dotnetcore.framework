# MySvc.Framework NuGet 包发布脚本 v2.1
# 支持 API Key 配置文件和环境变量两种方式

param(
  [string]$Version = "",
  [switch]$DryRun = $false,
  [switch]$SkipBuild = $false,
  [switch]$SkipTests = $false,
  [switch]$UpdateVersion = $false,
  [string]$ConfigFile = "src\nuget\nuget-config.json",
  [string]$ApiKeyConfigFile = "src\nuget\nuget-apikey.json",
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

# 获取 API Key - 支持配置文件和环境变量
function Get-NuGetApiKey($configFile, $envVar, $nugetSource) {
  $apiKey = $null
  $source = ""
  
  # 1. 优先尝试从配置文件读取
  if (Test-Path $configFile) {
    try {
      Write-Info "📁 尝试从配置文件读取 API Key: $configFile"
      $apiKeyConfig = Get-Content $configFile -Raw | ConvertFrom-Json
      
      if ($apiKeyConfig.defaultSource -and $apiKeyConfig.apiKeys) {
        $defaultSource = $apiKeyConfig.defaultSource
        $keyInfo = $apiKeyConfig.apiKeys.$defaultSource
        
        if ($keyInfo -and $keyInfo.apiKey -and $keyInfo.apiKey -ne "your-nuget-api-key-here") {
          $apiKey = $keyInfo.apiKey
          $source = "配置文件 ($defaultSource)"
          Write-Success "✅ 从配置文件获取 API Key: $($keyInfo.description)"
        }
        else {
          Write-Warning "⚠️ 配置文件中的 API Key 未设置或为默认值"
        }
      }
    }
    catch {
      Write-Warning "⚠️ 配置文件格式错误: $($_.Exception.Message)"
    }
  }
  else {
    Write-Info "📄 API Key 配置文件不存在: $configFile"
  }
  
  # 2. 如果配置文件未提供有效的 API Key，尝试环境变量
  if ([string]::IsNullOrWhiteSpace($apiKey)) {
    Write-Info "🔍 尝试从环境变量读取 API Key: $envVar"
    $apiKey = [Environment]::GetEnvironmentVariable($envVar)
    if (-not [string]::IsNullOrWhiteSpace($apiKey)) {
      $source = "环境变量"
      Write-Success "✅ 从环境变量获取 API Key"
    }
  }
  
  # 3. 如果都没有，返回错误
  if ([string]::IsNullOrWhiteSpace($apiKey)) {
    Write-Error "❌ 未找到有效的 NuGet API Key"
    Write-Info "💡 解决方案："
    Write-Info "   1. 设置环境变量: `$env:$envVar = 'your-api-key'"
    Write-Info "   2. 创建配置文件: $configFile"
    Write-Info "   3. 参考模板文件: $configFile.template"
    return @{ ApiKey = $null; Source = $null }
  }
  
  return @{ ApiKey = $apiKey; Source = $source }
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
Write-Info "=== MySvc.Framework NuGet 包发布工具 v2.1 ==="

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
  $apiKeyResult = Get-NuGetApiKey $ApiKeyConfigFile $ApiKeyEnvVar $config.nugetSource
  if (-not $apiKeyResult.ApiKey) {
    exit 1
  }
  Write-Success "✅ API Key 已获取 (来源: $($apiKeyResult.Source))"
  $apiKey = $apiKeyResult.ApiKey
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
      $testResult = & dotnet test mysvc.dotnetcore.framework.sln --no-build -c Release
      if ($LASTEXITCODE -ne 0) {
        Write-Warning "⚠️ 测试失败，但继续打包"
      }
    }
    Write-Success "✅ 测试完成"
  }

  # 4. 打包
  Write-Info "`n📦 步骤 4: 打包..."
  $packResults = @()
  
  # 切换到 nuget 目录
  Push-Location "src\nuget"
  
  try {
    foreach ($package in $config.packages) {
      $nuspecPath = "nuspecs\$($package.nuspecFile)"
      $outputDir = "nuget-packages\$($package.name)"
          
      Write-Info "打包 $($package.name)..."
      if (-not $DryRun) {
        # 确保输出目录存在
        if (-not (Test-Path $outputDir)) {
          New-Item -ItemType Directory -Path $outputDir -Force | Out-Null
        }
        
        $packResult = & nuget pack $nuspecPath -OutputDirectory $outputDir
        if ($LASTEXITCODE -eq 0) {
          $packResults += @{ Package = $package.name; Success = $true; Path = "$outputDir\$($package.packageName).$Version.nupkg" }
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
  }
  finally {
    Pop-Location
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

  # 6. 总结
  Write-Info "`n📊 发布总结:"
  Write-Info "版本: $Version"
  Write-Info "包数量: $($config.packages.Count)"
    
  if (-not $DryRun) {
    $successCount = ($publishResults | Where-Object { $_.Success }).Count
    $failCount = ($publishResults | Where-Object { -not $_.Success }).Count
        
    Write-Success "✅ 成功发布: $successCount 个包"
    if ($failCount -gt 0) {
      Write-Error "❌ 发布失败: $failCount 个包"
    }
    
    # 生成发布报告
    $report = @{
      Version = $Version
      Timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
      TotalPackages = $config.packages.Count
      SuccessCount = $successCount
      FailCount = $failCount
      ApiKeySource = $apiKeyResult.Source
      FailedPackages = ($publishResults | Where-Object { -not $_.Success } | ForEach-Object { $_.Package })
    }
    
    $reportFile = "nuget-release-report-$(Get-Date -Format 'yyyyMMdd-HHmmss').json"
    $report | ConvertTo-Json -Depth 3 | Set-Content $reportFile -Encoding UTF8
    Write-Info "📄 发布报告已生成: $reportFile"
  }
  else {
    Write-Info "🎯 DryRun 模式完成，未实际执行发布操作"
  }

  Write-Success "`n🎉 NuGet 包发布流程完成！"
}
catch {
  Write-Error "❌ 发布过程中发生错误: $($_.Exception.Message)"
  exit 1
}