# xUnit v3 批量升级脚本
# 用于将现有的 xUnit v2 测试项目升级到 xUnit v3

param(
  [string]$ProjectPath = "",
  [switch]$DryRun = $false,
  [switch]$All = $false
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
      $content = Get-Content $project.FullName -Raw
      # 检查是否是测试项目
      if ($content -match "<IsTestProject>true</IsTestProject>" -or 
        $content -match "xunit" -or 
        $content -match "Microsoft\.NET\.Test\.Sdk") {
        $testProjects += $project
      }
    }
  }
    
  return $testProjects
}

# 检查项目是否已经是 xUnit v3
function Test-IsXUnitV3Project($projectPath) {
  $content = Get-Content $projectPath -Raw
  return $content -match "xunit\.v3" -and $content -match "<OutputType>Exe</OutputType>"
}

# 升级单个项目
function Update-ProjectToXUnitV3($projectPath) {
  Write-Info "正在升级项目: $($projectPath)"
    
  if (Test-IsXUnitV3Project $projectPath) {
    Write-Warning "项目已经是 xUnit v3，跳过: $projectPath"
    return $true
  }
    
  try {
    $content = Get-Content $projectPath -Raw
    $originalContent = $content
        
    # 1. 添加 OutputType=Exe（如果不存在）
    if ($content -notmatch "<OutputType>Exe</OutputType>") {
      $content = $content -replace "(<IsTestProject>true</IsTestProject>)", "`$1`n    <OutputType>Exe</OutputType>"
    }
        
    # 2. 更新包引用
    # 移除旧的 xunit 包
    $content = $content -replace '<PackageReference Include="xunit" Version="[^"]*" />', ''
    $content = $content -replace '<PackageReference Include="xunit\.runner\.visualstudio" Version="[^"]*" />', ''
    $content = $content -replace '<PackageReference Include="Microsoft\.NET\.Test\.Sdk" Version="[^"]*" />', ''
        
    # 添加新的 xUnit v3 包引用
    if ($content -notmatch "xunit\.v3") {
      # 找到第一个 PackageReference ItemGroup 并添加新包
      if ($content -match '(<ItemGroup>\s*<PackageReference[^>]*.*?</ItemGroup>)') {
        $newPackages = @"
    <PackageReference Include="xunit.v3" Version="2.0.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.1" />
"@
        # 在第一个 PackageReference ItemGroup 的末尾添加新包
        $content = $content -replace '(</ItemGroup>)', "$newPackages`n  `$1"
      }
      else {
        # 如果没有 PackageReference ItemGroup，创建一个新的
        $newItemGroup = @"
  <ItemGroup>
    <PackageReference Include="xunit.v3" Version="2.0.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.1" />
  </ItemGroup>
"@
        $content = $content -replace '(</Project>)', "$newItemGroup`n`$1"
      }
    }
        
    # 3. 添加 xunit.runner.json 配置
    if ($content -notmatch "xunit\.runner\.json") {
      $configItem = '    <Content Include="xunit.runner.json" CopyToOutputDirectory="PreserveNewest" />'
      if ($content -match '<ItemGroup>\s*<Content') {
        $content = $content -replace '(<ItemGroup>\s*<Content[^>]*>[^<]*</Content>\s*</ItemGroup>)', "`$1`n  <ItemGroup>`n$configItem`n  </ItemGroup>"
      }
      else {
        $content = $content -replace '(</Project>)', "  <ItemGroup>`n$configItem`n  </ItemGroup>`n`$1"
      }
    }
        
    # 4. 添加 Using 指令（如果不存在）
    if ($content -notmatch '<Using Include="Xunit"') {
      $usingItem = '    <Using Include="Xunit" />'
      if ($content -match '<ItemGroup>\s*<Using') {
        # 已有 Using 组，添加到其中
        $content = $content -replace '(<Using Include="[^"]*" />)', "`$1`n$usingItem"
      }
      else {
        # 创建新的 Using 组
        $content = $content -replace '(</Project>)', "  <ItemGroup>`n$usingItem`n  </ItemGroup>`n`$1"
      }
    }
        
    if (-not $DryRun) {
      # 保存更新后的项目文件
      Set-Content -Path $projectPath -Value $content -Encoding UTF8
            
      # 创建 xunit.runner.json 文件
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
        Write-Success "已创建 xunit.runner.json: $runnerJsonPath"
      }
            
      Write-Success "项目升级完成: $projectPath"
    }
    else {
      Write-Info "DryRun 模式 - 将要进行的更改:"
      Write-Info "- 添加 <OutputType>Exe</OutputType>"
      Write-Info "- 更新包引用到 xUnit v3"
      Write-Info "- 添加 xunit.runner.json 配置"
      Write-Info "- 添加 Using 指令"
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
  Write-Info "=== xUnit v3 批量升级工具 ==="
  Write-Info "当前工作目录: $PWD"
    
  if ($DryRun) {
    Write-Warning "运行在 DryRun 模式，不会实际修改文件"
  }
    
  $projects = @()
    
  if ($ProjectPath) {
    # 升级指定项目
    if (Test-Path $ProjectPath) {
      $projects += Get-Item $ProjectPath
    }
    else {
      Write-Error "指定的项目文件不存在: $ProjectPath"
      return
    }
  }
  elseif ($All) {
    # 升级所有测试项目
    $projects = Get-TestProjects
    Write-Info "找到 $($projects.Count) 个测试项目"
  }
  else {
    # 交互式选择
    $allProjects = Get-TestProjects
    Write-Info "找到以下测试项目:"
    for ($i = 0; $i -lt $allProjects.Count; $i++) {
      $status = if (Test-IsXUnitV3Project $allProjects[$i].FullName) { "[v3]" } else { "[v2]" }
      Write-Info "$($i + 1). $status $($allProjects[$i].Name) - $($allProjects[$i].Directory.Name)"
    }
        
    $selection = Read-Host "请选择要升级的项目 (输入数字，多个用逗号分隔，或 'all' 升级全部)"
        
    if ($selection -eq "all") {
      $projects = $allProjects
    }
    else {
      $indices = $selection -split "," | ForEach-Object { [int]$_.Trim() - 1 }
      $projects = $indices | ForEach-Object { $allProjects[$_] }
    }
  }
    
  if ($projects.Count -eq 0) {
    Write-Warning "没有找到需要升级的项目"
    return
  }
    
  Write-Info "准备升级 $($projects.Count) 个项目..."
    
  $successCount = 0
  $failCount = 0
    
  foreach ($project in $projects) {
    if (Update-ProjectToXUnitV3 $project.FullName) {
      $successCount++
    }
    else {
      $failCount++
    }
  }
    
  Write-Info "=== 升级完成 ==="
  Write-Success "成功: $successCount 个项目"
  if ($failCount -gt 0) {
    Write-Error "失败: $failCount 个项目"
  }
    
  if (-not $DryRun -and $successCount -gt 0) {
    Write-Info ""
    Write-Info "后续步骤:"
    Write-Info "1. 运行 'dotnet restore' 恢复包引用"
    Write-Info "2. 运行 'dotnet build' 验证编译"
    Write-Info "3. 运行 'dotnet run' 测试项目执行"
    Write-Info "4. 在 VS Code 中验证测试发现功能"
  }
}

# 执行主函数
Main 