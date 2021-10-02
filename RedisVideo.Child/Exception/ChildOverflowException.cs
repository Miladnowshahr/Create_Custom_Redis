using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisVideo.Child.Exception
{
    public class ChildOverflowException : System.Exception
    {
        public ChildOverflowException() : base("Child is overflow")
        {
        }

        public ChildOverflowException(string message) : base(message) { }

        public ChildOverflowException(string message, System.Exception inner) : base(message, inner) { }
    }
}
