using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.Users.Register;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson),StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisteredUserJson request)
    {
            RegisterUserUseCase useCase = new();
            //manda para application validar a regra de negocio
            var response = useCase.Execute(request);

            return Created(string.Empty,response);              
    }

}
