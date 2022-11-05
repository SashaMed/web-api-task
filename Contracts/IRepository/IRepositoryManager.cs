using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IRepositoryManager
    {
        IFridgeRepository Fridge { get; }
        IFridgeProductsRepository FridgeProducts { get; }
        IProductsRepository Products { get; }
        IFridgeModelRepository FridgeModel { get; }

        void Save();
    }
}
