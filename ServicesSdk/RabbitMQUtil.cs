using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace ServicesSdk
{
    public class RabbitMQUtil
    {
        public static string HostName { get; set; }
        public static string Message { get; set; }
        public static string QueueName { get; set; }
        private byte[] Body()
        {
            return Encoding.UTF8.GetBytes(Message);

        }

        public List<string> GetValidationList(string list)
        {
            List<string> result = new List<string>();

            string[] terms = list.Split(new char[] { '|' });
            for (int i = 0; i < terms.Length; ++i)
            {
                string term = terms[i];
                result.Add(term);
            }
            
            return result;
        }


        private string GetName(string body)
        {
            return body.Split(' ').Last();
        }

        private IConnection CreateConnection()
        {
            var factory = new ConnectionFactory() { HostName = HostName };
            return factory.CreateConnection();

        }
        private IModel CreateChannel(IConnection connect)
        {
            IModel model = connect.CreateModel();
            model.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            return model;
        }


        public void PublishMessage()
        {
            using (var connection = CreateConnection())
            {
                using (var channel = CreateChannel(connection))
                {
                    channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: null, body: Body());

                }

            }
        }


        public void ConsumeMessage(List<string> list)
        {
            string message = "";
            byte[] body = null;
            var factory = new ConnectionFactory() { HostName = HostName };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueName,
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        body = ea.Body;
                        message = Encoding.UTF8.GetString(body);

                        var name = GetName(message);
                        if (list.Contains(name))
                            Console.WriteLine("Hello  {0} I am your father!",name);
                    };

                    channel.BasicConsume(queue: QueueName,
                                            autoAck: true,
                                            consumer: consumer
                                            );

                    Console.WriteLine("Press [enter] to exit");
                    Console.ReadLine();

                    


                }
            }

        }

    }
}
