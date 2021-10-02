﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Common.Abstraction
{
    public interface IHashGenerate
    {
        uint Generate<T>(T obj);
    }
}
