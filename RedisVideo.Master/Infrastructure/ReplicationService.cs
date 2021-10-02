using Microsoft.Extensions.Options;
using RedisVideo.Master.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisVideo.Master.Infrastructure
{
    public class ReplicationService : IReplicationService
    {
        private readonly IMasterClient _client;
        private readonly MasterOptions _options;

        public ReplicationService(IMasterClient client,IOptions<MasterOptions> options)
        {
            _client = client;
            _options = options.Value;
        }


        public Task ReplicateToSlavesAsync(string key, string value, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            foreach (var slaveUrl in _options.Slaves)
            {
                tasks.Add(_client.SendReplicationRequestAsync(slaveUrl, key, value, cancellationToken));
            }

            return Task.WhenAll(tasks);
        }
    }
}
