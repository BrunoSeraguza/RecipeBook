using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommomTestsUtilities.Requests;
using MyRecipeBook.Exceptions;
using WebApi.Test.InlineData;

namespace WebApi.Test.User.Register;

public class RegisterUserTest : IClassFixture< CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public RegisterUserTest(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task  Success()
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

    [Theory]
    [ClassData(typeof(CultureInlineData))]
    public async Task Error_Empty_Name(string culture)
    {
        var request = RequestRegisteredUserJsonBuild.Build();

        request.Nome = string.Empty;

        if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
            _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

        _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);

        var response = await _httpClient.PostAsJsonAsync("User", request);

        Assert.True(response.StatusCode.Equals(HttpStatusCode.BadRequest));

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);
        var errors = responseData.RootElement.GetProperty("erros").EnumerateArray();
        var expectedMessage = ResourceExceptionsMessage.ResourceManager.GetString("NOME_VAZIO", new CultureInfo(culture));
        Assert.Single(errors);
        Assert.Equal(errors.FirstOrDefault().ToString(), expectedMessage);

    }
}
