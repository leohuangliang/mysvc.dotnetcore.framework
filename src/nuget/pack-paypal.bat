dotnet build -c Release ..\..\PayPal\PayPal.SDK.NET5.sln

nuget pack nuspecs\PayPalCoreSDK.nuspec -OutputDirectory nuget-packages\PayPalCoreSDK
nuget pack nuspecs\PayPalMerchantSDK.nuspec -OutputDirectory nuget-packages\PayPalMerchantSDK