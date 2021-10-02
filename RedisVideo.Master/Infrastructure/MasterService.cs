using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RedisVideo.Common;
using RedisVideo.Common.Abstraction;
using RedisVideo.Common.Dto;
using RedisVideo.Master.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisVideo.Master.Infrastructure
{
    public class MasterService : IMasterService
    {
        private readonly IHashGenerate _hashGenerate;
        private readonly IChildClient _childClient;
        private readonly IPrimeNumberService _primeNumberService;
        private readonly MasterOptions _options;

        private readonly int _overallCount;
        private readonly List<Child> _children;

        public MasterService(IHashGenerate hashGenerate, IChildClient childClient, IPrimeNumberService primeNumberService, IOptions<MasterOption> options,IConfiguration config)
        {
            _hashGenerate = hashGenerate;
            _childClient = childClient;
            _primeNumberService = primeNumberService;
            _options = options.Value;

            var partitionItemsCount = primeNumberService.GetPrime(config.GetValue<int>(GlobalConst.PartitionItemsCountName));

            _overallCount = partitionItemsCount * _options.Children.Count;
            _children = InitalizeChildren(_options.Children, partitionItemsCount);
        }

        private List<Child> InitalizeChildren(List<string> childrenUrls, int partitionItemsCount)
        {
            var children = new List<Child>(childrenUrls.Count);

            var rangeMuliplier = 1;

            foreach (var childUrl in childrenUrls)
            {
                var max = rangeMuliplier * partitionItemsCount - 1;

                var min = max - partitionItemsCount + 1;

                var child = new Child(childUrl, min, max);
                children.Add(child);
                rangeMuliplier++;
            }
            return children;
        }

        public async Task AddAsync(string key, string value, CancellationToken cancellationToken)
        {
            var hash = _hashGenerate.Generate(key);
            var child = DetermineChildByHash(_children, key, hash, _overallCount);

            await _childClient.AddAsync(child, key, hash, value, cancellationToken);
        }

        private Child DetermineChildByHash(List<Child> children, string key, uint hash, int overallCount)
        {
            var hashMod = hash % overallCount;
            foreach (var child in children)
            {
                if (hashMod>=child.MinHash && hashMod<=child.MaxHash)
                {
                    return child;
                }
            }
            throw new Exception($"Cannot determine child node for this {hash} hash and {key} key.");
        }


        public async Task<List<ChildEntriesDto>> GetAllEntriesAsync(CancellationToken cancellationToken)
        {
            var resp = new List<ChildEntriesDto>();

            var tasks = new List<(string url, Task<List<BucketDto>> bucketTask)>();

            foreach (var child in _children)
            {
                tasks.Add((child.ChildUrl, Task.Run(() => _childClient.GetAllEntriesAsync(child, cancellationToken))));
            }

            foreach (var task in tasks)
            {
                var bucket = await task.bucketTask;
                resp.Add(new ChildEntriesDto(task.url, bucket));
            }
            return resp;
        }

        public async Task<string> GetAsync(string key, CancellationToken cancellationToken)
        {
            var hash = _hashGenerate.Generate(key);
            var child = DetermineChildByHash(_children, key, hash, _overallCount);

            var value = await _childClient.GetAsync(child, key, hash, cancellationToken);

            return value;
        }
    }
}
