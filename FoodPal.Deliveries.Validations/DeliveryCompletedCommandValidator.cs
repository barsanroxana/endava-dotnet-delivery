using FluentValidation;
using FoodPal.Deliveries.Application.Commands;

namespace FoodPal.Deliveries.Validations
{
    public class DeliveryCompletedCommandValidator : InternalValidator<DeliveryCompletedCommand>
    {
        public DeliveryCompletedCommandValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
        } 
    }
}
