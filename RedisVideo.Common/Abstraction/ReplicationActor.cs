using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Common.Abstraction
{
    public class ReplicationActor
    {
        public static ReplicationActor Master = new ReplicationActor("Master");
        public static ReplicationActor Slave = new ReplicationActor("Slave");

        private ReplicationActor(string actorName)
        {
            ActorName = actorName;
        }

        public string ActorName { get; private set; }
    }
}
