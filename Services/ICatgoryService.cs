using Entities;

namespace Services
{
    public interface ICatgoryService
    {
        Task<List<Catgory>> GetCatgories();
    }
}