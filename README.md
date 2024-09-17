# QuickBooks Integration in C#

This is a C# application that demonstrates how to integrate with QuickBooks Online using OAuth 2.0 and QuickBooks Online API to retrieve customer data.

## Prerequisites

1. **QuickBooks Developer Account**: 
   - Register your app at the [Intuit Developer Portal](https://developer.intuit.com/).
   - Obtain your `ClientID`, `ClientSecret`, and `Redirect URL`.

2. **Visual Studio or any C# IDE**: 
   - Ensure you have .NET SDK installed.

3. **NuGet Packages**:
   Install the following NuGet packages to use the QuickBooks SDK:
   ```bash
   Install-Package Intuit.Ipp.Core
   Install-Package Intuit.Ipp.Data
   Install-Package Intuit.Ipp.QueryFilter
   Install-Package Intuit.Ipp.Security
   Install-Package Intuit.Ipp.OAuth2PlatformClient
