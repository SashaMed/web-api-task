
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
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

        public async Task<IEnumerable<FridgeModel>> GetAllFridgeModelsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<FridgeModel> GetFridgeModelAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(b => b.Id == id, trackChanges).SingleOrDefaultAsync();
        }
    }
}
