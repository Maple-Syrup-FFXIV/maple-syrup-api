using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto.EventManagement
{
    public class DeleteEventIn
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
    }

    public class DeleteEventOut
    {
    }
}
