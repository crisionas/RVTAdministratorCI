using BusinessLayer;
using BusinessLayer.Interfaces;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RVT_AdministratorAPI.AppServices
{
    public class QueueConnectionWorker
    {
        private string _queueName;
        private readonly IQueueConnection _queueConnection;
        private readonly IResponseHandler _responseHandler;

        public QueueConnectionWorker(IQueueConnection queueConnection, string queueName)
        {
            _queueConnection = queueConnection;
            _queueName = queueName;
            var bl = new BusinessManager();
            _responseHandler = bl.GetResponseActions();
            
        }

        public void InitReceiverChannel()
        {
            if(!_queueConnection.IsConnected)
            {
                _queueConnection.TryConnect();
            }

            var channel = _queueConnection.CreateModel();

            channel.QueueDeclare(queue: _queueName,
                exclusive: false,
                durable:false,
                autoDelete: false,
                arguments: null);

            var receiver = new EventingBasicConsumer(channel);

            receiver.Received += ReceiveEvent;

            channel.BasicConsume(_queueName, true, consumer: receiver);

        }

        private void ReceiveEvent(object sender, BasicDeliverEventArgs e)
        {
            if(e.RoutingKey == "voteResponse")
            {
                var data = Encoding.UTF8.GetString(e.Body.ToArray());
                _responseHandler.PrepareVoteResponse(data);
            }
        }
    }
}