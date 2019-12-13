using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;

namespace BasicLocalActor
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("Akka.conf"));

            ActorSystem system = ActorSystem.Create("actorsystem", config);

            var actor = system.ActorOf(FooActor.Props(), nameof(FooActor));
            actor.Tell("hello");

            Console.ReadLine();
        }
    }
}
