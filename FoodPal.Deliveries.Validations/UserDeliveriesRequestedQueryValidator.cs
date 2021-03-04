using FluentValidation;
using FoodPal.Deliveries.Application.Queries;

namespace FoodPal.Deliveries.Validations
{
    public class UserDeliveriesRequestedQueryValidator : InternalValidator<UserDeliveriesRequestedQuery>
    {
        public UserDeliveriesRequestedQueryValidator()
        {
            this.RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
