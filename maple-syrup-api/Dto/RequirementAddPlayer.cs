using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maple_syrup_api.Models;

namespace maple_syrup_api.Dto
{
    public class RequirementAddPlayerIn
    {
        public string PlayerName { get; set; }
        public ClassType Class { get; set; }
        public JobType Job { get; set; }
        public DPSType DPSType { get; set; }
        public int EventId { get; set; }

    }

    public class RequirementAddPlayerOut
    {

        public bool Check { get; set; }

    }
}
