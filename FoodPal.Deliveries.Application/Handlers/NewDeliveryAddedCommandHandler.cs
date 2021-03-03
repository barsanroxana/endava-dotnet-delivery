using AutoMapper;
using FluentValidation;
using FoodPal.Deliveries.Application.Commands;
using FoodPal.Deliveries.Application.Extensions;
using FoodPal.Deliveries.Data.Abstractions;
using FoodPal.Deliveries.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Deliveries.Application.Handlers
{
    public class NewDeliveryAddedCommandHandler : IRequestHandler<NewDeliveryAddedCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<NewDeliveryAddedCommand> _validator;

        public NewDeliveryAddedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<NewDeliveryAddedCommand> validator)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validator = validator;
        }

        public async Task<bool> Handle(NewDeliveryAddedCommand request, CancellationToken cancellationToken)
        {
            var deliveryModel = this._mapper.Map<Delivery>(request);

            this._validator.ValidateAndThrowEx(request);

            // save to db
            this._unitOfWork.GetRepository<Delivery>().Create(deliveryModel);
            return await this._unitOfWork.SaveChangesAsnyc();
        }
    }
}
