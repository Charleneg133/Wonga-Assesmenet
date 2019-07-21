using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using ServicesSdk;

namespace Send
{
    public class Send:aSend
    {
        private RabbitMQUtil _util;
        public Send(RabbitMQUtil util)
        {
            _util = util;

        }
    
        public override void Publish()
        {
            _util.PublishMessage();
        }
    }
    
}
