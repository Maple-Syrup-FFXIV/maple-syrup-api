using System.ComponentModel;

namespace maple_syrup_api.Models
{
    public class GuildConfig
    {
        public int Id { get; set; }

        [DefaultValue("$")]
        public string Prefix { get; set; }

        public string GuildId { get; set; }
    }
}
