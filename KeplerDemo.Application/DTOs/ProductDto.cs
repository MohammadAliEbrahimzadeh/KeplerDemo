using Kepler.Core.Attributes;
using KeplerDemo.Domain.Entities;

namespace KeplerDemo.Application.DTOs;

public class ProductDto
{
    public bool MakeFlag { get; set; }
    public string? Name { get; set; }
    public string? Color { get; set; }

    //[KeplerIgnore]
    public int ProductID { get; set; }

    public int ProductModelID { get; set; }
}
