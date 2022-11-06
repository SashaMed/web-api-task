using Contracts.IRepository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class FridgeRepository : RepositoryBase<Fridge>, IFridgeRepository
    {
        public FridgeRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateFridge(Fridge fridge)
        {
            Create(fridge);
        }

        public void DeleteFridge(Fridge fridge)
        {
            Delete(fridge);
        }

        public async Task<IEnumerable<Fridge>> GetAllFridgesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        }
        public async Task<Fridge> GetFridgeAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(b => b.Id == id, trackChanges).SingleOrDefaultAsync();
        }
    }
}
