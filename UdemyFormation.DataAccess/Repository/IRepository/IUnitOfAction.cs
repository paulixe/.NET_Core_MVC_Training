﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyFormation.DataAccess.Repository.IRepository
{
    public interface IUnitOfAction
    {
        public ICategoryRepository Category { get; }
        public IProductRepository Product { get; }

        public void Save();
    }
}