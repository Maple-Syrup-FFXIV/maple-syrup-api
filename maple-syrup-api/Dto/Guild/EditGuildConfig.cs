using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto
{
    public class EditGuildConfigIn
    {
        public string Prefix { get; set; }

        public string GuildId { get; set; }
    }
}
