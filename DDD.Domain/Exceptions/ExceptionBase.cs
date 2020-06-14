using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Exceptios
{
    public abstract class ExceptionBase : Exception
    {
        public ExceptionBase(string message):base(message)
        {
        }

        public ExceptionBase(string message,Exception exception) : base(message,exception)
        {
        }

        // 継承するサブクラスはKindを返さないとコンパイルが通らない。
        public abstract ExceptionKind Kind { get; }
        public enum ExceptionKind
        {
            info,
            Warinng,
            Error,
        }
    }
}
