using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeplerDemo.Domain.Entities;

[Table(name: "ProductSubcategory", Schema = "Production")]
public class ProductSubcategory
{
    [Key]
    public int ProductSubcategoryID { get; set; }
    public int ProductCategoryID { get; set; }
    public string Name { get; set; } = null!;
    public Guid rowguid { get; set; }
    public DateTime ModifiedDate { get; set; }

    public ProductCategory ProductCategory { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}



