using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.EC.JOMA.EMP.DOMAIN.Tools
{
    public sealed class GSUserException : Exception
    {
        public GSUserException(string message)
            : base(message)
        {
        }
    }
}
