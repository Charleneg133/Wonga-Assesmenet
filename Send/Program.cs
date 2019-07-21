using System;
using System.Configuration;
using ServicesSdk;


namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("args is empty");
                Console.ReadLine();
            }
            else
            {
                var name = args[0];

                RabbitMQUtil.HostName = ConfigurationManager.AppSettings[SendConstants.HOST_NAME];
                RabbitMQUtil.QueueName = ConfigurationManager.AppSettings[SendConstants.QUEUE_NAME];

                RabbitMQUtil.Message = "Hello my name is, " + name;

                var publisher = new Send(new RabbitMQUtil());

                publisher.Publish();

                Console.WriteLine( RabbitMQUtil.Message);
                Console.ReadLine();



            }
        }
    }
}

