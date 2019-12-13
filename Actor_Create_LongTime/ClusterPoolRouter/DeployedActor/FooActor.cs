using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using Akka.Routing;
using Microsoft.VisualBasic.FileIO;

namespace DeployedActor
{
    public class FooActor : ReceiveActor
    {
        private Cluster _cluster;
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new FooActor())
                .WithRouter(FromConfig.Instance);
        }

        public FooActor()
        {
            _cluster = Cluster.Get(Context.System);
            _log.Info($">>> FooActor Constructor - Address : {_cluster.SelfAddress}, {Self.Path.ToStringWithAddress()}");

            Thread.Sleep(7000);

            Receive<string>(_ => Handle(_));
        }

        private void Handle(string msg)
        {
            _log.Info($">>> Message Received : {msg}"); 
        }

        protected override void PostStop()
        {
            //_log.Info($">>> PostStop()");
        }
    }
}
