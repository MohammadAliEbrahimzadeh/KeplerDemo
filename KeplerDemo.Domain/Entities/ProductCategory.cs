using System.ComponentModel.DataAnnotations.Schema;

namespace KeplerDemo.Domain.Entities;

[Table(name: "ProductCategory", Schema = "Production")]
public class ProductCategory
{
    public int ProductCategoryID { get; set; }
    public string? Name { get; set; }
    public Guid rowguid { get; set; }
    public DateTime ModifiedDate { get; set; }

    public ICollection<ProductSubcategory> ProductSubcategories { get; set; } = new List<ProductSubcategory>();
}


