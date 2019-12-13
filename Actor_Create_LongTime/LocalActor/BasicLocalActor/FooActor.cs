using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;

namespace BasicLocalActor
{
    public class FooActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new FooActor());
        }

        public FooActor()
        {
            Thread.Sleep(10000);

            Receive<string>(_ => Handle(_));
        }

        private void Handle(string msg)
        {
            _log.Info(">>> Message Received");
        }
    }
}
