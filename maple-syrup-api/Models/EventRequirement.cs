using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Models
{
    public class EventRequirement
    {
        public int Id { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }
        public int EventId { get; set; }
        public bool PreciseJob { get; set; }//If thisis true, when adding player will also check if their Job is in need
        //In case OnePlayerPerJob is true, PerJobRequirement all be set to 1
        public bool OnePerJob { get; set; }
        public bool DPSRequiredByType { get; set; }//Not yet Implemented
        public bool AllowBlueMage { get; set; }//Not yet Implemented
        public virtual List<int> DPSTypeRequirement { get; set; } //Not Yet implemented
        public virtual List<int> ClassRequirement { get; set; }
        //The Job list will have this format [TANKREQ,HEALERREQ,DPSREQ]. 
        public virtual List<int> PerJobRequirement { get; set; }//This list will have this format
                                                        //[PLD, WAR, DRK, GNB, => TANK
                                                        //WHM, SCH, AST, => HEALER
                                                        //MNK, DRG, NIN, SAM, => MELEE
                                                        //BRD, MCH, DNC, => RANGED
                                                        //BLM, SMN, RDM => CASTER
                                                        //, BLUE] => BLUE MAGE
        public virtual List<Player> Players { get; set; }//List of all Id of players currently in the fight

        public int PlayerLimit { get; set; }
        public int PlayerCount { get; set; }
        public int MinILevel { get; set; }//Not yet implemented
        public int MinLevel { get; set; }//Not yet implemented


        public EventRequirement()
        {
            Players = new List<Player>();
            DPSTypeRequirement = new List<int>();
            ClassRequirement = new List<int>();
            PerJobRequirement = new List<int>();
        }
    }

  


}
