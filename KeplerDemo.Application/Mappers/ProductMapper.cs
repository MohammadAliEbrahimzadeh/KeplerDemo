using KeplerDemo.Application.DTOs;
using KeplerDemo.Domain.Entities;
using Mapster;

namespace KeplerDemo.Application.Mappers;

public static class ProductMapper
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Product, ProductDto>.NewConfig()
            .Map(dest => dest.MakeFlag, src => src.ProductID)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Color, src => src.Color);


        TypeAdapterConfig<Product, ProductTestDto>.NewConfig()
           .Map(dest => dest.ProductID, src => src.ProductID)
           .Map(dest => dest.Name, src => src.Name)
           .Map(dest => dest.Color, src => src.Color)
           .Map(dest => dest.MakeFlag, src => src.MakeFlag)
           .Map(dest => dest.SellStartDate, src => src.SellStartDate);
    }
}
