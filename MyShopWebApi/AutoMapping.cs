using AutoMapper;
using DTO;
using Entities;

namespace MyShopWebApi
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();//.ForMember(dest=>dest.CatgoryName,opts=>opts.MapFrom(src=>src.Catgory.CatgoryName));
            CreateMap<Catgory, CategoryDTO>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Orderitem, OrderItemDTO>().ReverseMap();
        }
    }
}
