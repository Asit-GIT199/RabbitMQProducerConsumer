using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    public class Receiver
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())// creating connection
            using (var channel = connection.CreateModel())// open channel
            {
                channel.QueueDeclare("BasicTest", false, false, false, null);// this is the key
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received message {0}..", message);
                    };

                channel.BasicConsume("BasicTest",true,consumer);
                Console.WriteLine("Press [enter] to exit the consumer app");
                Console.ReadLine();
            }
        }
    }
}
