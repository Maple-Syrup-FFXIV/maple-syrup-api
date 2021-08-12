using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maple_syrup_api.Models;

namespace maple_syrup_api.Dto
{
    public class RequirementRemovePlayerIn
    {

        public int PlayerId { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }

    }
    public class RequirementRemovePlayerOut
    {
        public bool Check { get; set; }
    }

}
