using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IFridgeRepository
    {
        Task<IEnumerable<Fridge>> GetAllFridgesAsync(bool trackChanges);
        Task<Fridge> GetFridgeAsync(Guid id, bool trackChanges);

        void CreateFridge(Fridge fridge);

        void DeleteFridge(Fridge fridge);

        Task<IEnumerable<Fridge>> GetAllFridgesWithModels(bool trackChanges);
    }
}
