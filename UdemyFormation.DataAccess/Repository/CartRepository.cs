using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyFormation.DataAccess.Data;
using UdemyFormation.DataAccess.Repository.IRepository;
using UdemyFormation.Models;

namespace UdemyFormation.DataAccess.Repository
{
    public class CartRepository : Repository<ShoppingCart>, ICartRepository
    {
        public CartRepository(ApplicationDbContext db) : base(db)
        {
        }

        public void Update(ShoppingCart cart)
        {
            dbSet.Update(cart);
        }
    }
}