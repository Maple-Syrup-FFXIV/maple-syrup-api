using maple_syrup_api.Context;
using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Repositories.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly MapleSyrupContext _context;

        public UserRepository(MapleSyrupContext pContext) : base(pContext)
        {
            _context = pContext;
        }

        public User GetUserByDiscordId(long pId)
        {
            return _context.Set<User>().FirstOrDefault(x => x.DiscordId == pId);
        }

        public int InsertAndGetId(User user)
        {
            _context.Set<User>().Add(user);
            _context.SaveChanges();
            return user.Id;
        }
    }
}
