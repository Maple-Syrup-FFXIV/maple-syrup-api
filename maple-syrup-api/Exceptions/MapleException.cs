using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Exceptions
{
    public class MapleException : Exception
    {

        public MapleException() { }

        public MapleException(string message) : base(message){}
    }
}
