using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Common.Abstraction
{
    public interface IBitHelper
    {
        uint ByteToUint32(byte[] bytes);
    }
}
