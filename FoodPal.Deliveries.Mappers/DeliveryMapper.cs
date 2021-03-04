using FoodPal.Contracts;
using FoodPal.Deliveries.Application.Commands;
using FoodPal.Deliveries.Application.Queries;
using FoodPal.Deliveries.Domain;
using FoodPal.Deliveries.Dto;

namespace FoodPal.Deliveries.Mappers
{
    class DeliveryMapper : InternalProfile
    {
        public DeliveryMapper()
        {
            this.CreateMap<INewDeliveryAddedEvent, NewDeliveryAddedCommand>();
            this.CreateMap<NewDeliveryAddedCommand, Delivery>();

            this.CreateMap<IDeliveriesCompletedEvent, DeliveryCompletedCommand>();
            this.CreateMap<DeliveryCompletedCommand, Delivery>();

            this.CreateMap<IUserDeliveriesRequested, UserDeliveriesRequestedQuery>();
            this.CreateMap<Delivery, DeliveryDto>();
        }

    }
}
