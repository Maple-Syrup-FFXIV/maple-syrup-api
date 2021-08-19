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

        //Boolean Research
        public bool OnePlayerPerJob { get; set; }
        public bool AllowBlueMage { get; set; }
        public bool SpecificEventType { get; set; }
        public bool HasMinILevel { get; set; }
        public bool SpecificFight { get; set; }
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
