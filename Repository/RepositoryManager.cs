using Contracts.IRepository;
using Entities;
using Repository.Repositories;
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

        private IFridgeRepository _fridgeRepository;
        private IFridgeProductsRepository _fridgeProductsRepository;
        private IProductsRepository _productsRepository;
        private IFridgeModelRepository _fridgeModelRepository;

        public IFridgeRepository Fridge
        {
            get
            {
                if (_fridgeRepository == null)
                    _fridgeRepository = new FridgeRepository(_repositoryContext);

                return _fridgeRepository;
            }
        }


        public IFridgeProductsRepository FridgeProducts
        {
            get
            {
                if (_fridgeProductsRepository == null)
                    _fridgeProductsRepository = new FridgeProductsRepository(_repositoryContext);
                return _fridgeProductsRepository;
            }
        }

        public IProductsRepository Products
        {
            get
            {
                if (_productsRepository == null)
                    _productsRepository = new ProductsRepository(_repositoryContext);
                return _productsRepository;
            }
        }

        public IFridgeModelRepository FridgeModel
        {
            get
            {
                if (_fridgeModelRepository == null)
                    _fridgeModelRepository = new FridgeModelRepository(_repositoryContext);
                return _fridgeModelRepository;
            }
        }


        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
