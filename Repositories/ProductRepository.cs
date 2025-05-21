using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        _326059268_ShopApiContext DBcontext;
        public ProductRepository(_326059268_ShopApiContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public async Task<List<Product>> GetProducts()
        {
            return await DBcontext.Products.Include(c=>c.Catgory).ToListAsync();
            //return await DBcontext.Products.ToListAsync();
        }
    }
}
