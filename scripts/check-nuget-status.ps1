# NuGet.org 包状态检查工具
# 检查 MySvc.Framework 所有包的发布状态和版本信息

param(
  [string]$Version = "",
  [string]$ConfigFile = "src\nuget\nuget-config.json",
  [switch]$ShowDetails = $false,
  [switch]$CheckLatest = $false,
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

# 检查单个包的状态
function Get-PackageStatus($packageName, $targetVersion = $null) {
  try {
    Write-Verbose "检查包: $packageName"
        
    # 使用 NuGet API 查询包信息
    $apiUrl = "https://api.nuget.org/v3-flatcontainer/$($packageName.ToLower())/index.json"
    $response = Invoke-RestMethod -Uri $apiUrl -ErrorAction Stop
        
    $versions = $response.versions
    $latestVersion = $versions[-1]
        
    $status = @{
      PackageName         = $packageName
      LatestVersion       = $latestVersion
      TotalVersions       = $versions.Count
      AllVersions         = $versions
      IsPublished         = $true
      TargetVersionExists = $false
      TargetVersion       = $targetVersion
      NuGetUrl            = "https://www.nuget.org/packages/$packageName"
      ApiUrl              = $apiUrl
      LastChecked         = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    }
        
    if ($targetVersion) {
      $status.TargetVersionExists = $versions -contains $targetVersion
    }
        
    return $status
  }
  catch {
    if ($_.Exception.Response.StatusCode -eq 404) {
      return @{
        PackageName         = $packageName
        LatestVersion       = "未发布"
        TotalVersions       = 0
        AllVersions         = @()
        IsPublished         = $false
        TargetVersionExists = $false
        TargetVersion       = $targetVersion
        NuGetUrl            = "https://www.nuget.org/packages/$packageName"
        ApiUrl              = $apiUrl
        LastChecked         = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
        Error               = "包未发布或不存在"
      }
    }
    else {
      return @{
        PackageName         = $packageName
        LatestVersion       = "检查失败"
        TotalVersions       = 0
        AllVersions         = @()
        IsPublished         = $false
        TargetVersionExists = $false
        TargetVersion       = $targetVersion
        NuGetUrl            = "https://www.nuget.org/packages/$packageName"
        ApiUrl              = $apiUrl
        LastChecked         = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
        Error               = $_.Exception.Message
      }
    }
  }
}

# 获取包的详细信息
function Get-PackageDetails($packageName) {
  try {
    $metadataUrl = "https://api.nuget.org/v3/registration5-semver1/$($packageName.ToLower())/index.json"
    $response = Invoke-RestMethod -Uri $metadataUrl -ErrorAction Stop
        
    $latestItem = $response.items[-1].items[-1]
    $catalogEntry = $latestItem.catalogEntry
        
    return @{
      Description  = $catalogEntry.description
      Authors      = $catalogEntry.authors
      Published    = $catalogEntry.published
      Downloads    = $latestItem.packageContent
      Dependencies = $catalogEntry.dependencyGroups
      Tags         = $catalogEntry.tags
    }
  }
  catch {
    return @{
      Error = "无法获取详细信息: $($_.Exception.Message)"
    }
  }
}

# 格式化输出
function Format-Results($results, $format) {
  switch ($format.ToLower()) {
    "json" {
      return $results | ConvertTo-Json -Depth 5
    }
    "csv" {
      return $results | ConvertTo-Csv -NoTypeInformation
    }
    default {
      return $results | Format-Table -AutoSize
    }
  }
}

# 生成状态报告
function New-StatusReport($results, $version) {
  $publishedCount = ($results | Where-Object { $_.IsPublished }).Count
  $totalCount = $results.Count
  $targetVersionCount = 0
    
  if ($version) {
    $targetVersionCount = ($results | Where-Object { $_.TargetVersionExists }).Count
  }
    
  $report = @{
    CheckTime     = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    TargetVersion = $version
    Summary       = @{
      TotalPackages       = $totalCount
      PublishedPackages   = $publishedCount
      UnpublishedPackages = $totalCount - $publishedCount
      TargetVersionExists = $targetVersionCount
      SuccessRate         = [math]::Round(($publishedCount / $totalCount) * 100, 2)
    }
    Packages      = $results
  }
    
  if ($version) {
    $report.Summary.TargetVersionRate = [math]::Round(($targetVersionCount / $totalCount) * 100, 2)
  }
    
  return $report
}

# 主程序开始
Write-Info "=== MySvc.Framework NuGet 包状态检查工具 ==="

# 加载配置
$config = Get-NuGetConfig $ConfigFile
if (-not $config) {
  exit 1
}

# 确定检查版本
if ([string]::IsNullOrWhiteSpace($Version)) {
  if ($CheckLatest) {
    Write-Info "检查所有包的最新版本状态"
  }
  else {
    $Version = $config.version
    Write-Info "检查配置文件中的版本: $Version"
  }
}
else {
  Write-Info "检查指定版本: $Version"
}

Write-Info "包数量: $($config.packages.Count)"
Write-Info "输出格式: $OutputFormat"

# 检查所有包的状态
Write-Info "`n🔍 正在检查包状态..."
$results = @()
$i = 0

foreach ($package in $config.packages) {
  $i++
  $progress = [math]::Round(($i / $config.packages.Count) * 100, 1)
  Write-Progress -Activity "检查 NuGet 包状态" -Status "检查 $($package.packageName) ($i/$($config.packages.Count))" -PercentComplete $progress
    
  $status = Get-PackageStatus $package.packageName $Version
    
  if ($ShowDetails -and $status.IsPublished) {
    $details = Get-PackageDetails $package.packageName
    $status.Details = $details
  }
    
  $results += $status
    
  # 显示实时状态
  if ($status.IsPublished) {
    if ($Version -and $status.TargetVersionExists) {
      Write-Success "  ✅ $($package.packageName) - 版本 $Version 已发布"
    }
    elseif ($Version -and -not $status.TargetVersionExists) {
      Write-Warning "  ⚠️ $($package.packageName) - 最新: $($status.LatestVersion), 目标版本 $Version 未找到"
    }
    else {
      Write-Success "  ✅ $($package.packageName) - 最新版本: $($status.LatestVersion)"
    }
  }
  else {
    Write-Error "  ❌ $($package.packageName) - $($status.Error)"
  }
}

Write-Progress -Activity "检查 NuGet 包状态" -Completed

# 生成报告
$report = New-StatusReport $results $Version

# 显示摘要
Write-Info "`n📊 检查结果摘要:"
Write-Info "检查时间: $($report.CheckTime)"
Write-Info "总包数: $($report.Summary.TotalPackages)"
Write-Success "已发布: $($report.Summary.PublishedPackages) ($($report.Summary.SuccessRate)%)"

if ($report.Summary.UnpublishedPackages -gt 0) {
  Write-Error "未发布: $($report.Summary.UnpublishedPackages)"
}

if ($Version) {
  Write-Info "目标版本 $Version 存在: $($report.Summary.TargetVersionExists) ($($report.Summary.TargetVersionRate)%)"
}

# 显示详细结果
Write-Info "`n📋 详细状态:"
if ($OutputFormat -eq "Table") {
  $displayResults = $results | Select-Object PackageName, LatestVersion, TotalVersions, IsPublished, TargetVersionExists, NuGetUrl
  $displayResults | Format-Table -AutoSize
}
else {
  Format-Results $results $OutputFormat
}

# 显示问题包
$problemPackages = $results | Where-Object { -not $_.IsPublished -or ($Version -and -not $_.TargetVersionExists) }
if ($problemPackages.Count -gt 0) {
  Write-Warning "`n⚠️ 需要关注的包:"
  foreach ($pkg in $problemPackages) {
    if (-not $pkg.IsPublished) {
      Write-Error "  ❌ $($pkg.PackageName): 未发布"
    }
    elseif ($Version -and -not $pkg.TargetVersionExists) {
      Write-Warning "  ⚠️ $($pkg.PackageName): 缺少版本 $Version (最新: $($pkg.LatestVersion))"
    }
  }
}

# 保存报告
$reportPath = "nuget-status-report-$(Get-Date -Format 'yyyyMMdd-HHmmss').json"
$report | ConvertTo-Json -Depth 10 | Set-Content $reportPath -Encoding UTF8
Write-Info "`n📄 详细报告已保存: $reportPath"

# 提供有用的链接
Write-Info "`n🔗 有用的链接:"
Write-Info "NuGet.org 搜索: https://www.nuget.org/packages?q=MySvc.Framework"
Write-Info "包管理页面: https://www.nuget.org/account/Packages"

# 返回状态码
if ($report.Summary.UnpublishedPackages -gt 0 -or ($Version -and $report.Summary.TargetVersionExists -lt $report.Summary.TotalPackages)) {
  Write-Warning "`n⚠️ 存在未发布或缺失版本的包"
  exit 1
}
else {
  Write-Success "`n🎉 所有包状态正常！"
  exit 0
} 