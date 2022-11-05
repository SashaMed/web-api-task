using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IFridgeRepository
    {
        IEnumerable<Fridge> GetAllFridges(bool trackChanges);
        Fridge GetFridge(Guid id, bool trackChanges);
        
        void CreateFridge(Fridge fridge);

        void DeleteFridge(Fridge fridge);
    }
}
