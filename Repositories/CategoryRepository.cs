using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
namespace Repositories
{
   public  class CategoryRepository : ICategoryRepository
    {
        _326059268_ShopApiContext DBcontext;//_dbContext
        public CategoryRepository(_326059268_ShopApiContext DBcontext)
        {
            this.DBcontext = DBcontext;//_dbContext = dbContext;
        }
        public async Task<List<Catgory>> GetCatgories()
        {
            return await DBcontext.Catgories.Include(c=>c.Products).ToListAsync();
            //return await DBcontext.Catgories.ToListAsync();//
        }
    }
}
