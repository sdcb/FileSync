using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSync.Functional
{
    public static class Maybe
    {
        public static Maybe<T> Empty<T>() where T : class
        {
            return new Maybe<T>();
        }
    }
}
