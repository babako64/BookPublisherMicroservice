using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBusRabbitMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace EventBusRabbitMQ.Producer
{
    public class EventBusRabbitMQProducer
    {

        private readonly IRabbitMQConnection _rabbitMqConnection;

        public EventBusRabbitMQProducer(IRabbitMQConnection rabbitMqConnection)
        {
            _rabbitMqConnection = rabbitMqConnection;
        }

        public void PublishCartCheckout(string queueName, CartCheckoutEvent publishModel)
        {
            using (var channel = _rabbitMqConnection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false,
                    arguments: null);
                var message = JsonConvert.SerializeObject(publishModel);
                var body = Encoding.UTF8.GetBytes(message);

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: false, properties, body: body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, EventArg) =>
                {
                    Console.WriteLine("Send RabbitMQ");
                };
                channel.WaitForConfirms();
            }
        }
    }
}
