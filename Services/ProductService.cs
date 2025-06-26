using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;
using System.Runtime.CompilerServices;
using AutoMapper;
using DTO;
namespace Services
{
   public  class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<List<ProductDTO>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
           var products= await _productRepository.GetProducts(desc,  minPrice, maxPrice, categoryIds);
            //return products.Select(x=>_mapper.Map<ProductDTO>(x)).ToList();//
            return _mapper.Map<List<ProductDTO>>(products);
        }
    }
}
