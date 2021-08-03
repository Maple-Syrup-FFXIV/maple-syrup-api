using maple_syrup_api.Context;
using maple_syrup_api.Dto;
using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Repositories.Repository
{
    public class RequirementRepository : BaseRepository<EventRequirement>, IRequirementRepository
    {
        private readonly MapleSyrupContext _context;
        public RequirementRepository(MapleSyrupContext pContext) : base(pContext)
        {
            _context = pContext;
        }

        public int Update(EventRequirement req) //This is kinda useless!
        { //Error code => 0 : Success, 1 : Error
            _context.EventRequirements.Update(req);
            return 0;
            //Return error code
        }

    }
}
