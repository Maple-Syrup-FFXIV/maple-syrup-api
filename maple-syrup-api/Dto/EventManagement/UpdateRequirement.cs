using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto
{
    public class UpdateRequirementIn
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public bool PreciseJob { get; set; }
        public bool OnePerJob { get; set; }
        public bool DPSRequiredByType { get; set; }
        public bool AllowBlueMage { get; set; }
        public List<int> DPSTypeRequirement { get; set; }
        public List<int> ClassRequirement { get; set; }
        public List<int> PerJobRequirement { get; set; }
        public int PlayerLimit { get; set; }
        public int MinILevel { get; set; }//Not yet implemented
        public int MinLevel { get; set; }//Not yet implemented
    }

    public class UpdateRequirementOut
    {

        public bool Check { get; set; }

    }
}
