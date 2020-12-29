using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVT_AdministratorAPI.AppServices
{
    public class RabbitMQQueueConnection : IQueueConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _disposed;
        private QueueConnectionWorker _worker;

        public RabbitMQQueueConnection(IConnectionFactory connection)
        {
            _connectionFactory = connection;

            if(!IsConnected)
            {
                TryConnect();
            }
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public IModel CreateModel()
        {
            if(!IsConnected)
            {
                throw new InvalidOperationException("RabbitMQ is not available to creata Model");
            }
            return _connection.CreateModel();
        }

        public void Disconnect()
        {
           if(_disposed)
            {
                return;
            }
            Dispose();
        }

        public void InitReceiverChannel()
        {
            if(!IsConnected)
            {
                TryConnect();
            }
            _worker = new QueueConnectionWorker(this, "voteResponse");
            _worker.InitReceiverChannel();   
        }

        public bool TryConnect()
        {
            try
            {
               _connection= _connectionFactory.CreateConnection();
            }
            catch(BrokerUnreachableException e)
            {
                Console.WriteLine("Error while connecting to the RabbitMQ " + e.Message + "\r\n Trying to connect again");
                _connection = _connectionFactory.CreateConnection();
            }

            if(IsConnected)
            {
                _connection.ConnectionBlocked += OnConnectionBlocked;
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
            }

            Console.WriteLine("Connected to the RabbitMQServer" + _connection.Endpoint.HostName);
            return true;
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            Console.WriteLine(e.Detail);
            TryConnect();
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            if (_disposed) return;
            Console.WriteLine(e.Cause);
            TryConnect();
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            Console.WriteLine(e.Reason);
            TryConnect();
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void PublishData(string _queueName,string _content)
        {
            if (!IsConnected)
            {
                TryConnect();
            }
            using (var channel = CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(_content);


                channel.ConfirmSelect();
                channel.BasicPublish(exchange: "", routingKey: _queueName, mandatory: true, null, body);
                channel.WaitForConfirmsOrDie();

                channel.ConfirmSelect();
            }
        }
    }
}
