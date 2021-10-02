using RedisVideo.Common.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Common.Implementation
{
    public class NativeHashGenerator : IHashGenerate
    {
        public uint Generate<T>(T obj)
        {
            return (uint)obj.GetHashCode();
        }
    }
}
