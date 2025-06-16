# 🚨 安全清理指南 - NuGet API Key 泄露处理

## ⚠️ 发现的安全问题

在 `src/nuget/publish.bat` 文件中发现了明文存储的 NuGet API Key：
```
oy2pawbh2yyx7yl2ly2qiy7i4lor7kdunpsnkuu75wsw4i
```

这是一个**严重的安全风险**，需要立即处理！

## 🚨 紧急处理步骤

### 1. 立即撤销 API Key
```
1. 访问 https://www.nuget.org/account/apikeys
2. 登录你的 NuGet 账户
3. 找到泄露的 API Key (oy2pawbh...)
4. 点击 "Delete" 立即撤销
```

### 2. 生成新的 API Key
```
1. 在同一页面点击 "Create"
2. 设置合适的权限范围
3. 复制新的 API Key
4. 设置环境变量：$env:NUGET_API_KEY = "new-api-key"
```

### 3. 清理本地文件
```powershell
# 预览清理操作
.\scripts\cleanup-legacy-nuget.ps1 -DryRun

# 执行清理（会先备份）
.\scripts\cleanup-legacy-nuget.ps1 -Force
```

### 4. 清理 Git 历史记录

#### 方法一：使用 git filter-branch（推荐）
```bash
# 备份当前分支
git branch backup-before-cleanup

# 从历史记录中移除敏感文件
git filter-branch --force --index-filter \
  'git rm --cached --ignore-unmatch src/nuget/publish.bat' \
  --prune-empty --tag-name-filter cat -- --all

# 清理引用
git for-each-ref --format='delete %(refname)' refs/original | git update-ref --stdin
git reflog expire --expire=now --all
git gc --prune=now --aggressive
```

#### 方法二：使用 BFG Repo-Cleaner（更快）
```bash
# 下载 BFG
# https://rtyley.github.io/bfg-repo-cleaner/

# 清理文件
java -jar bfg.jar --delete-files publish.bat

# 清理历史
git reflog expire --expire=now --all && git gc --prune=now --aggressive
```

### 5. 强制推送更新远程仓库
```bash
# ⚠️ 警告：这会重写 Git 历史，影响所有协作者
git push origin --force --all
git push origin --force --tags
```

## 📋 验证清理结果

### 检查本地文件
```powershell
# 确认敏感文件已删除
Test-Path "src\nuget\publish.bat"  # 应该返回 False

# 确认备份文件存在
Get-ChildItem "src\nuget\*.backup*"
```

### 检查 Git 历史
```bash
# 搜索历史记录中的 API Key
git log --all --full-history -- src/nuget/publish.bat

# 搜索提交内容中的 API Key 片段
git log --all -S "oy2pawbh" --source --all
```

### 检查远程仓库
```bash
# 确认远程仓库已更新
git log --oneline origin/main | head -10
```

## 🔐 安全最佳实践

### 1. 环境变量管理
```powershell
# 设置临时环境变量
$env:NUGET_API_KEY = "your-new-api-key"

# 设置永久环境变量（用户级别）
[Environment]::SetEnvironmentVariable("NUGET_API_KEY", "your-new-api-key", "User")

# 验证设置
echo $env:NUGET_API_KEY
```

### 2. .gitignore 配置
确保以下内容在 `.gitignore` 中：
```
# NuGet 敏感文件
*.bat
*apikey*
*secret*
*password*

# 环境配置
.env
.env.local
```

### 3. 使用新版发布脚本
```powershell
# 使用安全的新版脚本
.\scripts\nuget-release-v2.ps1 -Version "8.0.0-beta6" -DryRun
```

## 📊 团队通知模板

发送给团队成员的通知：

```
主题：紧急 - NuGet API Key 安全事件处理

团队成员，

我们发现并处理了一个安全问题：NuGet API Key 在代码中泄露。

已采取的措施：
✅ 撤销了泄露的 API Key
✅ 生成了新的 API Key
✅ 清理了本地敏感文件
✅ 重写了 Git 历史记录

需要你们配合的操作：
1. 立即拉取最新代码：git pull origin main --force
2. 删除本地的旧分支：git branch -D backup-before-cleanup
3. 如果有本地修改，请重新应用到新的历史上

新的发布流程：
- 不再使用 src/nuget/publish.bat
- 使用新脚本：.\scripts\nuget-release-v2.ps1
- API Key 通过环境变量管理

如有疑问，请联系我。

谢谢配合！
```

## 🔍 监控和预防

### 1. 设置 Git Hooks
创建 `.git/hooks/pre-commit`：
```bash
#!/bin/bash
# 检查是否包含敏感信息
if git diff --cached --name-only | xargs grep -l "setapikey\|apikey.*=" 2>/dev/null; then
    echo "❌ 检测到可能的 API Key，提交被阻止"
    exit 1
fi
```

### 2. 定期安全扫描
```powershell
# 扫描当前代码中的敏感信息
Get-ChildItem -Recurse -Include "*.bat","*.ps1","*.json" | 
    Select-String -Pattern "apikey|password|secret" -CaseSensitive:$false
```

### 3. 使用 GitHub Secret Scanning
- 启用 GitHub 的 Secret Scanning 功能
- 配置自定义模式检测 NuGet API Key

## ✅ 清理完成检查清单

- [ ] 撤销了泄露的 NuGet API Key
- [ ] 生成了新的 API Key
- [ ] 设置了环境变量
- [ ] 清理了本地敏感文件
- [ ] 重写了 Git 历史记录
- [ ] 强制推送了更新
- [ ] 通知了团队成员
- [ ] 验证了清理结果
- [ ] 设置了预防措施

---

**重要提醒**：安全无小事，发现类似问题请立即处理！ 