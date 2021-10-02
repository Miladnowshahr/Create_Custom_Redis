using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Common.Dto
{
    public class BucketDto
    {
        public BucketDto(uint bucketIndex, IEnumerable<EntryDto> entries)
        {
            BucketIndex = bucketIndex;
            Entries = entries;
        }

        public uint BucketIndex { get; set; }
        public IEnumerable<EntryDto> Entries { get; set; }
    }
}
