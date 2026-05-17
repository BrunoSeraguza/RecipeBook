using Mapster;
using MyRecipeBook.Application.Services.EncryptPassword;
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
            var userMapper = request.Adapt<Domain.Entities.User>();
            var passwordEncrypt = new EncryptPassword();
            Validate(request);

            //customizar o password na classe dependencyInjection dps
            //var user = new Domain.Entities.User
            //{
            //    Name = request.Name,
            //};
          
            userMapper.Password = passwordEncrypt.Encrypt(request.Password);

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
