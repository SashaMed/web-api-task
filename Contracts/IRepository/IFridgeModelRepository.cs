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
        FridgeModel GetFridgeModel(Guid id, bool trackChanges);
    }
}
