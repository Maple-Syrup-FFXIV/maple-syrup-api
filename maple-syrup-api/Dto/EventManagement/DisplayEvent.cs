using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto.EventManagement
{
    public class DisplayEventIn
    {
        public int EventId { get; set; }
        //public int UserId { get; set; }
    }

    public class DisplayEventOut
    {
        public string NameFight { get; set; }
        public int PlayerCount { get; set; }
        public int PlayerLimit { get; set; }
        public List<PlayerButton> PlayerButton { get; set; }
        public string Description { get; set; }
        public bool OnePlayerPerJob { get; set; }
        public bool DutyComplete { get; set; }
    }

    public class PlayerButton
    {
        public IconType Icon { get; set; }
        public string PlayerName { get; set; }
        public bool IsFree { get; set; }
    }

    public enum IconType
    {
        TANK,
        HEALER,
        DPS,
        CASTER,
        MELEE,
        RANGED,
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
