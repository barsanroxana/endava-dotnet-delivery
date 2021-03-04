using AutoMapper;
using FluentValidation;
using FoodPal.Deliveries.Application.Extensions;
using FoodPal.Deliveries.Application.Queries;
using FoodPal.Deliveries.Data.Abstractions;
using FoodPal.Deliveries.Domain;
using FoodPal.Deliveries.Dto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodPal.Deliveries.Application.Handlers
{
    public class UserDeliveriesRequestedQueryHandler : IRequestHandler<UserDeliveriesRequestedQuery, DeliveriesDto>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IValidator<UserDeliveriesRequestedQuery> _validator;
        private readonly IMapper _mapper;

        public UserDeliveriesRequestedQueryHandler(IUnitOfWork unitOfWork, IValidator<UserDeliveriesRequestedQuery> validator, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validator = validator; 
        }

        public async Task<DeliveriesDto> Handle(UserDeliveriesRequestedQuery request, CancellationToken cancellationToken)
        {
            this._validator.ValidateAndThrowEx(request);
            var deliveries = new List<Delivery>();

            if (request.Id != 0)
            {
                 deliveries = this._unitOfWork.GetRepository<Delivery>().Find(x => x.UserId == request.UserId).ToList();
            }
            else
            {
                 deliveries = this._unitOfWork.GetRepository<Delivery>().Find(x => x.UserId == request.UserId && x.Id == request.Id).ToList();
            }

            var deliveriesDtos = this._mapper.Map<List<DeliveryDto>>(deliveries);

            return  new DeliveriesDto
            {
                Deliveries = deliveriesDtos
            };
        }
    }
}
