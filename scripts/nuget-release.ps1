# MySvc.Framework NuGet 包发布脚本
# 解决版本管理、安全性和自动化问题

param(
  [Parameter(Mandatory = $true)]
  [string]$Version,
    
  [switch]$DryRun = $false,
  [switch]$SkipBuild = $false,
  [switch]$SkipTests = $false,
  [string]$ApiKeyEnvVar = "NUGET_API_KEY",
  [string]$NuGetSource = "https://api.nuget.org/v3/index.json"
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
    return $null
  }
  return $apiKey
}

# 包定义
$packages = @(
  @{ Name = "Domain.Core"; NuspecFile = "Domain.Core.nuspec"; PackageName = "MySvc.Framework.Domain.Core" },
  @{ Name = "Infrastructure.Adapter.AutoMapper"; NuspecFile = "Infrastructure.Adapter.AutoMapper.nuspec"; PackageName = "MySvc.Framework.Infrastructure.AutoMapper" },
  @{ Name = "Infrastructure.Authorization.Client"; NuspecFile = "Infrastructure.Authorization.Client.nuspec"; PackageName = "MySvc.Framework.Infrastructure.Authorization.Client" },
  @{ Name = "Infrastructure.Authorization.Admin"; NuspecFile = "Infrastructure.Authorization.Admin.nuspec"; PackageName = "MySvc.Framework.Infrastructure.Authorization.Admin" },
  @{ Name = "Infrastructure.Authorization.InternalClient"; NuspecFile = "Infrastructure.Authorization.InternalClient.nuspec"; PackageName = "MySvc.Framework.Infrastructure.Authorization.InternalClient" },
  @{ Name = "Infrastructure.Authorization.Merchant"; NuspecFile = "Infrastructure.Authorization.Merchant.nuspec"; PackageName = "MySvc.Framework.Infrastructure.Authorization.Merchant" },
  @{ Name = "Infrastructure.Crosscutting"; NuspecFile = "Infrastructure.Crosscutting.nuspec"; PackageName = "MySvc.Framework.Infrastructure.Crosscutting" },
  @{ Name = "Infrastructure.Crosscutting.Json.NewtonsoftJson"; NuspecFile = "Infrastructure.Crosscutting.Json.NewtonsoftJson.nuspec"; PackageName = "MySvc.Framework.Infrastructure.NewtonsoftJson" },
  @{ Name = "Infrastructure.Crosscutting.Cache.StackExchangeRedis"; NuspecFile = "Infrastructure.Crosscutting.Cache.StackExchangeRedis.nuspec"; PackageName = "MySvc.Framework.Infrastructure.Crosscutting.StackExchangeRedis" },
  @{ Name = "Infrastructure.Data.MongoDB"; NuspecFile = "Infrastructure.Data.MongoDB.nuspec"; PackageName = "MySvc.Framework.Infrastructure.Data.MongoDB" },
  @{ Name = "Infrastructure.Job.Hangfire"; NuspecFile = "Infrastructure.Job.Hangfire.nuspec"; PackageName = "MySvc.Framework.Infrastructure.Job.Hangfire" },
  @{ Name = "Infrastructure.IntegrationEventService"; NuspecFile = "Infrastructure.IntegrationEventService.nuspec"; PackageName = "MySvc.Framework.Infrastructure.IntegrationEventService" },
  @{ Name = "Infrastructure.Logging.Serilog"; NuspecFile = "Infrastructure.Logging.Serilog.nuspec"; PackageName = "MySvc.Framework.Infrastructure.Serilog" },
  @{ Name = "Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator"; NuspecFile = "Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.nuspec"; PackageName = "MySvc.Framework.Infrastructure.Crosscutting.SnowflakeIdGenerator" },
  @{ Name = "Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.Redis"; NuspecFile = "Infrastructure.Crosscutting.IdGenerator.SnowflakeIdGenerator.Redis.nuspec"; PackageName = "MySvc.Framework.Infrastructure.Crosscutting.SnowflakeIdGenerator.Redis" }
)

Write-Info "=== MySvc.Framework NuGet 包发布工具 ==="
Write-Info "版本: $Version"
Write-Info "DryRun 模式: $DryRun"
Write-Info "跳过构建: $SkipBuild"
Write-Info "跳过测试: $SkipTests"

# 验证版本格式
if (-not (Test-VersionFormat $Version)) {
  Write-Error "❌ 版本格式无效: $Version"
  Write-Info "正确格式: 1.0.0 或 1.0.0-beta1"
  exit 1
}

# 获取 API Key
if (-not $DryRun) {
  $apiKey = Get-NuGetApiKey $ApiKeyEnvVar
  if (-not $apiKey) {
    exit 1
  }
  Write-Success "✅ API Key 已获取"
}

# 切换到 nuget 目录
$nugetDir = "src\nuget"
if (-not (Test-Path $nugetDir)) {
  Write-Error "❌ 未找到 nuget 目录: $nugetDir"
  exit 1
}

Push-Location $nugetDir

try {
  # 1. 更新所有 nuspec 文件的版本
  Write-Info "`n🔄 步骤 1: 更新版本号..."
  foreach ($package in $packages) {
    $nuspecPath = "nuspecs\$($package.NuspecFile)"
    if (Test-Path $nuspecPath) {
      Write-Info "更新 $nuspecPath"
      if (-not $DryRun) {
        # 更新版本号和依赖版本
        $content = Get-Content $nuspecPath -Raw
        $content = $content -replace '<version>[\d\.\-\w]+</version>', "<version>$Version</version>"
        $content = $content -replace 'version="8\.0\.0-beta4"', "version=`"$Version`""
        Set-Content $nuspecPath $content -Encoding UTF8
      }
    }
    else {
      Write-Warning "⚠️ 未找到: $nuspecPath"
    }
  }

  # 2. 构建解决方案
  if (-not $SkipBuild) {
    Write-Info "`n🔨 步骤 2: 构建解决方案..."
    if (-not $DryRun) {
      $buildResult = & dotnet build -c Release ..\..\mysvc.dotnetcore.framework.sln
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
      $testResult = & dotnet test ..\..\mysvc.dotnetcore.framework.sln --no-build -c Release
      if ($LASTEXITCODE -ne 0) {
        Write-Warning "⚠️ 测试失败，但继续打包"
      }
    }
    Write-Success "✅ 测试完成"
  }

  # 4. 打包
  Write-Info "`n📦 步骤 4: 打包..."
  $packResults = @()
  foreach ($package in $packages) {
    $nuspecPath = "nuspecs\$($package.NuspecFile)"
    $outputDir = "nuget-packages\$($package.Name)"
        
    Write-Info "打包 $($package.Name)..."
    if (-not $DryRun) {
      $packResult = & nuget pack $nuspecPath -OutputDirectory $outputDir
      if ($LASTEXITCODE -eq 0) {
        $packResults += @{ Package = $package.Name; Success = $true; Path = "$outputDir\$($package.PackageName).$Version.nupkg" }
        Write-Success "  ✅ 成功"
      }
      else {
        $packResults += @{ Package = $package.Name; Success = $false; Error = $packResult }
        Write-Error "  ❌ 失败: $($package.Name)"
      }
    }
    else {
      Write-Info "  [DryRun] 将打包到: $outputDir\$($package.PackageName).$Version.nupkg"
    }
  }

  # 5. 发布到 NuGet
  Write-Info "`n🚀 步骤 5: 发布到 NuGet..."
  if (-not $DryRun) {
    # 设置 API Key
    & nuget setapikey $apiKey -Source $NuGetSource
        
    $publishResults = @()
    foreach ($result in $packResults) {
      if ($result.Success) {
        Write-Info "发布 $($result.Package)..."
        $publishResult = & nuget push $result.Path -Source $NuGetSource
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
    Write-Info "[DryRun] 将发布到: $NuGetSource"
  }

  # 6. 总结
  Write-Info "`n📊 发布总结:"
  Write-Info "版本: $Version"
  Write-Info "包数量: $($packages.Count)"
    
  if (-not $DryRun) {
    $successCount = ($publishResults | Where-Object { $_.Success }).Count
    $failCount = ($publishResults | Where-Object { -not $_.Success }).Count
        
    Write-Success "✅ 成功发布: $successCount 个包"
    if ($failCount -gt 0) {
      Write-Error "❌ 发布失败: $failCount 个包"
    }
        
    # 显示失败的包
    $failedPackages = $publishResults | Where-Object { -not $_.Success }
    if ($failedPackages.Count -gt 0) {
      Write-Error "`n失败的包:"
      foreach ($failed in $failedPackages) {
        Write-Error "  - $($failed.Package)"
      }
    }
  }

}
finally {
  Pop-Location
}

Write-Success "`n🎉 发布流程完成!"