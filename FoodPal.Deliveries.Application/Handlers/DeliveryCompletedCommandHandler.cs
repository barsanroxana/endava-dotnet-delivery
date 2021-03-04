using FluentValidation;
using FoodPal.Deliveries.Application.Commands;
using FoodPal.Deliveries.Application.Extensions;
using FoodPal.Deliveries.Common.Enum;
using FoodPal.Deliveries.Data.Abstractions;
using FoodPal.Deliveries.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Deliveries.Application.Handlers
{
    public class DeliveryCompletedCommandHandler : IRequestHandler<DeliveryCompletedCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<DeliveryCompletedCommand> _validator;

        public DeliveryCompletedCommandHandler(IUnitOfWork unitOfWork, IValidator<DeliveryCompletedCommand> validator)
        {
            this._unitOfWork = unitOfWork;
            this._validator = validator;
        }

        public async Task<bool> Handle(DeliveryCompletedCommand request, CancellationToken cancellationToken)
        {
            this._validator.ValidateAndThrowEx(request);

            // save to db
            var deliveryModel = await this._unitOfWork.GetRepository<Delivery>().FindByIdAsync(request.Id);
            deliveryModel.Status = DeliveryStatusEnum.Completed;
            deliveryModel.ModifiedAt = DateTimeOffset.Now;
            deliveryModel.ModifiedBy = "system";
            var saved = await this._unitOfWork.SaveChangesAsnyc();

            return saved;
        }
    }
}
