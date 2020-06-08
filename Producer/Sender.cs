using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    public class Sender
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };//crating factory
            using (var connection = factory.CreateConnection())// creating connection
            using (var channel = connection.CreateModel())// open channel
            {
                channel.QueueDeclare("BasicTest", false, false, false, null);// this is the key
                string message = "getting started with .net core RabbitMQ";// message
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", "BasicTest", null, body);//publish
                Console.WriteLine("Sent message {0}", message);
            }

            Console.WriteLine("Press [enter] to exit the sender app");
            Console.ReadLine();
        }
    }
}
