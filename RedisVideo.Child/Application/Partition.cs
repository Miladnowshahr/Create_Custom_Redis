using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Child.Application
{
    public interface IPartition
    {
        List<(uint hashkey, LinkedList<Entry> entries)> GetEnries();
        void Add<T>(string key, uint hashkey, T obj);
        void Add(string key, uint hashkey, string obj);
        string Get(string key, uint hashkey);
        T Get<T>(string key, uint hashkey);
        
    }
}
