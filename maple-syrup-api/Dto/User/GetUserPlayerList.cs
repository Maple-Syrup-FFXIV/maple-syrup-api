using maple_syrup_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto.User
{
    public class GetUserPlayerListIn
    {
        public int UserId { get; set; }
    }
    public class GetUserPlayerListOut
    {
        public List<Player> playerList { get; set; }
    }
}
