using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RVT_AdministratorAPI.AppServices
{
    public interface IQueueConnection
    {
        bool IsConnected { get; }
        bool TryConnect();
        void Disconnect();
        IModel CreateModel();
        void InitReceiverChannel();
        void PublishData(string queueName,string content);

    }
}
