using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisVideo.Master.Application
{
    public interface IReplicationService
    {
        Task ReplicateToSlavesAsync(string key, string value, CancellationToken cancellationToken);
    }
}
