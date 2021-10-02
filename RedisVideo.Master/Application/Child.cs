using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Master.Application
{
    public class Child
    {
        public Child(string childUrl, int minHash, int maxHash)
        {
            ChildUrl = childUrl;
            MinHash = minHash;
            MaxHash = maxHash;
        }

        public string ChildUrl { get; set; }
        public int MinHash { get; set; }
        public int MaxHash { get; set; }
    }
}
