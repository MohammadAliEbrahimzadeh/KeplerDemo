using Kepler.Core.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeplerDemo.Domain.Entities;

[Table(name: "ProductModel", Schema = "Production")]
public class ProductModel
{
    [Key]
    public int ProductModelID { get; set; }
    public string Name { get; set; } = null!;
    public string? CatalogDescription { get; set; }
    public string? Instructions { get; set; }

    [KeplerGlobalExclude("test")]
    public Guid rowguid { get; set; }
    public DateTime ModifiedDate { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}


