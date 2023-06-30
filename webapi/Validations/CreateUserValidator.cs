using FluentValidation;
using webapi.Dto.Users;
using webapi.Models;

namespace webapi.Validations
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator(AppDbContext appDbContext)
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(u => u.Password).MinimumLength(6);
            RuleFor(u => u.ConfirmPassword).Equal(e => e.Password);
            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    var emialInUse = appDbContext.User.Any( u => u.Email == value);
                    if (emialInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
        }
    }
}
