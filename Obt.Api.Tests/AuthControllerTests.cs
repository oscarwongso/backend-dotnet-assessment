using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Obt.Api.Store;

namespace Obt.Api.Tests;

public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private static readonly JsonSerializerOptions Json = new() { PropertyNameCaseInsensitive = true };

    public AuthControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task LoginRejectsBadPassword()
    {
        var res = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            email = InMemoryStore.AdminEmail,
            password = "wrong"
        });
        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }

    [Fact]
    public async Task LoginAndMe()
    {
        var login = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            email = InMemoryStore.SalesOneEmail,
            password = InMemoryStore.DemoPassword
        });
        Assert.Equal(HttpStatusCode.OK, login.StatusCode);
        var body = await login.Content.ReadFromJsonAsync<JsonElement>(Json);
        Assert.Equal("sales", body.GetProperty("user").GetProperty("role").GetString());
        var token = body.GetProperty("token").GetString();
        Assert.False(string.IsNullOrWhiteSpace(token));

        using var meReq = new HttpRequestMessage(HttpMethod.Get, "/api/auth/me");
        meReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var me = await _client.SendAsync(meReq);
        Assert.Equal(HttpStatusCode.OK, me.StatusCode);
        var meBody = await me.Content.ReadFromJsonAsync<JsonElement>(Json);
        Assert.Equal(InMemoryStore.SalesOneEmail, meBody.GetProperty("user").GetProperty("email").GetString());
    }

    [Fact]
    public async Task CustomersRequireAuth()
    {
        var res = await _client.GetAsync("/api/master/customers");
        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }

    [Fact]
    public async Task CustomersAndContractsWithToken()
    {
        var login = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            email = InMemoryStore.SalesOneEmail,
            password = InMemoryStore.DemoPassword
        });
        var token = (await login.Content.ReadFromJsonAsync<JsonElement>(Json)).GetProperty("token").GetString();

        using var customersReq = new HttpRequestMessage(HttpMethod.Get, "/api/master/customers");
        customersReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var customers = await _client.SendAsync(customersReq);
        Assert.Equal(HttpStatusCode.OK, customers.StatusCode);
        var list = await customers.Content.ReadFromJsonAsync<JsonElement>(Json);
        Assert.True(list[0].TryGetProperty("sapId", out _));

        using var contractsReq = new HttpRequestMessage(HttpMethod.Get, "/api/master/contracts");
        contractsReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var contracts = await _client.SendAsync(contractsReq);
        Assert.Equal(HttpStatusCode.OK, contracts.StatusCode);
        var clist = await contracts.Content.ReadFromJsonAsync<JsonElement>(Json);
        Assert.Equal("1200012345", clist[0].GetProperty("contractRef").GetString());
        Assert.Equal(1, clist.GetArrayLength());
    }
}
