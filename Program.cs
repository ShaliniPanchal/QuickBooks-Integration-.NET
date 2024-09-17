using System;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Intuit.Ipp.OAuth2PlatformClient;
using System.Net.Http;

namespace QuickBooksIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            // Your QuickBooks API credentials
            string clientId = "YourClientID";
            string clientSecret = "YourClientSecret";
            string redirectUrl = "YourRedirectURL";
            string environment = "sandbox"; // or "production" for live environment
            string realmId = "YourRealmID"; // From your app's OAuth process

            // OAuth2 Access Token
            string accessToken = GetAccessToken(clientId, clientSecret, redirectUrl);

            // Initialize OAuth2 with access token
            var oauthValidator = new OAuth2RequestValidator(accessToken);

            // Create Service Context
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = environment == "sandbox" ? "https://sandbox-quickbooks.api.intuit.com/" : "https://quickbooks.api.intuit.com/";

            // Call QuickBooks API
            QueryService<Customer> customerQueryService = new QueryService<Customer>(serviceContext);
            var customers = customerQueryService.ExecuteIdsQuery("SELECT * FROM Customer");

            // Output Customer Info
            foreach (var customer in customers)
            {
                Console.WriteLine($"Customer: {customer.DisplayName}, Balance: {customer.Balance}");
            }
        }

        // Function to get OAuth2 Access Token
        private static string GetAccessToken(string clientId, string clientSecret, string redirectUrl)
        {
            var client = new OAuth2Client(clientId, clientSecret, redirectUrl, environment: OAuth2Client.SANDBOX);
            var authUrl = client.GetAuthorizationURL(new[] { OAuth2Client.QBO_SCOPE });

            Console.WriteLine("Go to this URL to authorize: " + authUrl);
            Console.WriteLine("Enter the authorization code:");
            string authCode = Console.ReadLine();

            var tokenResponse = client.GetBearerTokenAsync(authCode).Result;
            return tokenResponse.AccessToken;
        }
    }
}
