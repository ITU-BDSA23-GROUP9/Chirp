using System.Collections;
using System.Data.Common;
using System.Formats.Tar;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text.Json;
using DocoptNet;
using Utilities;

public static class WebServiceClient 
{
    public static async Task<IEnumerable<Cheep>?> Get() 
    {
        var baseURL = "http://localhost:5222";
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.BaseAddress = new Uri(baseURL);

        return await client.GetFromJsonAsync<IEnumerable<Cheep>>("cheeps");      
    }

    public static async Task Post(Cheep cheep) {
        var baseURL = "http://localhost:5222";
        using HttpClient client = new();
        client.BaseAddress = new Uri(baseURL);
        HttpResponseMessage response = await client.PostAsJsonAsync("/cheep", cheep);
    }
}
