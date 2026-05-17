using Mapster;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Users.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisteredUserJson request)
        {
            Validate(request);
            //TODO Validar dados
            var user = new Domain.Entities.User
            {
               Name = request.Name,
            };
            //customizar o password na classe dependencyInjection dps
            var userMapper = request.Adapt<Domain.Entities.User>();

            //mapear requiest para uma entidade
            //criptografar senha
            //persistir no banco

            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
            };
        }

        private void Validate(RequestRegisteredUserJson request)
        {
            var validade = new ValidateRegisterUser();
            var response = validade.Validate(request);

            if(!response.IsValid)
            {
                var errorMessage = response.Errors.Select(e => e.ErrorMessage).ToList();
                var errorCode = response.Errors.Select(v => v.ErrorCode);

                throw new ErrorOnValidateException(errorMessage);
                //throw new Exception(errorMessage.FirstOrDefault().ToString());
            }

        }
    }
}
