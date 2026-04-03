using AutoMapper;
using OrderingSystem.Global.DTOs.CustomerDtos;
using OrderingSystem.Global.DTOs.OrderDtos;
using OrderingSystem.Global.Entities;

namespace OrderingSystem.BusinessLogic.MapperConfigration
{
    public class MappingConfig: Profile
    {

        public MappingConfig()
        {
            // Customer Mapping
            CreateMap<CustomerRegisterDto, Customer>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<Customer, CustomerResponseDto>();

            // Order Mapping
            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore());
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"));
        }
    }
}
