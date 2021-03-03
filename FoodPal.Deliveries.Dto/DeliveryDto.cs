using FoodPal.Deliveries.Common.Enum;

namespace FoodPal.Deliveries.Dto
{
    public class DeliveryDto
    {
        public int Id { get; set; }
        public DeliveryStatusEnum Status { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string CreateBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Info { get; set; }
    }
}
