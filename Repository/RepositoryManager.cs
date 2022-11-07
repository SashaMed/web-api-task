using Contracts.IRepository;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

        public async Task<int> CallStoredProcedure()
        {
            int c = 0;
            var quantityParam = new SqlParameter("@affected_rows",c) { Direction = ParameterDirection.Output };
            await _repositoryContext.Database
                .ExecuteSqlRawAsync("EXEC update_quatity_to_default @affected_rows OUTPUT", quantityParam);
            var quantity = Convert.ToInt32(quantityParam.Value);
            return quantity;
        }
    }
}
