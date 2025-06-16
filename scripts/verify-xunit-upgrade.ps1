# xUnit v3 升级验证脚本
# 验证所有测试项目的升级状态

param(
  [switch]$RunTests = $false
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

# 检查项目的xUnit版本
function Get-XUnitVersion($projectPath) {
  try {
    $content = Get-Content $projectPath -Raw
        
    # 检查是否有xUnit v3
    if ($content -match 'xunit\.v3') {
      return "v3"
    }
    # 检查是否有xUnit v2
    elseif ($content -match 'xunit"[^>]*Version="[2-9]') {
      return "v2"
    }
    else {
      return "Unknown"
    }
  }
  catch {
    return "Error"
  }
}

# 检查项目配置
function Test-XUnitV3Configuration($projectPath) {
  try {
    $content = Get-Content $projectPath -Raw
        
    $hasOutputType = $content -match '<OutputType>Exe</OutputType>'
    $hasXunitV3 = $content -match 'xunit\.v3'
    $hasRunnerVisualStudio = $content -match 'xunit\.runner\.visualstudio.*Version="3\.'
    $hasRunnerJson = $content -match 'xunit\.runner\.json'
    $hasUsingXunit = $content -match '<Using Include="Xunit"'
        
    return @{
      OutputType         = $hasOutputType
      XunitV3            = $hasXunitV3
      RunnerVisualStudio = $hasRunnerVisualStudio
      RunnerJson         = $hasRunnerJson
      UsingXunit         = $hasUsingXunit
      IsComplete         = $hasOutputType -and $hasXunitV3 -and $hasRunnerVisualStudio -and $hasRunnerJson -and $hasUsingXunit
    }
  }
  catch {
    return @{
      OutputType         = $false
      XunitV3            = $false
      RunnerVisualStudio = $false
      RunnerJson         = $false
      UsingXunit         = $false
      IsComplete         = $false
    }
  }
}

# 测试项目运行
function Test-ProjectRun($projectPath) {
  try {
    $projectDir = Split-Path $projectPath -Parent
    $projectName = [System.IO.Path]::GetFileNameWithoutExtension($projectPath)
        
    Write-Info "测试运行项目: $projectName"
        
    # 切换到项目目录并运行
    Push-Location $projectDir
    $result = & dotnet run 2>&1
    Pop-Location
        
    # 检查是否包含xUnit v3运行器输出
    $output = $result -join "`n"
    if ($output -match "xUnit\.net v3 In-Process Runner") {
      return @{ Success = $true; Output = $output }
    }
    else {
      return @{ Success = $false; Output = $output }
    }
  }
  catch {
    Pop-Location
    return @{ Success = $false; Output = $_.Exception.Message }
  }
}

# 主函数
function Main {
  Write-Info "=== xUnit v3 升级验证工具 ==="
  Write-Info "当前工作目录: $PWD"
  Write-Info ""
    
  $projects = Get-TestProjects
    
  Write-Info "找到 $($projects.Count) 个测试项目"
  Write-Info ""
    
  $v3Count = 0
  $v2Count = 0
  $completeCount = 0
  $runSuccessCount = 0
    
  # 验证配置
  Write-Info "=== 配置验证 ==="
  foreach ($project in $projects) {
    $projectName = $project.BaseName
    $version = Get-XUnitVersion $project.FullName
    $config = Test-XUnitV3Configuration $project.FullName
        
    if ($version -eq "v3") {
      $v3Count++
      if ($config.IsComplete) {
        $completeCount++
        Write-Success "✅ $projectName - xUnit v3 (完整配置)"
      }
      else {
        Write-Warning "⚠️  $projectName - xUnit v3 (配置不完整)"
        if (-not $config.OutputType) { Write-Warning "   - 缺少 OutputType=Exe" }
        if (-not $config.RunnerVisualStudio) { Write-Warning "   - 缺少 xunit.runner.visualstudio v3" }
        if (-not $config.RunnerJson) { Write-Warning "   - 缺少 xunit.runner.json 引用" }
        if (-not $config.UsingXunit) { Write-Warning "   - 缺少 Using Xunit 指令" }
      }
    }
    elseif ($version -eq "v2") {
      $v2Count++
      Write-Error "❌ $projectName - xUnit v2 (需要升级)"
    }
    else {
      Write-Error "❓ $projectName - 未知版本"
    }
  }
    
  Write-Info ""
  Write-Info "=== 配置统计 ==="
  Write-Success "xUnit v3 项目: $v3Count"
  Write-Warning "xUnit v2 项目: $v2Count"
  Write-Success "完整配置项目: $completeCount"
    
  # 运行测试（如果指定）
  if ($RunTests) {
    Write-Info ""
    Write-Info "=== 运行测试验证 ==="
        
    $v3Projects = $projects | Where-Object { (Get-XUnitVersion $_.FullName) -eq "v3" }
        
    foreach ($project in $v3Projects) {
      $projectName = $project.BaseName
      $result = Test-ProjectRun $project.FullName
            
      if ($result.Success) {
        $runSuccessCount++
        Write-Success "✅ $projectName - 运行成功"
      }
      else {
        Write-Error "❌ $projectName - 运行失败"
        Write-Info "   错误信息: $($result.Output.Split("`n")[0])"
      }
    }
        
    Write-Info ""
    Write-Info "=== 运行统计 ==="
    Write-Success "运行成功: $runSuccessCount / $v3Count"
  }
    
  Write-Info ""
  Write-Info "=== 总结 ==="
  if ($v2Count -eq 0 -and $completeCount -eq $projects.Count) {
    Write-Success "🎉 所有项目已成功升级到 xUnit v3！"
  }
  elseif ($v2Count -eq 0) {
    Write-Warning "⚠️  所有项目已升级到 xUnit v3，但有 $($v3Count - $completeCount) 个项目配置不完整"
  }
  else {
    Write-Warning "⚠️  还有 $v2Count 个项目需要升级到 xUnit v3"
  }
    
  Write-Info ""
  Write-Info "后续建议:"
  Write-Info "1. 在 VS Code 中打开 Testing 视图验证测试发现"
  Write-Info "2. 运行 'dotnet test' 验证测试执行"
  Write-Info "3. 检查测试代码中的 xUnit v3 兼容性问题"
}

# 执行主函数
Main 