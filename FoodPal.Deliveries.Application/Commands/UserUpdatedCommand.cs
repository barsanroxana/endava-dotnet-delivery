﻿using MediatR;

namespace FoodPal.Deliveries.Application.Commands
{
    public class UserUpdatedCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
    }
}
