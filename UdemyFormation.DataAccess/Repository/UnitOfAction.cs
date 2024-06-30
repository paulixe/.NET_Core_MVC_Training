using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyFormation.DataAccess.Data;
using UdemyFormation.DataAccess.Repository.IRepository;

namespace UdemyFormation.DataAccess.Repository
{
    public class UnitOfAction : IUnitOfAction
    {
        protected readonly ApplicationDbContext db;

        public UnitOfAction(ApplicationDbContext db)
        {
            this.db = db;
            Category = new CategoryRepository(db);
            Product = new ProductRepository(db);
            Company = new CompanyRepository(db);
            Cart = new CartRepository(db);
        }

        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public ICartRepository Cart { get; private set; }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}