using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace maple_syrup_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public long DiscordId { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public virtual List<UserToken> UserTokens { get; set; }

    }
}
