using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Master
{
    public class MasterOptions
    {
        public string ReplicationActor { get; set; }
        public List<string> Slaves { get; set; }
        public List<string> Children { get; set; }
    }
}
