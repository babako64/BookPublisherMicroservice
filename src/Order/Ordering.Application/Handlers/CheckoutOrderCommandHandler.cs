using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Mappers;
using Ordering.Application.Response;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, OrderResponse>
    {

        private readonly IOrderRepository _orderRepository;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = OrderMapper.Mapper.Map<Order>(request);
            if (orderEntity == null)
            {
                throw new ApplicationException("not mapped");
            }

            var newOrder = await _orderRepository.AddAsync(orderEntity);

            var orderResponse = OrderMapper.Mapper.Map<OrderResponse>(newOrder);
            return orderResponse;
        }
    }
}
