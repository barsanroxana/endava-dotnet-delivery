using FoodPal.Contracts;
using FoodPal.Deliveries.Dto;
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

        public DeliveryController(ILogger<DeliveryController> logger, IPublishEndpoint publishEndpoint)
        {
            this._logger = logger;
            this._publishEndpoint = publishEndpoint;
        } 

        [HttpPost]
        public async Task<IActionResult> CreateDelivery(DeliveryDto deliveryDto)
        { 
            await this._publishEndpoint.Publish<INewDeliveryAddedEvent>(deliveryDto);

            return Accepted();
        }
    }
}
