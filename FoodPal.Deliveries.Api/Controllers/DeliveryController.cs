using FoodPal.Contracts;
using FoodPal.Deliveries.Api.Exceptions;
using FoodPal.Deliveries.Dto;
using FoodPal.Deliveries.Dto.Intern;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FoodPal.Deliveries.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : ControllerBase
    {  
        private readonly ILogger<DeliveryController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<IUserDeliveriesRequested> _requestClientUserDeliveriesRequested;

        public DeliveryController(ILogger<DeliveryController> logger, IPublishEndpoint publishEndpoint, IRequestClient<IUserDeliveriesRequested> requestClientUserDeliveriesRequested)
        {
            this._logger = logger;
            this._publishEndpoint = publishEndpoint;
            this._requestClientUserDeliveriesRequested = requestClientUserDeliveriesRequested;
        } 

        [HttpPost]
        public async Task<IActionResult> CreateDelivery(DeliveryDto deliveryDto)
        { 
            await this._publishEndpoint.Publish<INewDeliveryAddedEvent>(deliveryDto);

            ///nu am stiut exact care este medota corecta de implementare, am observat ca avand aceleas nume la interfetele din Deliveries.Contract cu
            ///Notification.Contract, Publish-ul scrie pe aceasi coada. Astfel eu am facut publish aici la IDeliveryAddedEvent si s-a apelat metoda
            ///din DeliveryAddedConsumer din FoodPal.Notifications.


            await this._publishEndpoint.Publish<IDeliveryAddedEvent>(
                new { Title = "Delivery status",
                    Message = "Your delivery was created!",
                    UserId = deliveryDto.UserId,
                    CreateBy = "",
                    ModifiedBy = "",
                    Type = 2,
                    Info = ""
                });

            return Accepted();
        }

        [Route("completed/{id}")]
        [HttpPatch]
        public async Task<IActionResult> CompletedDelivery(int id)
        {
            await this._publishEndpoint.Publish<IDeliveriesCompletedEvent>(new
            {
                Id = id
            });

            //Todo: cred ca aici pentru userId ar trebui creat un serviciu care sa caute in DB
            await this._publishEndpoint.Publish<IDeliveryCompletedEvent>(
               new
               {
                   Title = "Delivery status",
                   Message = "Your delivery was completed!",
                   UserId = 1,
                   CreateBy = "",
                   ModifiedBy = "",
                   Type = 2,
                   Info = ""
               });

            return Accepted();
        }

        [Route("{userId}/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetDelivery(int userId, int id)
        {
            var response = await this._requestClientUserDeliveriesRequested.GetResponse<DeliveryDto, InternalErrorResponseDto>(new
            {
                UserId = userId,
                Id = id
            });

            if (response.Is<InternalErrorResponseDto>(out var respError))
            {
                throw new HttpResponseException(respError.Message);
            }

            return Ok(response.Message);
        }
    }
}
