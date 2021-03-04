using FoodPal.Deliveries.Common.Enums;

namespace FoodPal.Deliveries.Dto.Intern
{
    public class InternalErrorResponseDto
    {
        public string Message { get; set; }
        public string Details { get; set; }
        public InternalErrorResponseTypeEnum Type { get; set; }
    }
}
