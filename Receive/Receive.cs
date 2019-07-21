using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServicesSdk;
namespace Receive
{
    class Receive:aReceive
    {
        private RabbitMQUtil _util;
        
        public Receive(RabbitMQUtil util)
        {
            _util = util;

        }

        public override void ConsumeMessage(string list)
        {
            var input = _util.GetValidationList(list);
            _util.ConsumeMessage(input);
        }
    }
}
