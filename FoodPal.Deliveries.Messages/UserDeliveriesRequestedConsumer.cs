using AutoMapper;
using FoodPal.Contracts;
using FoodPal.Deliveries.Application.Queries;
using FoodPal.Deliveries.Common.Enums;
using FoodPal.Deliveries.Dto.Exceptions;
using FoodPal.Deliveries.Dto.Intern;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPal.Deliveries.Messages
{
    public class UserDeliveriesRequestedConsumer : IConsumer<IUserDeliveriesRequested>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<UserDeliveriesRequestedConsumer> _logger;

        public UserDeliveriesRequestedConsumer(IServiceScopeFactory serviceScopeFactory, IMapper mapper, ILogger<UserDeliveriesRequestedConsumer> logger)
        {
            this._serviceScopeFactory = serviceScopeFactory;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task Consume(ConsumeContext<IUserDeliveriesRequested> context)
        {
            try
            {
                var message = context.Message;

                var query = this._mapper.Map<UserDeliveriesRequestedQuery>(message);

                using (var scope = this._serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var response = await mediator.Send(query);

                    await context.RespondAsync(response);
                }
            }
            catch (ValidationsException e)
            {
                var errors = e.Errors.Aggregate((curr, next) => $"{curr}; {next}");

                var internalErrorResponseDto = new InternalErrorResponseDto
                {
                    Message = "Please correct the validations errors and try again",
                    Details = errors,
                    Type = InternalErrorResponseTypeEnum.Validation
                };
                await context.RespondAsync<InternalErrorResponseDto>(internalErrorResponseDto);

                // log ex
                this._logger.LogError(e, errors);
            }
            catch (Exception e)
            {
                var internalErrorResponseDto = new InternalErrorResponseDto
                {
                    Message = $"Something went wrong in {nameof(UserDeliveriesRequestedConsumer)}",
                    Details = "An error occured",
                    Type = InternalErrorResponseTypeEnum.Error
                };
                await context.RespondAsync<InternalErrorResponseDto>(internalErrorResponseDto);

                this._logger.LogError(e, $"Something went wrong in {nameof(UserDeliveriesRequestedConsumer)}");
            }
        }
    }
}
