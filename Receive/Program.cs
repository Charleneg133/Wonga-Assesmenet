using ServicesSdk;
using System.Configuration;
namespace Receive
{
    class Program
    {
    
        static void Main(string[] args)
        {

            
            RabbitMQUtil.HostName = ConfigurationManager.AppSettings[ReceiveConstants.HOST_NAME];
            RabbitMQUtil.QueueName = ConfigurationManager.AppSettings[ReceiveConstants.QUEUE_NAME];
                               
            var list = ConfigurationManager.AppSettings[ReceiveConstants.VALIDATION_LIST];
            var publisher = new Receive(new RabbitMQUtil());

            

            publisher.ConsumeMessage(list);

        }
    }
}
