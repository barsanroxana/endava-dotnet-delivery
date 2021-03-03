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
    public class UserUpdatedCommandHandler : IRequestHandler<UserUpdatedCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UserUpdatedCommand> _validator;

        public UserUpdatedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UserUpdatedCommand> validator)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validator = validator;
        }

        public async Task<bool> Handle(UserUpdatedCommand request, CancellationToken cancellationToken)
        {
            var userModel = this._mapper.Map<User>(request);

            this._validator.ValidateAndThrowEx(request);

            // save to db
            this._unitOfWork.GetRepository<User>().Update(userModel);
            return await this._unitOfWork.SaveChangesAsnyc();
        }
    }
}
