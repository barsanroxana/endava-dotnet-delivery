using FluentValidation;
using FoodPal.Deliveries.Application.Commands;

namespace FoodPal.Deliveries.Validations
{
    public class UserUpdatedCommandValidator : InternalValidator<UserUpdatedCommand>
    {
        public UserUpdatedCommandValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
            this.RuleFor(x => x.Email).NotEmpty();
            this.RuleFor(x => x.FirstName).NotEmpty();
            this.RuleFor(x => x.LastName).NotEmpty();
            this.RuleFor(x => x.PhoneNo).NotEmpty();
            this.RuleFor(x => x.Address).NotEmpty();
        }
    }
}
