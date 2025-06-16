# 清理重复包引用脚本
# 移除项目中重复的 PackageReference

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

# 清理单个项目的重复包引用
function Clean-DuplicatePackages($projectPath) {
  Write-Info "正在检查项目: $($projectPath)"
    
  try {
    $content = Get-Content $projectPath -Raw
    $originalContent = $content
    $hasChanges = $false
        
    # 移除旧版本的 xunit.runner.visualstudio
    $oldRunnerPattern = '<PackageReference Include="xunit\.runner\.visualstudio" Version="2\.[^"]*"[^>]*>'
    if ($content -match $oldRunnerPattern) {
      Write-Info "发现旧版本的 xunit.runner.visualstudio，正在移除..."
      $content = $content -replace $oldRunnerPattern, ''
      $hasChanges = $true
    }
        
    # 移除旧版本的 xunit 包
    $oldXunitPattern = '<PackageReference Include="xunit" Version="[^"]*"[^>]*>'
    if ($content -match $oldXunitPattern) {
      Write-Info "发现旧版本的 xunit 包，正在移除..."
      $content = $content -replace $oldXunitPattern, ''
      $hasChanges = $true
    }
        
    # 移除 Microsoft.NET.Test.Sdk
    $testSdkPattern = '<PackageReference Include="Microsoft\.NET\.Test\.Sdk"[^>]*>'
    if ($content -match $testSdkPattern) {
      Write-Info "发现 Microsoft.NET.Test.Sdk，正在移除..."
      $content = $content -replace $testSdkPattern, ''
      $hasChanges = $true
    }
        
    # 清理空行
    $content = $content -replace '(\r?\n\s*){3,}', "`r`n`r`n"
        
    if ($hasChanges) {
      if (-not $DryRun) {
        Set-Content -Path $projectPath -Value $content -Encoding UTF8
        Write-Success "项目清理完成: $projectPath"
      }
      else {
        Write-Info "DryRun 模式 - 将要移除重复的包引用"
      }
      return $true
    }
    else {
      Write-Info "项目无需清理: $projectPath"
      return $false
    }
  }
  catch {
    Write-Error "清理项目失败: $projectPath"
    Write-Error $_.Exception.Message
    return $false
  }
}

# 主函数
function Main {
  Write-Info "=== 重复包引用清理工具 ==="
  Write-Info "当前工作目录: $PWD"
    
  if ($DryRun) {
    Write-Warning "运行在 DryRun 模式，不会实际修改文件"
  }
    
  $projects = Get-TestProjects
    
  Write-Info "找到 $($projects.Count) 个测试项目"
  Write-Info "开始清理重复包引用..."
    
  $cleanedCount = 0
  $skippedCount = 0
    
  foreach ($project in $projects) {
    if (Clean-DuplicatePackages $project.FullName) {
      $cleanedCount++
    }
    else {
      $skippedCount++
    }
  }
    
  Write-Info "=== 清理完成 ==="
  Write-Success "已清理: $cleanedCount 个项目"
  Write-Info "跳过: $skippedCount 个项目"
    
  if (-not $DryRun -and $cleanedCount -gt 0) {
    Write-Info ""
    Write-Info "后续步骤:"
    Write-Info "1. 运行 'dotnet restore' 重新恢复包引用"
    Write-Info "2. 运行 'dotnet build' 验证编译"
  }
}

# 执行主函数
Main 