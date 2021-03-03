using FoodPal.Deliveries.Common.Enum;
using MediatR;

namespace FoodPal.Deliveries.Application.Commands 
{
    public class NewDeliveryAddedCommand : IRequest<bool>
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
