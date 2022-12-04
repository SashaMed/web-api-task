using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IRepositoryManager
    {
        IFridgeRepository Fridge { get; }
        IFridgeProductsRepository FridgeProducts { get; }
        IProductsRepository Products { get; }
        IFridgeModelRepository FridgeModel { get; }

        Task<int> CallStoredProcedure();

        Task SaveAsync();
    }
}
