using System.Net.Http;
using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json;

namespace TestWebApiClient
{
    public class ClientController:Controller
    {
        public IActionResult Call()
        {
            // Obtains the actual URL to request the token from the instance of Identity Server.
            // // By default, it is < server-URL >/ connect/ token.
            var disco = DiscoveryClient.GetAsync("http://localhost:6000").Result; 
            // Attempts to get an access token to call the web API. ID and secret of
            // // the client application to use must be provided.
            var tokenClient = new TokenClient(disco.TokenEndpoint, "bal323", "bal323-secret");
            
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("test-API").Result;
            if (tokenResponse.IsError)
            {
                return Json("Error: " + tokenResponse.Error);
            } 
            var http = new HttpClient(); 
            http.SetBearerToken( tokenResponse.AccessToken); 
            var response = http.GetAsync("http://localhost:7000/test/5").Result;
            return Json(new {Answer = response.Content.ToString(), Token = tokenResponse.AccessToken});

        }
    }
}