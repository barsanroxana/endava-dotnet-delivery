using FluentValidation;
using FoodPal.Deliveries.Dto.Exceptions;
using System.Linq;

namespace FoodPal.Deliveries.Application.Extensions
{
    public static class ValidationExtensions 
    {
        public static void ValidateAndThrowEx<T>(this IValidator<T> validator, T o)
        {
            var result = validator.Validate(o);
            if (!result.IsValid)
            {
                throw new ValidationsException(result.Errors.Select(x => x.ErrorMessage).ToList());
            }
        }
    }
}
