using Newtonsoft.Json;
using RedisVideo.Common.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Common.Implementation
{
    public class JsonSerializer:IBinarySerializer
    {
        
        public byte[] Serialize<T>(T obj)
        {
            var startObj = JsonConvert.SerializeObject(obj);
            var bytes = Encoding.UTF8.GetBytes(startObj);
            return bytes;
        }
    }
}
