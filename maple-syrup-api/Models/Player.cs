using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public ClassType Class { get; set; }
        public JobType Job { get; set; }
        public DPSType DPSType { get; set; }
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
