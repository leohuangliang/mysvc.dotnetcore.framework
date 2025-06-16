# 修复损坏的项目文件脚本
# 修复被清理脚本破坏的XML结构

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
    $projects = Get-ChildItem -Path $testDir -Recurse -Filter "*.csproj"
    foreach ($project in $projects) {
      $testProjects += $project
    }
  }
    
  return $testProjects
}

# 检查项目文件是否损坏
function Test-ProjectBroken($projectPath) {
  try {
    # 尝试加载为XML
    [xml]$xml = Get-Content $projectPath
    return $false
  }
  catch {
    return $true
  }
}

# 修复单个项目文件
function Fix-BrokenProject($projectPath) {
  Write-Info "正在修复项目: $($projectPath)"
    
  try {
    $content = Get-Content $projectPath -Raw
    $originalContent = $content
        
    # 基本清理：移除孤立的标签和属性
    $content = $content -replace '^\s*<PrivateAssets>.*?</PrivateAssets>\s*$', ''
    $content = $content -replace '^\s*<IncludeAssets>.*?</IncludeAssets>\s*$', ''
    $content = $content -replace '^\s*</PackageReference>\s*$', ''
    $content = $content -replace '^\s*<PackageReference[^>]*>\s*$', ''
        
    # 清理空的ItemGroup
    $content = $content -replace '<ItemGroup>\s*</ItemGroup>', ''
        
    # 确保PropertyGroup正确
    if ($content -notmatch '<IsTestProject>true</IsTestProject>') {
      $content = $content -replace '(<IsPackable>false</IsPackable>)', "`$1`r`n    <IsTestProject>true</IsTestProject>"
    }
        
    if ($content -notmatch '<OutputType>Exe</OutputType>') {
      $content = $content -replace '(<IsTestProject>true</IsTestProject>)', "`$1`r`n    <OutputType>Exe</OutputType>"
    }
        
    # 确保有正确的xUnit v3包引用
    if ($content -notmatch 'xunit\.v3') {
      $xunitPackages = @"

  <ItemGroup>
    <PackageReference Include="xunit.v3" Version="2.0.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.1" />
  </ItemGroup>
"@
      $content = $content -replace '(</Project>)', "$xunitPackages`$1"
    }
        
    # 确保有xunit.runner.json配置
    if ($content -notmatch 'xunit\.runner\.json') {
      $runnerConfig = @"

  <ItemGroup>
    <Content Include="xunit.runner.json" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>
"@
      $content = $content -replace '(</Project>)', "$runnerConfig`$1"
    }
        
    # 确保有Using指令
    if ($content -notmatch '<Using Include="Xunit"') {
      $usingDirective = @"

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>
"@
      $content = $content -replace '(</Project>)', "$usingDirective`$1"
    }
        
    # 清理多余的空行
    $content = $content -replace '(\r?\n\s*){3,}', "`r`n`r`n"
        
    if (-not $DryRun) {
      Set-Content -Path $projectPath -Value $content -Encoding UTF8
            
      # 创建xunit.runner.json文件（如果不存在）
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
            
      Write-Success "项目修复完成: $projectPath"
    }
    else {
      Write-Info "DryRun 模式 - 将要修复项目结构"
    }
        
    return $true
  }
  catch {
    Write-Error "修复项目失败: $projectPath"
    Write-Error $_.Exception.Message
    return $false
  }
}

# 主函数
function Main {
  Write-Info "=== 损坏项目文件修复工具 ==="
  Write-Info "当前工作目录: $PWD"
    
  if ($DryRun) {
    Write-Warning "运行在 DryRun 模式，不会实际修改文件"
  }
    
  $projects = Get-TestProjects
  $brokenProjects = @()
    
  Write-Info "检查损坏的项目文件..."
  foreach ($project in $projects) {
    if (Test-ProjectBroken $project.FullName) {
      $brokenProjects += $project
      Write-Warning "发现损坏的项目: $($project.Name)"
    }
  }
    
  if ($brokenProjects.Count -eq 0) {
    Write-Success "没有发现损坏的项目文件！"
    return
  }
    
  Write-Info "找到 $($brokenProjects.Count) 个损坏的项目，开始修复..."
    
  $fixedCount = 0
  $failCount = 0
    
  foreach ($project in $brokenProjects) {
    if (Fix-BrokenProject $project.FullName) {
      $fixedCount++
    }
    else {
      $failCount++
    }
  }
    
  Write-Info "=== 修复完成 ==="
  Write-Success "成功修复: $fixedCount 个项目"
  if ($failCount -gt 0) {
    Write-Error "修复失败: $failCount 个项目"
  }
    
  if (-not $DryRun -and $fixedCount -gt 0) {
    Write-Info ""
    Write-Info "后续步骤:"
    Write-Info "1. 运行 'dotnet restore' 恢复包引用"
    Write-Info "2. 运行 'dotnet build' 验证编译"
  }
}

# 执行主函数
Main 