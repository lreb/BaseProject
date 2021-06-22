using BaseProjectAPI.Service.Users.Queries;
using FluentValidation;

namespace BaseProjectAPI.Service.Users
{
    public class UserCommandValidation : AbstractValidator<AuthenticateQuery>
    {
        public UserCommandValidation()
        {
            RuleFor(p=>p.Email).NotEmpty().WithMessage($"{nameof(UserCommandValidation)} {nameof(AuthenticateQuery.Email)} cannot be null");
            RuleFor(p => p.Password).NotEmpty().WithMessage($"{nameof(UserCommandValidation)} {nameof(AuthenticateQuery.Password)} cannot be null");
        }
    }
}
