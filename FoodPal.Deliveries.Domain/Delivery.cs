using FoodPal.Deliveries.Common.Enum;
using System;

namespace FoodPal.Deliveries.Domain
{
    public class Delivery : IEntity
    {
        public int Id { get; set; }
        public DeliveryStatusEnum Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int OrderId { get; set; }
        public string CreateBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public string Info { get; set; }
    }
}
