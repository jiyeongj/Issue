using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;

namespace ClusterShardingShared
{
    public class FooActor : ReceiveActor
    {
        #region Messages
        public sealed class AddMessage
        {
            public string Msg;

            public AddMessage(string msg)
            {
                Msg = msg;
            }
        }

        public sealed class GetMessages { }
        #endregion

        private List<string> _messages;
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public FooActor()
        {
            _messages = new List<string>();
            _log.Info("Actor Instantiated");

            Receive<AddMessage>(_ => Handle(_));
            Receive<GetMessages>(_ => Handle(_));
        }

        private void Handle(AddMessage msg)
        {
            _log.Info("Received type: " + msg.GetType().Name);
            _log.Info("Received message: " + msg.Msg.ToString());
            _messages.Add(msg.ToString());
        }

        private void Handle(GetMessages msg)
        {
            _log.Info("Received message's Count: " + _messages.Count);
        }
    }
}
