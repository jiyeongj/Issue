using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster.Sharding;
using Akka.Configuration;
using ClusterShardingShared;
using Petabridge.Cmd.Cluster;
using Petabridge.Cmd.Cluster.Sharding;
using Petabridge.Cmd.Host;
using static ClusterShardingShared.FooActor;

namespace SeedNode1
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("Akka.conf"));

            //
            // "{app-name} - akka.tcp://{actorysystem-name}@{hostname}:{port}"
            //
            Console.Title = $"{config.GetString("akka.system.app-name")}" +
                $" - akka.tcp://{config.GetString("akka.system.actorsystem-name")}" +
                $"@{config.GetString("akka.remote.dot-netty.tcp.hostname")}" +
                $":{config.GetString("akka.remote.dot-netty.tcp.port")}";

            ActorSystem system = ActorSystem.Create("ClusterLab", config);

            var cmd = PetabridgeCmd.Get(system);
            cmd.RegisterCommandPalette(ClusterCommands.Instance);
            cmd.RegisterCommandPalette(ClusterShardingCommands.Instance);
            cmd.Start();

            Console.WriteLine();
            Console.WriteLine("SeedNode1 is running...");
            Console.WriteLine();

            // register actor type as a sharded entity
            var region = ClusterSharding.Get(system).Start(
                typeName: "my-actor",
                entityProps: Props.Create<FooActor>(),
                settings: ClusterShardingSettings.Create(system),
                messageExtractor: new MessageExtractor());

            Thread.Sleep(20000);

            // send message to entity through shard region
            region.Tell(new ClusterShardingShared.Envelope(shardId: 1, entityId: 1, message: new AddMessage("hello")));
            region.Tell(new ClusterShardingShared.Envelope(shardId: 1, entityId: 2, message: new AddMessage("hello")));
            region.Tell(new ClusterShardingShared.Envelope(shardId: 1, entityId: 3, message: new AddMessage("hello")));
            region.Tell(new ClusterShardingShared.Envelope(shardId: 1, entityId: 4, message: new AddMessage("hello")));
            region.Tell(new ClusterShardingShared.Envelope(shardId: 2, entityId: 1, message: new AddMessage("hello")));
            region.Tell(new ClusterShardingShared.Envelope(shardId: 2, entityId: 2, message: new AddMessage("hello")));
            region.Tell(new ClusterShardingShared.Envelope(shardId: 2, entityId: 3, message: new AddMessage("hello")));

            Console.ReadLine();
        }
    }
}
