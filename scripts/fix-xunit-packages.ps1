# 修复 xUnit v3 包引用脚本
# 用于修复已经部分升级但缺少包引用的项目

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
      try {
        $content = Get-Content $project.FullName -Raw
        # 检查是否是测试项目
        if ($content -match "<IsTestProject>true</IsTestProject>" -or 
          $content -match "xunit" -or 
          $content -match "Microsoft\.NET\.Test\.Sdk") {
          $testProjects += $project
        }
      }
      catch {
        Write-Warning "无法读取项目文件: $($project.FullName)"
      }
    }
  }
    
  return $testProjects
}

# 检查项目是否需要修复包引用
function Test-NeedsPackageFix($projectPath) {
  try {
    $content = Get-Content $projectPath -Raw
    $hasOutputType = $content -match "<OutputType>Exe</OutputType>"
    $hasXunitV3 = $content -match "xunit\.v3"
    $hasRunnerJson = $content -match "xunit\.runner\.json"
        
    # 如果有 OutputType 和 runner.json 但没有 xunit.v3，说明需要修复
    return $hasOutputType -and $hasRunnerJson -and -not $hasXunitV3
  }
  catch {
    return $false
  }
}

# 修复单个项目的包引用
function Fix-ProjectPackages($projectPath) {
  Write-Info "正在修复项目: $($projectPath)"
    
  if (-not (Test-NeedsPackageFix $projectPath)) {
    Write-Warning "项目不需要修复或已经正确配置: $projectPath"
    return $true
  }
    
  try {
    $content = Get-Content $projectPath -Raw
    $originalContent = $content
        
    # 添加 xUnit v3 包引用
    if ($content -notmatch "xunit\.v3") {
      # 查找现有的 PackageReference ItemGroup
      if ($content -match '(<ItemGroup>\s*<PackageReference[^>]*.*?</ItemGroup>)') {
        # 在现有的 PackageReference 组中添加
        $newPackages = @"
    <PackageReference Include="xunit.v3" Version="2.0.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.1" />
"@
        # 找到第一个 PackageReference ItemGroup 的结束标签前插入
        $pattern = '(<ItemGroup>\s*<PackageReference[^>]*.*?)(</ItemGroup>)'
        if ($content -match $pattern) {
          $content = $content -replace $pattern, "`$1$newPackages`n  `$2"
        }
      }
      else {
        # 创建新的 PackageReference ItemGroup
        $newItemGroup = @"

  <ItemGroup>
    <PackageReference Include="xunit.v3" Version="2.0.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.1" />
  </ItemGroup>
"@
        $content = $content -replace '(</Project>)', "$newItemGroup`$1"
      }
    }
        
    if (-not $DryRun) {
      # 保存更新后的项目文件
      Set-Content -Path $projectPath -Value $content -Encoding UTF8
      Write-Success "项目包引用修复完成: $projectPath"
    }
    else {
      Write-Info "DryRun 模式 - 将要添加的包引用:"
      Write-Info "- xunit.v3 Version 2.0.3"
      Write-Info "- xunit.runner.visualstudio Version 3.1.1"
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
  Write-Info "=== xUnit v3 包引用修复工具 ==="
  Write-Info "当前工作目录: $PWD"
    
  if ($DryRun) {
    Write-Warning "运行在 DryRun 模式，不会实际修改文件"
  }
    
  $projects = Get-TestProjects
  $needsFixProjects = @()
    
  Write-Info "检查需要修复的项目..."
  foreach ($project in $projects) {
    if (Test-NeedsPackageFix $project.FullName) {
      $needsFixProjects += $project
    }
  }
    
  if ($needsFixProjects.Count -eq 0) {
    Write-Success "没有找到需要修复的项目！"
    return
  }
    
  Write-Info "找到 $($needsFixProjects.Count) 个需要修复的项目:"
  foreach ($project in $needsFixProjects) {
    Write-Info "- $($project.Name)"
  }
    
  Write-Info ""
  Write-Info "开始修复..."
    
  $successCount = 0
  $failCount = 0
    
  foreach ($project in $needsFixProjects) {
    if (Fix-ProjectPackages $project.FullName) {
      $successCount++
    }
    else {
      $failCount++
    }
  }
    
  Write-Info "=== 修复完成 ==="
  Write-Success "成功: $successCount 个项目"
  if ($failCount -gt 0) {
    Write-Error "失败: $failCount 个项目"
  }
    
  if (-not $DryRun -and $successCount -gt 0) {
    Write-Info ""
    Write-Info "后续步骤:"
    Write-Info "1. 运行 'dotnet restore' 恢复包引用"
    Write-Info "2. 运行 'dotnet build' 验证编译"
    Write-Info "3. 测试各个项目的运行情况"
  }
}

# 执行主函数
Main 