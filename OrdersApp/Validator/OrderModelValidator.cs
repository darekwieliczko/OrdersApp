using FluentValidation;
using OrdersApp.Constans;
using OrdersApp.Models;

namespace OrdersApp.Validator;

public class OrderModelValidator : AbstractValidator<OrderModel>
{
    public OrderModelValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage(Messages.ERROR_PRODUCTNAME_EMPTY)
            .MaximumLength(30).WithMessage(Messages.ERROR_PRODUCTNAME_TOOLONG)
            .MinimumLength(3).WithMessage(Messages.ERROR_PRODUCTNAME_TOOSHORT);

        RuleFor(x => x.DeliveryAddress)
            .NotEmpty().WithMessage(Messages.ERROR_ADRESS_EMPTY)
            .MinimumLength(5).WithMessage(Messages.ERROR_ADRESS_TOOSHORT)
            .MaximumLength(50).WithMessage(Messages.ERROR_ADRESS_TOOLONG);

        RuleFor(x => x.ClientType).IsInEnum().WithMessage(Messages.ERROR_CLIENTTYPE);
        RuleFor(x => x.PaymentType).IsInEnum().WithMessage(Messages.ERROR_PAYMENT);
        RuleFor(x => x.Status).IsInEnum().WithMessage(Messages.ERROR_STATUS);

        RuleFor(x => x.Price).GreaterThan(0).WithMessage(Messages.ERROR_PRICE);
    }

}
