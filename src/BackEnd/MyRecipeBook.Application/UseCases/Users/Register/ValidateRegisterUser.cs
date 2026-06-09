using FluentValidation;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.Users.Register;

public class ValidateRegisterUser : AbstractValidator<RequestRegisteredUserJson>
{
    public ValidateRegisterUser()
    {
        RuleFor(u => u.Nome).NotEmpty().WithMessage(ResourceExceptionsMessage.NOME_VAZIO);
        RuleFor(u => u.Email).NotEmpty().WithMessage(ResourceExceptionsMessage.EMAIL_VAZIO);
        RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(6);
        When(e => !string.IsNullOrEmpty(e.Email), () =>
        {
            RuleFor(u => u.Email).EmailAddress().WithMessage(ResourceExceptionsMessage.EMAIL_VALIDO);
        });
    }
}
