using DTO;
using Entities;

namespace Services
{
    public interface ICatgoryService
    {
        Task<List<CategoryDTO>> GetCatgories();
    }
}