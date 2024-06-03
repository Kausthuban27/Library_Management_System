using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Exceptions
{
    public class ExistingItemException : Exception
    {
        public ExistingItemException(string Message) : base(Message) { }
    }
}
