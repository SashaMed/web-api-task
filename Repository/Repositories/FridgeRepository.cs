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
    public class FridgeRepository : RepositoryBase<Fridge>, IFridgeRepository
    {
        public FridgeRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateFridge(Fridge fridge) => Create(fridge);

        public void DeleteFridge(Fridge fridge)
        {
            Delete(fridge);
        }

        public IEnumerable<Fridge> GetAllFridges(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();

        public Fridge GetFridge(Guid id, bool trackChanges)
        {
            return FindByCondition(b => b.Id == id, trackChanges).SingleOrDefault();
        }
    }
}
