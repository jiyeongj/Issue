using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClusterShardingShared
{
    public sealed class Envelope
    {
        public readonly int ShardId;
        public readonly int EntityId;
        public readonly object Message;

        public Envelope(int shardId, int entityId, object message)
        {
            ShardId = shardId;
            EntityId = entityId;
            Message = message;
        }
    }
}
