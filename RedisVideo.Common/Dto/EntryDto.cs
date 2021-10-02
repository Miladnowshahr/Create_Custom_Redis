using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Common.Dto
{
    public class EntryDto
    {
        public EntryDto(uint hashKey,string key,string value)
        {
            HashKey = hashKey;
            Key = key;
            Value = value;
        }

        public uint HashKey { get; }
        public string Key { get; }
        public string Value { get; }
    }
}
