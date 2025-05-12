using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;
namespace Services
{
    public class CatgoryService : ICatgoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CatgoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<Catgory>> GetCatgories()
        {
            return await _categoryRepository.GetCatgories();
        }
    }
}
