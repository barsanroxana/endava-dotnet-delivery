namespace FoodPal.Deliveries.Api.Dto
{
    public class HttpErrorResponseDto
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public string Details { get; set; }
    }
}
