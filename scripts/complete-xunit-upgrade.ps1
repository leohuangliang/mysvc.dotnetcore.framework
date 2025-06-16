# 完整的 xUnit v3 升级脚本
# 一次性处理所有升级问题

param(
  [switch]$DryRun = $false
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

# 获取所有测试项目
function Get-TestProjects {
  $testProjects = @()
    
  # 扫描 test 目录下的所有 .csproj 文件
  $testDir = Join-Path $PSScriptRoot "../test"
  if (Test-Path $testDir) {
    $projects = Get-ChildItem -Path $testDir -Recurse -Filter "*.csproj" | Where-Object { $_.Directory.Name -notmatch "bin|obj" }
    foreach ($project in $projects) {
      $testProjects += $project
    }
  }
    
  return $testProjects
}

# 完全重写项目文件为标准的xUnit v3格式
function Upgrade-ProjectToXUnitV3($projectPath) {
  Write-Info "正在升级项目: $($projectPath)"
    
  try {
    $content = Get-Content $projectPath -Raw
    $projectName = [System.IO.Path]::GetFileNameWithoutExtension($projectPath)
        
    # 提取现有的ProjectReference
    $projectRefs = @()
    $matches = [regex]::Matches($content, '<ProjectReference Include="([^"]*)"[^>]*>')
    foreach ($match in $matches) {
      $projectRefs += $match.Groups[1].Value
    }
        
    # 提取现有的有用PackageReference（排除xunit相关的）
    $packageRefs = @()
    $matches = [regex]::Matches($content, '<PackageReference Include="([^"]*)" Version="([^"]*)"[^>]*>')
    foreach ($match in $matches) {
      $packageName = $match.Groups[1].Value
      $version = $match.Groups[2].Value
            
      # 排除xunit相关包和Microsoft.NET.Test.Sdk
      if ($packageName -notmatch "xunit|Microsoft\.NET\.Test\.Sdk") {
        $packageRefs += @{ Name = $packageName; Version = $version }
      }
    }
        
    # 生成新的项目文件内容
    $newContent = @"
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
    <OutputType>Exe</OutputType>
  </PropertyGroup>

  <ItemGroup>
    <Content Include="xunit.runner.json" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="xunit.v3" Version="2.0.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.1" />
"@

    # 添加其他包引用
    foreach ($pkg in $packageRefs) {
      $newContent += "`n    <PackageReference Include=`"$($pkg.Name)`" Version=`"$($pkg.Version)`" />"
    }
        
    $newContent += "`n  </ItemGroup>"
        
    # 添加项目引用
    if ($projectRefs.Count -gt 0) {
      $newContent += "`n`n  <ItemGroup>"
      foreach ($projRef in $projectRefs) {
        $newContent += "`n    <ProjectReference Include=`"$projRef`" />"
      }
      $newContent += "`n  </ItemGroup>"
    }
        
    $newContent += "`n`n</Project>"
        
    if (-not $DryRun) {
      # 保存新的项目文件
      Set-Content -Path $projectPath -Value $newContent -Encoding UTF8
            
      # 创建xunit.runner.json文件
      $projectDir = Split-Path $projectPath -Parent
      $runnerJsonPath = Join-Path $projectDir "xunit.runner.json"
            
      if (-not (Test-Path $runnerJsonPath)) {
        $runnerJsonContent = @"
{
  "methodDisplay": "method",
  "methodDisplayOptions": "all",
  "parallelizeTestCollections": true,
  "maxParallelThreads": -1,
  "preEnumerateTheories": true,
  "diagnosticMessages": false,
  "internalDiagnosticMessages": false
}
"@
        Set-Content -Path $runnerJsonPath -Value $runnerJsonContent -Encoding UTF8
      }
            
      Write-Success "项目升级完成: $projectName"
    }
    else {
      Write-Info "DryRun 模式 - 将要重写项目文件为标准xUnit v3格式"
    }
        
    return $true
  }
  catch {
    Write-Error "升级项目失败: $projectPath"
    Write-Error $_.Exception.Message
    return $false
  }
}

# 主函数
function Main {
  Write-Info "=== 完整 xUnit v3 升级工具 ==="
  Write-Info "当前工作目录: $PWD"
    
  if ($DryRun) {
    Write-Warning "运行在 DryRun 模式，不会实际修改文件"
  }
    
  $projects = Get-TestProjects
    
  Write-Info "找到 $($projects.Count) 个测试项目"
  Write-Info "开始完整升级到 xUnit v3..."
    
  $successCount = 0
  $failCount = 0
    
  foreach ($project in $projects) {
    if (Upgrade-ProjectToXUnitV3 $project.FullName) {
      $successCount++
    }
    else {
      $failCount++
    }
  }
    
  Write-Info "=== 升级完成 ==="
  Write-Success "成功升级: $successCount 个项目"
  if ($failCount -gt 0) {
    Write-Error "升级失败: $failCount 个项目"
  }
    
  if (-not $DryRun -and $successCount -gt 0) {
    Write-Info ""
    Write-Info "后续步骤:"
    Write-Info "1. 运行 'dotnet restore' 恢复包引用"
    Write-Info "2. 运行 'dotnet build' 验证编译"
    Write-Info "3. 测试各个项目的运行情况"
    Write-Info "4. 在 VS Code 中验证测试发现功能"
  }
}

# 执行主函数
Main 