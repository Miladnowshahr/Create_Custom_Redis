using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Common.Dto
{
    public class ChildEntriesDto
    {
        public ChildEntriesDto(string url, List<BucketDto> entries)
        {
            Url = url;
            Entries = entries;
        }

        public string Url { get; set; }
        public List<BucketDto> Entries { get; set; }

    }
}
