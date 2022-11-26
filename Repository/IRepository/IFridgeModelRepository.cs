using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IFridgeModelRepository
    {
        Task<FridgeModel> GetFridgeModelAsync(Guid id, bool trackChanges);

        Task<IEnumerable<FridgeModel>> GetAllFridgeModelsAsync(bool trackChanges);
    }
}
