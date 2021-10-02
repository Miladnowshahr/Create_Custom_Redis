using RedisVideo.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisVideo.Master.Application
{
    public interface IMasterService
    {
        Task<List<ChildEntriesDto>> GetAllEntriesAsync(CancellationToken cancellationToken);
        Task AddAsync(string key, string value, CancellationToken cancellationToken);
        Task<string> GetAsync(string key, CancellationToken cancellationToken);
    }
}
