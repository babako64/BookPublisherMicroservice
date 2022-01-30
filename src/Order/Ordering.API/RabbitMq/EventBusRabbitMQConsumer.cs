using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ordering.API.RabbitMq
{
    public class EventBusRabbitMQConsumer
    {

        private readonly IRabbitMQConnection _connection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;


        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IMediator mediator, IMapper mapper, IOrderRepository repository)
        {
            _connection = connection;
            _mediator = mediator;
            _mapper = mapper;
            _repository = repository;
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.CartCheckoutQueue, durable: false, exclusive: false,
                autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.CartCheckoutQueue, autoAck: true, consumer: consumer);

        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.CartCheckoutQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var basketCheckoutEvent = JsonConvert.DeserializeObject<CartCheckoutEvent>(message);

                var command = _mapper.Map<CheckoutOrderCommand>(basketCheckoutEvent);
                var result = await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }

    }
}
