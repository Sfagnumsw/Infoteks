using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoteks.Domain.Helpers.Exceptions
{
    public class EmptyScvException : Exception
    {
        public EmptyScvException(string message) : base(message) { }
    }
}
