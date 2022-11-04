﻿using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FridgeRepository : RepositoryBase<Fridge>, IFridgeRepository
    {
        public FridgeRepository(RepositoryContext context) : base(context)
        {
        }

        public IEnumerable<Fridge> GetAllFridges(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();
    }
}
