using FluentValidation;
using FoodPal.Deliveries.Application.Commands;

namespace FoodPal.Deliveries.Validations
{
    public class NewDeliveryAddedCommandValidator : InternalValidator<NewDeliveryAddedCommand>
    {
        public NewDeliveryAddedCommandValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
            this.RuleFor(x => x.UserId).NotEmpty();
            this.RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
