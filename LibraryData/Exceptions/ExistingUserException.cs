using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData.Exceptions
{
    public class ExistingUserException : Exception
    {
        public ExistingUserException(string Message) : base(Message) {}
    }
}
