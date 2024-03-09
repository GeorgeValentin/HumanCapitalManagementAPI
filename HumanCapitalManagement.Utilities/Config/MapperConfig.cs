using AutoMapper;

namespace HumanCapitalManagement.Utilities.Config;
public static class MapperConfig<T> 
    where T : Profile, new()
{
    public static IMapper ConfigureMapper()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<T>()
        );
        var mapper = config.CreateMapper();

        return mapper;
    }
}
