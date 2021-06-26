using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Models
{
    public class GuildConfigs
    {
        public int Id { get; set; }

        [DefaultValue("$")]
        public string Prefix { get; set; }

        public string GuildId { get; set; }
    }
}
