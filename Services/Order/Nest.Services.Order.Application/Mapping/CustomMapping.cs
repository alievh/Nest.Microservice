using AutoMapper;
using Nest.Services.Order.Application.DTO_s;
using Nest.Services.Order.Domain.OrderAggregate;

namespace Nest.Services.Order.Application.Mapping;

public class CustomMapping : Profile
{
    public CustomMapping()
    {
        CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}
