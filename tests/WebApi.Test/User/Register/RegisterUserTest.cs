using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommomTestsUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.Test.User.Register;

public class RegisterUserTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public RegisterUserTest(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async void Success()
    {
        // var httpClient = new HttpClient();
        // httpClient.BaseAddress;
        var request = RequestRegisteredUserJsonBuild.Build();
        //pega o body, convert pra docment,e pega o root da data


        var response = await _httpClient.PostAsJsonAsync("User", request);
        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);
        var data = responseData.RootElement.GetProperty("name");

        Assert.NotNull(responseData);
        Assert.NotEmpty(data.ToString());
        Assert.Equal(data.ToString(), request.Nome);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

    }
}
