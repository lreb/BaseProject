using BaseProjectAPI.Service.Items.Commands;
using BaseProjectAPI.Service.Items.Queries;
using FluentValidation;

namespace BaseProjectAPI.Service.Items.Validations
{
    public class ItemCommandValidator : AbstractValidator<GetItemByIdQuery>
    {
        public ItemCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage($"{nameof(ItemCommandValidator)} {nameof(GetItemByIdQuery.Id)} cannot be null");
            RuleFor(p => p.Id).GreaterThan(0).WithMessage($"{nameof(ItemCommandValidator)} {nameof(GetItemByIdQuery.Id)} must be greater than 0");
        }
    }

    public class ItemCreateCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public ItemCreateCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage($"{nameof(ItemCreateCommandValidator)} {nameof(CreateItemCommand.Name)} cannot be empty");
            RuleFor(p => p.Quantity).GreaterThan(0).WithMessage($"{nameof(ItemCreateCommandValidator)} {nameof(CreateItemCommand.Quantity)} must be greater than 0");
        }
    }
}
