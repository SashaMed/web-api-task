using Contracts.IRepository;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    internal class FridgeModelRepository : RepositoryBase<FridgeModel>, IFridgeModelRepository
    {
        public FridgeModelRepository(RepositoryContext context) : base(context)
        {
        }

        public FridgeModel GetFridgeModel(Guid id, bool trackChanges)
        {
            return FindByCondition(b => b.Id == id, trackChanges).SingleOrDefault();
        }
    }
}
