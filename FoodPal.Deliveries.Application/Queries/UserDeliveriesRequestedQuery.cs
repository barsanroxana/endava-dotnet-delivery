using FoodPal.Deliveries.Dto;
using MediatR;

namespace FoodPal.Deliveries.Application.Queries
{
    public class UserDeliveriesRequestedQuery : IRequest<DeliveriesDto>
    {
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
