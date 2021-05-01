set LatestVersion=5.0.0-beta1

nuget setapikey oy2dmiwvfxlulcnah5uacl64xz3hegiepd3u6vvzfrumeu -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\PayPalCoreSDK\MySvc.DotNetCore.PayPalCoreSDK.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
nuget.exe push nuget-packages\PayPalMerchantSDK\MySvc.DotNetCore.PayPalMerchantSDK.%LatestVersion%.nupkg  -Source https://api.nuget.org/v3/index.json
