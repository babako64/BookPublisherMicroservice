using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace EventBusRabbitMQ
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _dispose;

        public RabbitMQConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            if (!IsConnected)
            {
                TryConnect();
            }
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_dispose;
        public bool TryConnect()
        {
            try
            {
                _connection = _connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                Thread.Sleep(2000);
                _connection = _connectionFactory.CreateConnection();
                throw;
            }

            if (IsConnected)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException();

            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
