using Mapster;
using MyRecipeBook.Application.Services.EncryptPassword;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Users;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Users.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserReadOnlyRepository _readOnlyUserRepository;
        private readonly IUserWriteOnlyRepository _writeOnlyUserRepository;
        private readonly EncryptPassword _encryptPassword;

        public RegisterUserUseCase(IUserReadOnlyRepository readOnlyUserRepository, IUserWriteOnlyRepository writeOnlyUserRepository, EncryptPassword encryptPassword)
        {
            _readOnlyUserRepository = readOnlyUserRepository;
            _encryptPassword = encryptPassword;
            _writeOnlyUserRepository = writeOnlyUserRepository;           
        }

        public  async Task<ResponseRegisteredUserJson> Execute(RequestRegisteredUserJson request)
        {
            User userMapper = request.Adapt<Domain.Entities.User>();
            Validate(request);

            //customizar o password na classe dependencyInjection dps
            //var user = new Domain.Entities.User
            //{
            //    Name = request.Name,
            //};
          
            userMapper.Password = _encryptPassword.Encrypt(request.Password);

            await _writeOnlyUserRepository.Add(userMapper);
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
