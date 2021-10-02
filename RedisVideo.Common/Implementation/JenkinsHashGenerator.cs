using RedisVideo.Common.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.Jenkins;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Common.Implementation
{
    public class JenkinsHashGenerator:IHashGenerate
    {
        private readonly IJenkinsOneAtATime _jenkinsOneAtATime = JenkinsOneAtATimeFactory.Instance.Create();

        private readonly IBinarySerializer _serializer;
        private readonly IBitHelper _bitHelper;

        public JenkinsHashGenerator(IBitHelper bitHelper,IBinarySerializer serializer)
        {
            _serializer = serializer;
            _bitHelper = bitHelper;
        }

        public uint Generate<T>(T obj)
        {
            var bytes = _serializer.Serialize(obj);

            var hash = _jenkinsOneAtATime.ComputeHash(bytes);
            var hashBytes = hash.Hash;
            var intHash = _bitHelper.ByteToUint32(hashBytes);
            return intHash;
        }
    }
}
