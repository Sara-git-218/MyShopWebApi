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
        _326059268_ShopApiContext DBcontext;
        public CategoryRepository(_326059268_ShopApiContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public async Task<List<Catgory>> GetCatgories()
        {
            return await DBcontext.Catgories.ToListAsync();
        }
    }
}
