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
            CreateMap<UserRegisterDTO,User>().ReverseMap();
            //CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<User, UserDTO>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName.Trim()))
    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.Trim()))
    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.Trim()));


        }
    }
}
