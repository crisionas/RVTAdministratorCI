using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RVT_AdministratorAPI.AppServices;
using RVTLibrary.Models.LoadBalancer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_AdministratorAPI.AppServices.Tests
{
    [TestClass()]
    public class RabbitMQQueueConnectionTests
    {
        [TestMethod()]
        public void RabbitMQQueueConnectionTest()
        {


            var clientFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
            };

            var connection = clientFactory.CreateConnection();






            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "voteDataMsg", durable: false, exclusive: false, autoDelete: false, arguments: null);

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;
                properties.Headers = new Dictionary<string, object>();

                for (int i = 1; i <= 100; i++)
                {
                    var message = new ChooserLbMessage()
                    {
                        //IDNP = "512412412312",
                        IDVN = "1" + i.ToString(),
                        PartyChoosed = 3,
                        Vote_date = DateTime.Now,
                        Birth_date = DateTime.Now,
                        Gender = "M",
                        Region = 2,
                    };


                    string messageSerialized = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(messageSerialized);

                    channel.ConfirmSelect();

                    channel.BasicPublish(exchange: "", routingKey: "voteDataMsg", basicProperties: properties,
                        body: body);

                    channel.WaitForConfirmsOrDie();
                    channel.ConfirmSelect();
                }
            }
        }

    }
}
