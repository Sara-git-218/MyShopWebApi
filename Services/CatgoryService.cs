using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Entities;
using Repositories;
namespace Services
{
    public class CatgoryService : ICatgoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CatgoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<List<CategoryDTO>> GetCatgories()
        {
           var categories= await _categoryRepository.GetCatgories();
           return _mapper.Map<List<CategoryDTO>>(categories);
        }
    }
}
