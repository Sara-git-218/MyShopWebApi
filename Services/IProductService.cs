using DTO;
using Entities;

namespace Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}