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
        public async Task<List<Product>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {


            var query = DBcontext.Products.Include(product => product.Catgory)
            .Where(product =>
            (desc == null ? (true) : (product.ProductDesdription.Contains(desc)))
            && (minPrice == null ? (true) : (product.Price >= minPrice))
            && (maxPrice == null ? (true) : (product.Price <= maxPrice))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CatgoryId)))).OrderBy(product => product.Price);

            return await query.ToListAsync();
            //return await DBcontext.Products.Include(c=>c.Catgory).ToListAsync();
            //return await DBcontext.Products.ToListAsync();
        }
    }
}
