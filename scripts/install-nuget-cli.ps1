# NuGet CLI 自动安装脚本
# 解决 "nuget 命令未找到" 的问题

param(
  [switch]$Force = $false
)

Write-Host "=== NuGet CLI 安装工具 ===" -ForegroundColor Cyan

# 检查是否已安装 NuGet CLI
function Test-NuGetInstalled {
  try {
    $null = Get-Command nuget -ErrorAction Stop
    $version = & nuget | Select-String "NuGet Version:" | ForEach-Object { $_.ToString().Split(':')[1].Trim() }
    Write-Host "✅ NuGet CLI 已安装，版本: $version" -ForegroundColor Green
    return $true
  }
  catch {
    Write-Host "❌ NuGet CLI 未安装" -ForegroundColor Red
    return $false
  }
}

# 安装 NuGet CLI
function Install-NuGetCLI {
  Write-Host "🔄 正在安装 NuGet CLI..." -ForegroundColor Yellow
    
  try {
    # 方法1: 使用 winget (推荐)
    if (Get-Command winget -ErrorAction SilentlyContinue) {
      Write-Host "使用 winget 安装 NuGet CLI..." -ForegroundColor Cyan
      & winget install Microsoft.NuGet --silent
            
      if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ NuGet CLI 安装成功" -ForegroundColor Green
        return $true
      }
    }
        
    # 方法2: 使用 Chocolatey
    if (Get-Command choco -ErrorAction SilentlyContinue) {
      Write-Host "使用 Chocolatey 安装 NuGet CLI..." -ForegroundColor Cyan
      & choco install nuget.commandline -y
            
      if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ NuGet CLI 安装成功" -ForegroundColor Green
        return $true
      }
    }
        
    # 方法3: 直接下载
    Write-Host "直接下载 NuGet CLI..." -ForegroundColor Cyan
    $nugetPath = "$env:LOCALAPPDATA\NuGet"
    $nugetExe = "$nugetPath\nuget.exe"
        
    if (-not (Test-Path $nugetPath)) {
      New-Item -Path $nugetPath -ItemType Directory -Force | Out-Null
    }
        
    $downloadUrl = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
    Invoke-WebRequest -Uri $downloadUrl -OutFile $nugetExe
        
    # 添加到 PATH
    $currentPath = [Environment]::GetEnvironmentVariable("PATH", "User")
    if ($currentPath -notlike "*$nugetPath*") {
      [Environment]::SetEnvironmentVariable("PATH", "$currentPath;$nugetPath", "User")
      Write-Host "✅ 已添加到用户 PATH 环境变量" -ForegroundColor Green
    }
        
    Write-Host "✅ NuGet CLI 下载完成" -ForegroundColor Green
    return $true
  }
  catch {
    Write-Host "❌ 安装失败: $($_.Exception.Message)" -ForegroundColor Red
    return $false
  }
}

# 刷新环境变量
function Update-EnvironmentPath {
  Write-Host "🔄 刷新环境变量..." -ForegroundColor Yellow
  $env:PATH = [System.Environment]::GetEnvironmentVariable("PATH", "Machine") + ";" + [System.Environment]::GetEnvironmentVariable("PATH", "User")
  Write-Host "✅ 环境变量已刷新" -ForegroundColor Green
}

# 主程序
if (Test-NuGetInstalled -and -not $Force) {
  Write-Host "NuGet CLI 已经安装，无需重复安装。" -ForegroundColor Green
  Write-Host "如需强制重新安装，请使用 -Force 参数。" -ForegroundColor Yellow
  exit 0
}

if ($Force -or -not (Test-NuGetInstalled)) {
  if (Install-NuGetCLI) {
    Update-EnvironmentPath
        
    # 验证安装
    Start-Sleep -Seconds 2
    if (Test-NuGetInstalled) {
      Write-Host "`n🎉 NuGet CLI 安装完成！" -ForegroundColor Green
      Write-Host "现在可以使用以下命令测试:" -ForegroundColor Cyan
      Write-Host "  nuget" -ForegroundColor White
      Write-Host "  .\scripts\nuget-release-v2.ps1 -DryRun" -ForegroundColor White
    }
    else {
      Write-Host "`n⚠️ 安装完成，但可能需要重启终端才能使用 nuget 命令" -ForegroundColor Yellow
      Write-Host "请关闭当前终端并重新打开，或者运行以下命令:" -ForegroundColor Cyan
      Write-Host "  `$env:PATH = [System.Environment]::GetEnvironmentVariable('PATH','Machine') + ';' + [System.Environment]::GetEnvironmentVariable('PATH','User')" -ForegroundColor White
    }
  }
  else {
    Write-Host "`n❌ 安装失败，请手动安装 NuGet CLI" -ForegroundColor Red
    Write-Host "手动安装方法:" -ForegroundColor Cyan
    Write-Host "1. 访问: https://www.nuget.org/downloads" -ForegroundColor White
    Write-Host "2. 下载 nuget.exe" -ForegroundColor White
    Write-Host "3. 将 nuget.exe 添加到 PATH 环境变量" -ForegroundColor White
    exit 1
  }
}

Write-Host "`n📋 后续步骤:" -ForegroundColor Cyan
Write-Host "1. 设置 NuGet API Key: `$env:NUGET_API_KEY = 'your-api-key'" -ForegroundColor White
Write-Host "2. 测试发布脚本: .\scripts\nuget-release-v2.ps1 -DryRun" -ForegroundColor White
Write-Host "3. 查看完整文档: .\scripts\README-nuget-release.md" -ForegroundColor White 