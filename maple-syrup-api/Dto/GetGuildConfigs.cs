using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto
{
    public class GetGuildConfigsOut
    {
        public GetGuildConfigsOut()
        {
            GuildList = new List<PartialGuildGetGuildConfigs>();
        }
        public List<PartialGuildGetGuildConfigs> GuildList { get; set; }
    }

    public class PartialGuildGetGuildConfigs
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public string GuildId { get; set; }
    }
}
