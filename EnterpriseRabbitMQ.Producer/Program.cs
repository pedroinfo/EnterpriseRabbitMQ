using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace EnterpriseRabbitMQ.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);


                int counter = 1;
                while (true)
                {
                    string message = $"{counter ++} - Message... :)";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);

                    Thread.Sleep(100);
                }
            }

            Console.ReadLine();
        }
    }
}
