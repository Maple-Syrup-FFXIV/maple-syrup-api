using maple_syrup_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace maple_syrup_api.Repositories.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByDiscordId(long pId);
        int InsertAndGetId(User user);
    }
}
