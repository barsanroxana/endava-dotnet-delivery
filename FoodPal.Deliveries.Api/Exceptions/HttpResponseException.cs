using FoodPal.Deliveries.Dto.Intern;
using System;

namespace FoodPal.Deliveries.Api.Exceptions
{
    public class HttpResponseException : Exception
    {
        public InternalErrorResponseDto ExceptionDto { get; private set; }

        public HttpResponseException(InternalErrorResponseDto internalResponseDto)
        {
            this.ExceptionDto = internalResponseDto;   
        }
    }
}
