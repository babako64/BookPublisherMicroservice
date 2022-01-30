using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.API.Entities;
using AutoMapper;
using EventBusRabbitMQ.Events;

namespace Accounting.API.Mapping
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<AccountingCheckout, CartCheckoutEvent>().ReverseMap();
        }
    }
}
