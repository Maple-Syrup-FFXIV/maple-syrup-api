using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto
{
    public class RequirementAddPlayerIn
    {
        public String PlayerName { get; set; }
        public int EventId { get; set; }

    }

    public class RequirementAddPlayerOut
    {

        public bool Check { get; set; }

    }
}
