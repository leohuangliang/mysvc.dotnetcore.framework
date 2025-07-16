#!/usr/bin/env pwsh

<#
.SYNOPSIS
    紧急修复脚本 - 恢复语法错误
.DESCRIPTION
    修复由于警告修复脚本导致的语法错误
#>

Write-Host "🚨 开始紧急修复语法错误..." -ForegroundColor Red

# 修复 LoggerConfigurationEmailExtensions.cs
$filePath = ".\src\Infrastructure.Logging.Serilog\LoggerConfigurationEmailExtensions.cs"
Write-Host "修复: $filePath"

$content = Get-Content $filePath -Raw
$content = $content -replace 'IFormatProvider formatProvider\? = null', 'IFormatProvider? formatProvider = null'
$content = $content -replace 'ICredentialsByHost networkCredential\? = null', 'ICredentialsByHost? networkCredential = null'
Set-Content $filePath $content -Encoding UTF8

# 修复 Rfc6238AuthenticationService.cs
$filePath = ".\src\Infrastructure.Crosscutting\Helpers\Rfc6238AuthenticationService.cs"
Write-Host "修复: $filePath"

$content = Get-Content $filePath -Raw
$content = $content -replace 'string secretKey\? = null', 'string? secretKey = null'
Set-Content $filePath $content -Encoding UTF8

# 修复 IJobSchedule.cs
$filePath = ".\src\Infrastructure.Crosscutting\Jobs\IJobSchedule.cs"
Write-Host "修复: $filePath"

$content = Get-Content $filePath -Raw
$content = $content -replace 'string cronExpression\? = null', 'string? cronExpression = null'
$content = $content -replace 'TimeZoneInfo timeZone\? = null', 'TimeZoneInfo? timeZone = null'
Set-Content $filePath $content -Encoding UTF8

Write-Host "✅ 紧急修复完成!" -ForegroundColor Green 