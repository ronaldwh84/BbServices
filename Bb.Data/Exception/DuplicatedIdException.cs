using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Data
{
    public class DuplicatedIdException : Exception
    {
        public DuplicatedIdException()
        {
        }

        public DuplicatedIdException(string message)
            : base(message)
        {
        }

        public DuplicatedIdException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
