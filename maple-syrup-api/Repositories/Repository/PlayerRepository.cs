using maple_syrup_api.Context;
using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;


namespace maple_syrup_api.Repositories.Repository
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        private readonly MapleSyrupContext _context;
        public PlayerRepository(MapleSyrupContext pContext) : base(pContext)
        {
            _context = pContext;
        }
    }
}
