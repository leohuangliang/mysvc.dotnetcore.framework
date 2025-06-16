# 简化版过时字符串扩展方法迁移脚本

param(
    [switch]$DryRun = $false
)

Write-Host "=== 过时字符串扩展方法迁移工具 ===" -ForegroundColor Cyan
Write-Host "当前工作目录: $PWD" -ForegroundColor Cyan

if ($DryRun) {
    Write-Host "运行在 DryRun 模式，不会实际修改文件" -ForegroundColor Yellow
}

# 获取所有需要处理的 .cs 文件（排除 bin/obj 目录）
$files = Get-ChildItem -Path . -Recurse -Filter "*.cs" | Where-Object { 
    $_.FullName -notmatch "\\(bin|obj)\\" 
}

$targetFiles = @()
$totalChanges = 0

Write-Host "`n扫描包含过时方法的文件..." -ForegroundColor Cyan

foreach ($file in $files) {
    try {
        $content = Get-Content $file.FullName -Raw
        if ($content -match '\.IsNullOrBlank\(' -or $content -match '\.NotNullOrBlank\(') {
            $targetFiles += $file
        }
    }
    catch {
        Write-Host "跳过文件: $($file.FullName)" -ForegroundColor Yellow
    }
}

if ($targetFiles.Count -eq 0) {
    Write-Host "🎉 没有找到使用过时方法的文件！" -ForegroundColor Green
    exit 0
}

Write-Host "找到 $($targetFiles.Count) 个需要迁移的文件`n" -ForegroundColor Cyan

foreach ($file in $targetFiles) {
    Write-Host "正在处理: $($file.Name)" -ForegroundColor White
    
    try {
        $content = Get-Content $file.FullName -Raw
        $originalContent = $content
        $fileChanges = 0
        
        # 替换 .IsNullOrBlank() 为 string.IsNullOrWhiteSpace()
        $pattern1 = '(\w+)\.IsNullOrBlank\(\)'
        $matches1 = [regex]::Matches($content, $pattern1)
        if ($matches1.Count -gt 0) {
            $content = $content -replace $pattern1, 'string.IsNullOrWhiteSpace($1)'
            $fileChanges += $matches1.Count
            Write-Host "  - 替换 $($matches1.Count) 个 .IsNullOrBlank() 调用" -ForegroundColor Gray
        }
        
        # 替换 .NotNullOrBlank() 为 !string.IsNullOrWhiteSpace()
        $pattern2 = '(\w+)\.NotNullOrBlank\(\)'
        $matches2 = [regex]::Matches($originalContent, $pattern2)
        if ($matches2.Count -gt 0) {
            $content = $content -replace $pattern2, '!string.IsNullOrWhiteSpace($1)'
            $fileChanges += $matches2.Count
            Write-Host "  - 替换 $($matches2.Count) 个 .NotNullOrBlank() 调用" -ForegroundColor Gray
        }
        
        if ($fileChanges -gt 0) {
            if (-not $DryRun) {
                Set-Content -Path $file.FullName -Value $content -Encoding UTF8
                Write-Host "  ✅ 文件已更新，共 $fileChanges 处修改" -ForegroundColor Green
            } else {
                Write-Host "  📋 DryRun 模式 - 将要进行 $fileChanges 处修改" -ForegroundColor Yellow
            }
            $totalChanges += $fileChanges
        } else {
            Write-Host "  ⏭️  文件无需修改" -ForegroundColor Gray
        }
    }
    catch {
        Write-Host "  ❌ 处理文件失败: $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host "`n=== 迁移完成 ===" -ForegroundColor Cyan
Write-Host "处理文件总数: $($targetFiles.Count)" -ForegroundColor Green
Write-Host "总修改次数: $totalChanges" -ForegroundColor Green

Write-Host "`n=== 迁移指南 ===" -ForegroundColor Cyan
Write-Host "已完成的替换:" -ForegroundColor White
Write-Host "  • variable.IsNullOrBlank() → string.IsNullOrWhiteSpace(variable)" -ForegroundColor White
Write-Host "  • variable.NotNullOrBlank() → !string.IsNullOrWhiteSpace(variable)" -ForegroundColor White

if (-not $DryRun -and $totalChanges -gt 0) {
    Write-Host "`n建议运行以下命令验证修改:" -ForegroundColor Yellow
    Write-Host "dotnet build" -ForegroundColor White
    Write-Host "dotnet test" -ForegroundColor White
} 