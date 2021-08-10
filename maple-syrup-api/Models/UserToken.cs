using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Models
{
    public class UserToken
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
