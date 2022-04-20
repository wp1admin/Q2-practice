using producer.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace producer.Utils
{
    public class RabbitMQ
    {
        public static void publish(APIresult apiResult)
        {
            var factory = new ConnectionFactory()
            {
                //      HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
                //      Port = Convert.ToInt32 (Environment.GetEnvironmentVariable("RABBITMQ_PORT"))


                HostName = "localhost",
                Port = 31672
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "TaskExchange",
                            type: ExchangeType.Direct,
                            durable: true,
                            autoDelete: false);

                channel.QueueDeclare(queue: "TaskQueue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = JsonSerializer.SerializeToUtf8Bytes(apiResult);
                channel.BasicPublish(exchange: "TaskExchange",
                    routingKey: "TaskExchange",
                    basicProperties: null,
                    body: body);
            }
                
           
        }
    }
}
