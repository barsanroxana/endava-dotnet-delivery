using MediatR;

namespace FoodPal.Deliveries.Application.Commands
{
    public class DeliveryCompletedCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
