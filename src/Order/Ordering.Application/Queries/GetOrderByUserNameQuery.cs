﻿using System.Collections.Generic;
using MediatR;
using Ordering.Application.Response;

namespace Ordering.Application.Queries
{
    public class GetOrderByUserNameQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public string UserName { get; set; }

        public GetOrderByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
