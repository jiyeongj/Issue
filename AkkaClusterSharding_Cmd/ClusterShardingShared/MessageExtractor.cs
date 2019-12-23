using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Cluster.Sharding;

namespace ClusterShardingShared
{
    public sealed class MessageExtractor : IMessageExtractor
    {
        public string EntityId(object message)
        {
            return (message as Envelope)?.EntityId.ToString();
        }

        public string ShardId(object message)
        {
            return (message as Envelope)?.ShardId.ToString();
        }

        public object EntityMessage(object message)
        {
            return (message as Envelope)?.Message;
        }
    }
}
