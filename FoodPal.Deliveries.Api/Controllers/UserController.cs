﻿using FoodPal.Contracts;
using FoodPal.Deliveries.Dto;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FoodPal.Deliveries.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {  
        private readonly ILogger<UserController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserController(ILogger<UserController> logger, IPublishEndpoint publishEndpoint)
        {
            this._logger = logger;
            this._publishEndpoint = publishEndpoint;
        } 

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        { 
            await this._publishEndpoint.Publish<INewUserAddedEvent>(userDto);

            return Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {
            await this._publishEndpoint.Publish<IUserUpdatedEvent>(userDto);

            return Accepted();
        }
    }
}
