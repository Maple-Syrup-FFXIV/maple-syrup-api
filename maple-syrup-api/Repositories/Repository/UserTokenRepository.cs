using maple_syrup_api.Context;
using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Repositories.Repository
{
    public class UserTokenRepository : BaseRepository<UserToken>, IUserTokenRepository
    {
        private readonly MapleSyrupContext _context;

        public UserTokenRepository(MapleSyrupContext pContext) : base(pContext)
        {
            _context = pContext;
        }

        public UserToken GetByAccessToken(string pAccesstoken)
        {
            return _context.Set<UserToken>().FirstOrDefault(x => x.Token == pAccesstoken);
        }
    }
}
