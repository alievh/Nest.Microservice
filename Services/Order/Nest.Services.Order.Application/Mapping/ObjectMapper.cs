using AutoMapper;

namespace Nest.Services.Order.Application.Mapping;

public class ObjectMapper
{
    private static readonly Lazy<IMapper> lazy = new(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomMapping>();
        });

        return config.CreateMapper();
    });

    public static IMapper Mapper => lazy.Value;
}
