using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RedisVideo.Child.Application;
using RedisVideo.Child.Exception;
using RedisVideo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisVideo.Child.Infrastructure
{
    public class Partition : IPartition
    {
        private readonly IPrimeNumberService _primeNumberService;
        private readonly LinkedList<Entry>[] _entries;
        private readonly int _entriesCount;
        private int _count;

        public Partition(IPrimeNumberService primeNumberService,IConfiguration config)
        {
            _primeNumberService = primeNumberService;
            var partitionItemCount = config.GetValue<int>(GlobalConst.PartitionItemsCountName);
            _entriesCount = _primeNumberService.GetPrime(partitionItemCount);
            _entries = new LinkedList<Entry>[_entriesCount];
            _count = 0;
        }

        public void Add<T>(string key, uint hashkey, T obj)
        {
            var jsonObj = JsonConvert.SerializeObject(obj);
            Add(key, hashkey, jsonObj);
        }

        public void Add(string key, uint hashkey, string obj)
        {
            if (_count >= _entriesCount)
                throw new ChildOverflowException();

            if (_entries[hashkey% _entriesCount]==null)
            {
                var newLinkedList = new LinkedList<Entry>();

                Interlocked.CompareExchange(ref _entries[hashkey % _entriesCount], newLinkedList, null);
            }

            lock(_entries[hashkey%_entriesCount])
            {
                foreach (var entry in _entries[hashkey%_entriesCount])
                {
                    if (entry.Key==key)
                    {
                        throw new System.Exception("Object with the same value has already existed");
                    }
                }

                _entries[hashkey % _entriesCount].AddLast(new Entry(hashkey, key, obj));
                _count++;
            }
            
        
        
        }

        public string Get(string key, uint hashkey)
        {
            var hashCodeList = _entries[hashkey % _entriesCount];

            if (hashCodeList == null)
            {
                return string.Empty;
            }

            foreach (var entry in hashCodeList)
            {
                if (entry.Key==key)
                {
                    return entry.Value;
                }
            }

            throw new System.Exception($"Cannot find node by {key} key");
        }

        public T Get<T>(string key, uint hashkey)
        {
            var hashCodeList = _entries[hashkey % _entriesCount];

            foreach (var entry in hashCodeList)
            {
                if (entry.Key==key)
                {
                    return JsonConvert.DeserializeObject<T>(entry.Value);
                }
            }
            throw new System.Exception($"Cannot find node by {key} key");
        }

        public List<(uint hashkey, LinkedList<Entry> entries)> GetEnries()
        {
            var entries = _entries
                .Where(l => l != null)
                .Select(l => (l.First().HashCode % (uint)_entriesCount, l))
                .ToList();
            return entries;
        }
    }
}
