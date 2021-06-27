using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto
{
    public class AddGuildConfigIn
    {
        string Prefix { get; set; }

        string GuildId { get; set; }
    }

    public class AddGuildConfigOut
    {
        int Id { get; set; }

        string Prefix { get; set; }

        string GuildId { get; set; }
    }
}
