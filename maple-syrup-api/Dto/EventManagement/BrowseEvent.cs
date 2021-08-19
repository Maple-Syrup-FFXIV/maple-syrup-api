using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maple_syrup_api.Models;

namespace maple_syrup_api.Dto.EventManagement
{
    public class BrowseEventIn
    {
        public int Skip { get; set; }
        public int OffSet { get; set; }

        //List of Search Criteria that we want
        public bool BlueMageCriteria {get; set;}
        public bool OnePlayerPerJobCriteria{get; set;}
        public bool SpecificEventTypeCriteria{get; set;}
        public bool MinILevelCriteria {get; set;}
        public bool SpecificEventType { get; set; }
        public bool SpecificFight { get; set; }
        public bool SpecificFithLevel { get; set; }
        public bool LookingForSpecificJob { get; set; }
        public bool LookingForSpecificClass { get; set; }

        //List of JobRestriction
        public List<JobType> JobRestriction { get; set; }//List containing Job we are looking for
        public List<ClassType> ClassRestriction { get; set; }//List containing Class we are looking for
    //Boolean Research
        public bool OnePlayerPerJob { get; set; }
        public bool AllowBlueMage { get; set; }
        //Type of fight
        public EventType EventType { get; set; }
        //
        public int MinILevel { get; set; }
        public int FightLevel { get; set; }
        public string FightName { get; set; }//Should probably change this to an enum at some point
        public DateTime Time { get; set; }

        //Lets begin with that

    }
    public class BrowseEventOut
    {
        public List<DisplayEventOut> ResultList { get; set; }
    }

}
