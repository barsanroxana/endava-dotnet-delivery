using FoodPal.Contracts;
using FoodPal.Deliveries.Application.Commands;
using FoodPal.Deliveries.Domain;

namespace FoodPal.Deliveries.Mappers
{
    class DeliveryMapper : InternalProfile
    {
        public DeliveryMapper()
        {
            this.CreateMap<INewDeliveryAddedEvent, NewDeliveryAddedCommand>();
            this.CreateMap<NewDeliveryAddedCommand, Delivery>();
        }

    }
}
