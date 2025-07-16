#!/usr/bin/env pwsh

<#
.SYNOPSIS
    批量修复MySvc.Framework项目中的XML注释警告
.DESCRIPTION
    自动为公共成员添加XML注释，解决CS1591警告
.PARAMETER ProjectPath
    项目根目录路径
.PARAMETER DryRun
    预览模式，不实际修改文件
#>

param(
  [Parameter(Mandatory = $false)]
  [string]$ProjectPath = ".",
    
  [Parameter(Mandatory = $false)]
  [switch]$DryRun
)

Write-Host "🔧 开始修复XML注释警告..." -ForegroundColor Green

# Currency枚举值XML注释映射
$currencyComments = @{
  'AED' = '阿联酋迪尔汗'
  'AFN' = '阿富汗尼'
  'ALL' = '阿尔巴尼亚列克'
  'AMD' = '亚美尼亚德拉姆'
  'ANG' = '荷属安的列斯盾'
  'AOA' = '宽扎'
  'ARS' = '阿根廷比索'
  'AUD' = '澳大利亚元'
  'AWG' = '阿鲁巴弗洛林'
  'AZN' = '阿塞拜疆马纳特'
  'BAM' = '波斯尼亚和黑塞哥维那可兑换马克'
  'BBD' = '巴巴多斯元'
  'BDT' = '孟加拉塔卡'
  'BGN' = '保加利亚列弗'
  'BHD' = '巴林第纳尔'
  'BIF' = '布隆迪法郎'
  'BMD' = '百慕大元'
  'BND' = '文莱元'
  'BOB' = '玻利维亚诺'
  'BOV' = '玻利维亚资金'
  'BRL' = '巴西雷亚尔'
  'BSD' = '巴哈马元'
  'BTN' = '不丹努扎姆'
  'BWP' = '博茨瓦纳普拉'
  'BYR' = '白俄罗斯卢布'
  'BZD' = '伯利兹元'
  'CAD' = '加拿大元'
  'CDF' = '刚果法郎'
  'CHE' = 'WIR欧元'
  'CHF' = '瑞士法郎'
  'CHW' = 'WIR法郎'
  'CLF' = '智利发展单位'
  'CLP' = '智利比索'
  'CNY' = '人民币'
  'CNH' = '人民币（离岸）'
  'COP' = '哥伦比亚比索'
  'COU' = '货币英雄联盟(UVR)(基金代码)'
  'CRC' = '哥斯达黎加科朗'
  'CUC' = '古巴可兑换比索'
  'CUP' = '古巴比索'
  'CVE' = '佛得角埃斯库多'
  'CZK' = '捷克克朗'
  'DJF' = '吉布提法郎'
  'DKK' = '丹麦克朗'
  'DOP' = '多米尼加比索'
  'DZD' = '阿尔及利亚第纳尔'
  'EGP' = '埃及磅'
  'ERN' = '厄立特里亚纳克法'
  'ETB' = '埃塞俄比亚比尔'
  'EUR' = '欧元'
  'FJD' = '斐济元'
  'FKP' = '福克兰镑'
  'GBP' = '英镑'
  'GEL' = '格鲁吉亚拉里'
  'GHS' = '加纳塞地'
  'GIP' = '直布罗陀镑'
  'GMD' = '冈比亚达拉西'
  'GNF' = '几内亚法郎'
  'GTQ' = '危地马拉格查尔'
  'GYD' = '圭亚那元'
  'HKD' = '港币'
  'HNL' = '洪都拉斯伦皮拉'
  'HRK' = '克罗地亚库纳'
  'HTG' = '海地古德'
  'HUF' = '匈牙利福林'
  'IDR' = '印尼盾'
  'ILS' = '以色列新谢克尔'
  'INR' = '印度卢比'
  'IQD' = '伊拉克第纳尔'
  'IRR' = '伊朗里亚尔'
  'ISK' = '冰岛克朗'
  'JMD' = '牙买加元'
  'JOD' = '约旦第纳尔'
  'JPY' = '日元'
  'KES' = '肯尼亚先令'
  'KGS' = '吉尔吉斯斯坦索姆'
  'KHR' = '柬埔寨瑞尔'
  'KMF' = '科摩罗法郎'
  'KPW' = '朝鲜元'
  'KRW' = '韩元'
  'KWD' = '科威特第纳尔'
  'KYD' = '开曼群岛元'
  'KZT' = '哈萨克斯坦坚戈'
  'LAK' = '老挝基普'
  'LBP' = '黎巴嫩镑'
  'LKR' = '斯里兰卡卢比'
  'LRD' = '利比里亚元'
  'LSL' = '莱索托洛提'
  'LYD' = '利比亚第纳尔'
  'MAD' = '摩洛哥迪拉姆'
  'MDL' = '摩尔多瓦列伊'
  'MGA' = '马达加斯加阿里亚里'
  'MKD' = '马其顿第纳尔'
  'MMK' = '缅甸元'
  'MNT' = '蒙古图格里克'
  'MOP' = '澳门元'
  'MRO' = '毛里塔尼亚乌吉亚'
  'MUR' = '毛里求斯卢比'
  'MVR' = '马尔代夫拉菲亚'
  'MWK' = '马拉维克瓦查'
  'MXN' = '墨西哥比索'
  'MXV' = '墨西哥倒置基金(UDI)(基金代码)'
  'MYR' = '马来西亚令吉'
  'MZN' = '莫桑比克梅蒂卡尔'
  'NAD' = '纳米比亚元'
  'NGN' = '尼日利亚奈拉'
  'NIO' = '科多巴'
  'NOK' = '挪威克朗'
  'NPR' = '尼泊尔卢比'
  'NZD' = '新西兰元'
  'OMR' = '阿曼里亚尔'
  'PAB' = '巴拿马巴波亚'
  'PEN' = '秘鲁新索尔'
  'PGK' = '巴布亚新几内亚基那'
  'PHP' = '菲律宾比索'
  'PKR' = '巴基斯坦卢比'
  'PLN' = '波兰兹罗提'
  'PYG' = '巴拉圭瓜拉尼'
  'QAR' = '卡塔尔里亚尔'
  'RON' = '罗马尼亚列伊'
  'RSD' = '塞尔维亚第纳尔'
  'RUB' = '俄罗斯卢布'
  'RWF' = '卢旺达法郎'
  'SAR' = '沙特里亚尔'
  'SBD' = '所罗门群岛元'
  'SCR' = '塞舌尔卢比'
  'SDG' = '苏丹镑'
  'SEK' = '瑞典克朗'
  'SGD' = '新加坡元'
  'SHP' = '圣赫勒拿镑'
  'SLL' = '塞拉利昂利昂'
  'SOS' = '索马里先令'
  'SRD' = '苏里南元'
  'SSP' = '南苏丹镑'
  'STD' = '圣多美和普林西比多布拉'
  'SVC' = '萨尔瓦多科朗'
  'SYP' = '叙利亚镑'
  'SZL' = '斯威士兰里兰吉尼'
  'THB' = '泰铢'
  'TJS' = '塔吉克斯坦索莫尼'
  'TMT' = '土库曼斯坦马纳特'
  'TND' = '突尼斯第纳尔'
  'TOP' = '汤加潘加'
  'TRY' = '土耳其里拉'
  'TTD' = '特立尼达多巴哥元'
  'TWD' = '新台币'
  'TZS' = '坦桑尼亚先令'
  'UAH' = '乌克兰格里夫纳'
  'UGX' = '乌干达先令'
  'USD' = '美元'
  'USN' = '美元(次日)(资金代码)'
  'UYI' = '乌拉圭指数比索(URUIURUI)(基金代码)'
  'UYU' = '乌拉圭比索'
  'UZS' = '乌兹别克斯坦苏姆'
  'VEF' = '委内瑞拉玻利瓦'
  'VND' = '越南盾'
  'VUV' = '瓦努阿图瓦图'
  'WST' = '萨摩亚塔拉'
  'XAF' = '中非金融合作法郎'
  'XAG' = '银'
  'XAU' = '金'
  'XBA' = '欧洲复合单位'
  'XBB' = '欧洲货币联盟'
  'XBC' = '9号帐户的欧洲单位'
  'XBD' = '17号帐户的欧洲单位'
  'XCD' = '东加勒比元'
  'XDR' = '国际货币基金组织的特别提款权'
  'XOF' = '多哥非洲共同体法郎'
  'XPD' = '钯(金衡制盎司)'
  'XPF' = '太平洋法郎'
  'XPT' = '铂(金衡制盎司)'
  'XSU' = '区域补偿统一的系统'
  'XTS' = '为测试保留的代码'
  'XUA' = '亚行账户单位'
  'XXX' = '非货币'
  'YER' = '也门里亚尔'
  'ZAR' = '南非兰特'
  'ZMW' = '赞比亚克瓦查'
  'ZWL' = '津巴布韦元'
}

function Add-XmlCommentsToCurrency {
  param([string]$FilePath)
    
  $content = Get-Content $FilePath -Raw
  $modified = $false
    
  foreach ($currency in $currencyComments.Keys) {
    $description = $currencyComments[$currency]
    $pattern = "(\s+)($currency\s*=\s*\d+,?)"
    $replacement = "`$1/// <summary>`n`$1/// $description`n`$1/// </summary>`n`$1`$2"
        
    if ($content -match $pattern -and $content -notmatch "/// <summary>[\s\S]*?/// $description") {
      $content = $content -replace $pattern, $replacement
      $modified = $true
    }
  }
    
  if ($modified) {
    if (-not $DryRun) {
      Set-Content $FilePath $content -NoNewline
      Write-Host "✅ 已修复: $FilePath" -ForegroundColor Green
    }
    else {
      Write-Host "🔍 [预览] 将修复: $FilePath" -ForegroundColor Yellow
    }
  }
}

function Add-XmlCommentsToHangfire {
  param([string]$FilePath)
    
  $content = Get-Content $FilePath -Raw
  $modified = $false
    
  # HangfireJobSchedule类注释
  $classPattern = "(\s+)(public\s+class\s+HangfireJobSchedule)"
  if ($content -match $classPattern -and $content -notmatch "/// <summary>[\s\S]*?Hangfire作业调度服务") {
    $replacement = "`$1/// <summary>`n`$1/// Hangfire作业调度服务，提供延迟、队列和定时任务功能`n`$1/// </summary>`n`$1`$2"
    $content = $content -replace $classPattern, $replacement
    $modified = $true
  }
    
  # 方法注释映射
  $methodComments = @{
    'DelayedJob<TJob, TParam>' = '创建延迟执行的作业'
    'Enqueue<TJob, TParam>'    = '将作业加入执行队列'
    'Recurring<TParam>'        = '创建定时重复执行的作业'
    'Recurring<TJob, TParam>'  = '创建定时重复执行的作业（带时区）'
  }
    
  foreach ($method in $methodComments.Keys) {
    $description = $methodComments[$method]
    $pattern = "(\s+)(public\s+static\s+[\w<>,\s]+\s+$method\s*\()"
    if ($content -match $pattern -and $content -notmatch "/// <summary>[\s\S]*?$description") {
      $replacement = "`$1/// <summary>`n`$1/// $description`n`$1/// </summary>`n`$1`$2"
      $content = $content -replace $pattern, $replacement
      $modified = $true
    }
  }
    
  if ($modified) {
    if (-not $DryRun) {
      Set-Content $FilePath $content -NoNewline
      Write-Host "✅ 已修复: $FilePath" -ForegroundColor Green
    }
    else {
      Write-Host "🔍 [预览] 将修复: $FilePath" -ForegroundColor Yellow
    }
  }
}

function Add-XmlCommentsToOperator {
  param([string]$FilePath)
    
  $content = Get-Content $FilePath -Raw
  $modified = $false
    
  # Operator属性注释
  $propertyComments = @{
    'FullName'    = '操作员全名'
    'DialCode'    = '国际区号'
    'PhoneNumber' = '电话号码'
  }
    
  foreach ($property in $propertyComments.Keys) {
    $description = $propertyComments[$property]
    $pattern = "(\s+)(public\s+string\s+$property\s*{\s*get;\s*set;\s*})"
    if ($content -match $pattern -and $content -notmatch "/// <summary>[\s\S]*?$description") {
      $replacement = "`$1/// <summary>`n`$1/// $description`n`$1/// </summary>`n`$1`$2"
      $content = $content -replace $pattern, $replacement
      $modified = $true
    }
  }
    
  if ($modified) {
    if (-not $DryRun) {
      Set-Content $FilePath $content -NoNewline
      Write-Host "✅ 已修复: $FilePath" -ForegroundColor Green
    }
    else {
      Write-Host "🔍 [预览] 将修复: $FilePath" -ForegroundColor Yellow
    }
  }
}

# 执行修复
$currencyFile = Join-Path $ProjectPath "src\Domain.Core\Models\Currency.cs"
$hangfireFile = Join-Path $ProjectPath "src\Infrastructure.Job.Hangfire\HangfireJobSchedule.cs"
$operatorFile = Join-Path $ProjectPath "src\Domain.Core\Models\Operator.cs"

if (Test-Path $currencyFile) {
  Add-XmlCommentsToCurrency $currencyFile
}

if (Test-Path $hangfireFile) {
  Add-XmlCommentsToHangfire $hangfireFile
}

if (Test-Path $operatorFile) {
  Add-XmlCommentsToOperator $operatorFile
}

Write-Host "🎉 XML注释修复完成!" -ForegroundColor Green

if ($DryRun) {
  Write-Host "💡 这是预览模式，要实际执行请运行: .\scripts\fix-xml-comments.ps1" -ForegroundColor Cyan
} 