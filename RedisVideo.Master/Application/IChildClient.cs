using RedisVideo.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisVideo.Master.Application
{
    public interface IChildClient
    {
        Task<List<BucketDto>> GetAllEntriesAsync(Child child, CancellationToken cancellationToken);

        Task AddAsync(Child child, string key, uint hash, string value, CancellationToken cancellationToken);

        Task<string> GetAsync(Child child, string key, uint hash, CancellationToken cancellationToken);
    }
}
