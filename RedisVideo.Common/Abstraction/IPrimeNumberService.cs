using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Common.Abstraction
{
    public interface IPrimeNumberService
    {
        int GetPrime(int min);
    }
}
