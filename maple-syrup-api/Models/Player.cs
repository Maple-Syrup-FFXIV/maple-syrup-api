using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace maple_syrup_api.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int EventRequirementId { get; set; }
        [ForeignKey("EventRequirementId")]
        [JsonIgnore]
        public virtual EventRequirement EventRequirement{get;set;}
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual User User{get;set;}
        public string PlayerName { get; set; }
        public ClassType Class { get; set; }
        public JobType Job { get; set; }
        public DPSType DPSType { get; set; }

        public bool Equals(Player p2)
        {
            return (this.EventRequirement == p2.EventRequirement) && (this.UserId == p2.UserId) && (this.PlayerName == p2.PlayerName)
                && (this.Class == p2.Class) && (this.Job == p2.Job) && (this.DPSType == p2.DPSType);
        }
    }
    public enum DPSType
    {
        MELEE,
        RANGED,
        CASTER

    }

    public enum ClassType
    {
        TANK,
        HEALER,
        DPS
    }

    public enum JobType
    {
        PLD,
        WAR,
        DRK,
        GNB,
        WHM,
        SCH,
        AST,
        MNK,
        DRG,
        NIN,
        SAM,
        BRD,
        MCH,
        DNC,
        BLM,
        SMN,
        RDM,
        BLUE

    }

}
