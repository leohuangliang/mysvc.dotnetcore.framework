# NuGet 包健康检查工具
# 检查 MySvc.Framework 包的健康状态：依赖关系、安全漏洞、过时版本等

param(
  [string]$ConfigFile = "src\nuget\nuget-config.json",
  [switch]$CheckSecurity = $false,
  [switch]$CheckDependencies = $true,
  [switch]$CheckOutdated = $true,
  [string]$OutputFormat = "Table" # Table, Json, Csv
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

# 检查包的健康状态
function Get-PackageHealth($packageName) {
  try {
    # 获取包的基本信息
    $metadataUrl = "https://api.nuget.org/v3/registration5-semver1/$($packageName.ToLower())/index.json"
    $response = Invoke-RestMethod -Uri $metadataUrl -ErrorAction Stop
        
    $latestItem = $response.items[-1].items[-1]
    $catalogEntry = $latestItem.catalogEntry
        
    # 分析依赖关系
    $dependencies = @()
    $dependencyIssues = @()
        
    if ($catalogEntry.dependencyGroups) {
      foreach ($depGroup in $catalogEntry.dependencyGroups) {
        if ($depGroup.dependencies) {
          foreach ($dep in $depGroup.dependencies) {
            $dependencies += @{
              Name      = $dep.id
              Version   = $dep.range
              Framework = $depGroup.targetFramework
            }
                        
            # 检查是否有版本范围问题
            if ($dep.range -match '\[.*,.*\)' -or $dep.range -match '\(.*,.*\]') {
              $dependencyIssues += "依赖 $($dep.id) 使用了开放版本范围: $($dep.range)"
            }
          }
        }
      }
    }
        
    # 检查版本发布频率
    $allVersions = @()
    foreach ($item in $response.items) {
      foreach ($versionItem in $item.items) {
        $allVersions += @{
          Version      = $versionItem.catalogEntry.version
          Published    = $versionItem.catalogEntry.published
          IsPrerelease = $versionItem.catalogEntry.version -match '-'
        }
      }
    }
        
    $allVersions = $allVersions | Sort-Object Published
        
    # 计算发布间隔
    $releaseIntervals = @()
    for ($i = 1; $i -lt $allVersions.Count; $i++) {
      $interval = ([DateTime]$allVersions[$i].Published) - ([DateTime]$allVersions[$i - 1].Published)
      $releaseIntervals += $interval.TotalDays
    }
        
    $avgReleaseInterval = if ($releaseIntervals.Count -gt 0) { 
      [math]::Round(($releaseIntervals | Measure-Object -Average).Average, 1) 
    }
    else { 0 }
        
    # 检查最后发布时间
    $lastPublished = [DateTime]$catalogEntry.published
    $daysSinceLastRelease = ([DateTime]::Now - $lastPublished).TotalDays
        
    # 健康评分计算
    $healthScore = 100
    $healthIssues = @()
        
    # 依赖关系检查
    if ($dependencies.Count -gt 20) {
      $healthScore -= 10
      $healthIssues += "依赖过多 ($($dependencies.Count) 个)"
    }
        
    if ($dependencyIssues.Count -gt 0) {
      $healthScore -= 15
      $healthIssues += $dependencyIssues
    }
        
    # 发布频率检查
    if ($daysSinceLastRelease -gt 365) {
      $healthScore -= 20
      $healthIssues += "超过1年未更新"
    }
    elseif ($daysSinceLastRelease -gt 180) {
      $healthScore -= 10
      $healthIssues += "超过6个月未更新"
    }
        
    # 预发布版本比例检查
    $prereleaseCount = ($allVersions | Where-Object { $_.IsPrerelease }).Count
    $stableCount = ($allVersions | Where-Object { -not $_.IsPrerelease }).Count
    $prereleaseRatio = if ($allVersions.Count -gt 0) { $prereleaseCount / $allVersions.Count } else { 0 }
        
    if ($prereleaseRatio -gt 0.7) {
      $healthScore -= 15
      $healthIssues += "预发布版本比例过高 ($([math]::Round($prereleaseRatio * 100, 1))%)"
    }
        
    # 确定健康等级
    $healthLevel = switch ($healthScore) {
      { $_ -ge 90 } { "优秀" }
      { $_ -ge 80 } { "良好" }
      { $_ -ge 70 } { "一般" }
      { $_ -ge 60 } { "需要关注" }
      default { "有问题" }
    }
        
    return @{
      PackageName            = $packageName
      HealthScore            = $healthScore
      HealthLevel            = $healthLevel
      HealthIssues           = $healthIssues
      LatestVersion          = $catalogEntry.version
      LastPublished          = $catalogEntry.published
      DaysSinceLastRelease   = [math]::Round($daysSinceLastRelease, 0)
      TotalVersions          = $allVersions.Count
      StableVersions         = $stableCount
      PrereleaseVersions     = $prereleaseCount
      PrereleaseRatio        = [math]::Round($prereleaseRatio * 100, 1)
      DependencyCount        = $dependencies.Count
      Dependencies           = $dependencies
      AverageReleaseInterval = $avgReleaseInterval
      IsHealthy              = $healthScore -ge 70
      LastChecked            = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    }
  }
  catch {
    if ($_.Exception.Response.StatusCode -eq 404) {
      return @{
        PackageName  = $packageName
        HealthScore  = 0
        HealthLevel  = "未发布"
        HealthIssues = @("包未发布或不存在")
        IsHealthy    = $false
        Error        = "包未发布或不存在"
        LastChecked  = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
      }
    }
    else {
      return @{
        PackageName  = $packageName
        HealthScore  = 0
        HealthLevel  = "检查失败"
        HealthIssues = @("检查失败: $($_.Exception.Message)")
        IsHealthy    = $false
        Error        = $_.Exception.Message
        LastChecked  = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
      }
    }
  }
}

# 生成健康报告
function New-HealthReport($results) {
  $healthyPackages = ($results | Where-Object { $_.IsHealthy }).Count
  $totalPackages = $results.Count
  $avgHealthScore = if ($totalPackages -gt 0) { 
    [math]::Round(($results | Measure-Object -Property HealthScore -Average).Average, 1) 
  }
  else { 0 }
    
  # 按健康评分排序
  $sortedPackages = $results | Sort-Object HealthScore -Descending
    
  # 统计问题
  $allIssues = @()
  foreach ($pkg in $results) {
    if ($pkg.HealthIssues) {
      foreach ($issue in $pkg.HealthIssues) {
        $allIssues += @{
          Package     = $pkg.PackageName
          Issue       = $issue
          HealthScore = $pkg.HealthScore
        }
      }
    }
  }
    
  # 问题分类统计
  $issueStats = @{}
  foreach ($issue in $allIssues) {
    $key = $issue.Issue -replace '\d+', 'X' -replace '\(.*\)', '(X)'
    if ($issueStats.ContainsKey($key)) {
      $issueStats[$key]++
    }
    else {
      $issueStats[$key] = 1
    }
  }
    
  $report = @{
    GeneratedAt      = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Summary          = @{
      TotalPackages      = $totalPackages
      HealthyPackages    = $healthyPackages
      UnhealthyPackages  = $totalPackages - $healthyPackages
      HealthyRate        = if ($totalPackages -gt 0) { [math]::Round(($healthyPackages / $totalPackages) * 100, 1) } else { 0 }
      AverageHealthScore = $avgHealthScore
      TotalIssues        = $allIssues.Count
    }
    PackagesByHealth = $sortedPackages
    CommonIssues     = $issueStats
    AllIssues        = $allIssues
  }
    
  return $report
}

# 主程序开始
Write-Info "=== MySvc.Framework NuGet 包健康检查工具 ==="

# 加载配置
$config = Get-NuGetConfig $ConfigFile
if (-not $config) {
  exit 1
}

Write-Info "包数量: $($config.packages.Count)"
Write-Info "检查项目: 依赖关系、发布频率、版本分布"

# 检查所有包的健康状态
Write-Info "`n🏥 正在进行健康检查..."
$results = @()
$i = 0

foreach ($package in $config.packages) {
  $i++
  $progress = [math]::Round(($i / $config.packages.Count) * 100, 1)
  Write-Progress -Activity "包健康检查" -Status "检查 $($package.packageName) ($i/$($config.packages.Count))" -PercentComplete $progress
    
  $health = Get-PackageHealth $package.packageName
  $results += $health
    
  # 显示实时状态
  if ($health.Error) {
    Write-Error "  ❌ $($package.packageName) - $($health.Error)"
  }
  else {
    $statusIcon = switch ($health.HealthLevel) {
      "优秀" { "🟢" }
      "良好" { "🟡" }
      "一般" { "🟠" }
      "需要关注" { "🔴" }
      "有问题" { "💀" }
      default { "❓" }
    }
    Write-Info "  $statusIcon $($package.packageName) - $($health.HealthLevel) ($($health.HealthScore)分)"
  }
}

Write-Progress -Activity "包健康检查" -Completed

# 生成健康报告
$report = New-HealthReport $results

# 显示摘要
Write-Info "`n📊 健康检查摘要:"
Write-Info "检查时间: $($report.GeneratedAt)"
Write-Info "总包数: $($report.Summary.TotalPackages)"
Write-Success "健康包数: $($report.Summary.HealthyPackages) ($($report.Summary.HealthyRate)%)"
if ($report.Summary.UnhealthyPackages -gt 0) {
  Write-Warning "不健康包数: $($report.Summary.UnhealthyPackages)"
}
Write-Info "平均健康评分: $($report.Summary.AverageHealthScore)"
Write-Warning "发现问题: $($report.Summary.TotalIssues) 个"

# 显示健康等级分布
Write-Info "`n🏆 健康等级分布:"
$healthLevels = $results | Group-Object HealthLevel | Sort-Object Count -Descending
foreach ($level in $healthLevels) {
  $icon = switch ($level.Name) {
    "优秀" { "🟢" }
    "良好" { "🟡" }
    "一般" { "🟠" }
    "需要关注" { "🔴" }
    "有问题" { "💀" }
    default { "❓" }
  }
  Write-Info "$icon $($level.Name): $($level.Count) 个包"
}

# 显示详细健康状态
Write-Info "`n📋 详细健康状态:"
if ($OutputFormat -eq "Table") {
  $displayResults = $results | Select-Object PackageName, HealthScore, HealthLevel, DaysSinceLastRelease, TotalVersions, DependencyCount, PrereleaseRatio | Sort-Object HealthScore -Descending
  $displayResults | Format-Table -AutoSize
}
else {
  switch ($OutputFormat.ToLower()) {
    "json" {
      $results | ConvertTo-Json -Depth 5
    }
    "csv" {
      $results | ConvertTo-Csv -NoTypeInformation
    }
  }
}

# 显示常见问题
if ($report.CommonIssues.Count -gt 0) {
  Write-Warning "`n⚠️ 常见问题统计:"
  $sortedIssues = $report.CommonIssues.GetEnumerator() | Sort-Object Value -Descending
  foreach ($issue in $sortedIssues) {
    Write-Warning "  - $($issue.Key): $($issue.Value) 个包"
  }
}

# 显示需要关注的包
$problematicPackages = $results | Where-Object { -not $_.IsHealthy } | Sort-Object HealthScore
if ($problematicPackages.Count -gt 0) {
  Write-Error "`n🚨 需要关注的包:"
  foreach ($pkg in $problematicPackages) {
    Write-Error "  💀 $($pkg.PackageName) - $($pkg.HealthLevel) ($($pkg.HealthScore)分)"
    if ($pkg.HealthIssues) {
      foreach ($issue in $pkg.HealthIssues) {
        Write-Error "     - $issue"
      }
    }
  }
}

# 提供改进建议
Write-Info "`n💡 改进建议:"

# 长期未更新的包
$outdatedPackages = $results | Where-Object { $_.DaysSinceLastRelease -gt 180 } | Sort-Object DaysSinceLastRelease -Descending
if ($outdatedPackages.Count -gt 0) {
  Write-Warning "以下包需要更新:"
  foreach ($pkg in $outdatedPackages | Select-Object -First 5) {
    Write-Warning "  - $($pkg.PackageName): $($pkg.DaysSinceLastRelease) 天未更新"
  }
}

# 依赖过多的包
$heavyDependencyPackages = $results | Where-Object { $_.DependencyCount -gt 15 } | Sort-Object DependencyCount -Descending
if ($heavyDependencyPackages.Count -gt 0) {
  Write-Info "以下包依赖较多，可考虑优化:"
  foreach ($pkg in $heavyDependencyPackages | Select-Object -First 3) {
    Write-Info "  - $($pkg.PackageName): $($pkg.DependencyCount) 个依赖"
  }
}

# 预发布版本过多的包
$prereleaseHeavyPackages = $results | Where-Object { $_.PrereleaseRatio -gt 50 } | Sort-Object PrereleaseRatio -Descending
if ($prereleaseHeavyPackages.Count -gt 0) {
  Write-Info "以下包可考虑发布更多稳定版本:"
  foreach ($pkg in $prereleaseHeavyPackages | Select-Object -First 3) {
    Write-Info "  - $($pkg.PackageName): $($pkg.PrereleaseRatio)% 预发布版本"
  }
}

# 保存详细报告
$reportPath = "nuget-health-report-$(Get-Date -Format 'yyyyMMdd-HHmmss').json"
$report | ConvertTo-Json -Depth 10 | Set-Content $reportPath -Encoding UTF8
Write-Info "`n📄 详细健康报告已保存: $reportPath"

# 返回状态码
if ($report.Summary.UnhealthyPackages -gt 0) {
  Write-Warning "`n⚠️ 发现不健康的包，请查看详细报告"
  exit 1
}
else {
  Write-Success "`n🎉 所有包健康状态良好！"
  exit 0
} 