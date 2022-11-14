using Contracts.IRepository;
using Entities;
using Entities.DataTransferObjects;
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
            var fridge =  await FindByCondition(b => b.Id == id, trackChanges).SingleOrDefaultAsync();
            fridge.FridgeModel = await Context.FridgeModels.Where(b => b.Id == fridge.FridgeModelId).FirstAsync();
            return fridge;
        }


        public async Task<IEnumerable<Fridge>> GetAllFridgesWithModels(bool trackChanges)
        {
            var fridges = await Context.Fridges.Join(Context.FridgeModels,
                b => b.FridgeModelId,
                c => c.Id,
                (b, c) => new Fridge
                {
                    Id = b.Id,
                    Name = b.Name,
                    OwnerName = b.OwnerName,
                    FridgeModelId = c.Id,
                    FridgeModel = new FridgeModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Year = c.Year
                    }
                }).ToListAsync();

            return fridges;
        }
    }
}
