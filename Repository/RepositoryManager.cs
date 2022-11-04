using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;

        public IFridgeRepository _fridgeRepository;

        public IFridgeRepository Fridge
        {
            get
            {
                if (_fridgeRepository == null)
                    _fridgeRepository = new FridgeRepository(_repositoryContext);

                return _fridgeRepository;
            }
        }
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
