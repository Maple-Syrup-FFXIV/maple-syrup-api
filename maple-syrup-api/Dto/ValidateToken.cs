using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Dto
{
    public class ValidateToken
    {
        public int? UserId { get; set; }
        public string AccessToken { get; set; }
    }
}
