using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Response;

namespace MyRecipeBook.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson),StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisteredUserJson request)
    {
        return Created();
    }

    [HttpPost]
    public IActionResult RegisterAll()
    {
        return Created();
    }
}
