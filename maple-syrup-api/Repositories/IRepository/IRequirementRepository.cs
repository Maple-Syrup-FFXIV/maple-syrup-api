using maple_syrup_api.Dto;
using maple_syrup_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace maple_syrup_api.Repositories.IRepository
{
    public interface IRequirementRepository : IBaseRepository<EventRequirement>
    {

        int Update(EventRequirement req);


    }
}
