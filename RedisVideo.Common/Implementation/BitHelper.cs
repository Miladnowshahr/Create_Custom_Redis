using RedisVideo.Common.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Common.Implementation
{
    public class BitHelper : IBitHelper
    {
        public uint ByteToUint32(byte[] bytes)
        {
            if (bytes.Length != 4)
                throw new Exception($"Cannot convert from bytes to uint32 because byte length is not equal to 4. Its {bytes.Length}");

            var intHash = BitConverter.ToInt32(bytes, 0);

            return (uint)intHash;
        }
    }
}
