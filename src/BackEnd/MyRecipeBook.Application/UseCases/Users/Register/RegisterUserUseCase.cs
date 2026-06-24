using Mapster;
using MyRecipeBook.Application.Services.EncryptPassword;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Response;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.Users;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Users.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserReadOnlyRepository _readOnlyUserRepository;
        private readonly IUserWriteOnlyRepository _writeOnlyUserRepository;
        private readonly EncryptPassword _encryptPassword;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserUseCase(IUserReadOnlyRepository readOnlyUserRepository, IUserWriteOnlyRepository writeOnlyUserRepository, EncryptPassword encryptPassword, IUnitOfWork unitOfWork)
        {
            _readOnlyUserRepository = readOnlyUserRepository;
            _encryptPassword = encryptPassword;
            _writeOnlyUserRepository = writeOnlyUserRepository;  
            _unitOfWork = unitOfWork;
            
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisteredUserJson request)
        {
            //var user = new Domain.Entities.User
            //{
            //    Name = request.Name,
            //};

            User userMapper = request.Adapt<Domain.Entities.User>();
            await  Validate(request);

            userMapper.Password = _encryptPassword.Encrypt(request.Password);

            await _writeOnlyUserRepository.Add(userMapper);
            //persistir no banco
            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = userMapper.Nome,
            };
        }

        private async Task Validate(RequestRegisteredUserJson request)
        {
            var validade = new ValidateRegisterUser();
            var response = validade.Validate(request);

            var emailIsNotValid =  await _readOnlyUserRepository.ExistActiveUserEmail(request.Email);//

            if (emailIsNotValid)
                response.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceExceptionsMessage.EMAIL_EXISTE));

            if (!response.IsValid)
            {
                var errorMessage = response.Errors.Select(e => e.ErrorMessage).ToList();
                var errorCode = response.Errors.Select(v => v.ErrorCode);

                throw new ErrorOnValidateException(errorMessage);
            }

        }
    }
}
