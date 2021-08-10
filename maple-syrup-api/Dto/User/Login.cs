using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto.Auth
{
    public class LoginIn
    {
        public string DiscordCode { get; set; }
    }

    public class LoginOut
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string AccessToken { get; set; }

        public string Username { get; set; }

    }
}
