using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto
{
    public class RequirementAddPlayerIn
    {
        public string PlayerName { get; set; }
        public int Class { get; set; }
        public int Job { get; set; }
        public int? DPSType { get; set; }
        public int EventId { get; set; }

    }

    public class RequirementAddPlayerOut
    {

        public bool Check { get; set; }

    }
}
