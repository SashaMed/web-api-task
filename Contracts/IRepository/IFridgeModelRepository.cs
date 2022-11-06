using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IFridgeModelRepository
    {
        Task<FridgeModel> GetFridgeModelAsync(Guid id, bool trackChanges);

        Task<IEnumerable<FridgeModel>> GetAllFridgeModelsAsync(bool trackChanges);
    }
}
