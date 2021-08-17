using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maple_syrup_api.Models;

namespace maple_syrup_api.Dto
{
    public class CreateEventIn
    {
        //Part necessary for Event
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int EventType { get; set; }
        public int EventStatus { get; set; }
        public string FightName { get; set; }
        public int OwnerId { get; set; }


        //Part necessary for Requirement
        public bool PreciseJob { get; set; }//If thisis true, when adding player will also check if their Job is in need
        //In case OnePlayerPerJob is true, PerJobRequirement all be set to 1
        public bool OnePerJob { get; set; }
        public bool DPSRequiredByType { get; set; }//Not yet Implemented
        public bool AllowBlueMage { get; set; }//Not yet Implemented
        public List<int> DPSTypeRequirement { get; set; } //Not Yet implemented
        public List<int> ClassRequirement { get; set; }
        public List<int> PerJobRequirement { get; set; }
        public int PlayerLimit { get; set; }
        public int PlayerCount { get; set; }
        public int MinILevel { get; set; }//Not yet implemented
        public int MinLevel { get; set; }//Not yet implemented
        public string Description { get; set; }
    }

    public class CreateEventOut
    {
    }
}