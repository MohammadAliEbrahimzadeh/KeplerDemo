using System.ComponentModel.DataAnnotations.Schema;


namespace KeplerDemo.Domain.Entities;

[Table(name: "Product", Schema = "Production")]
public class Product
{
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public string? ProductNumber { get; set; }
    public bool MakeFlag { get; set; }
    public bool FinishedGoodsFlag { get; set; }
    public string? Color { get; set; }
    public decimal StandardCost { get; set; }
    public decimal ListPrice { get; set; }
    public string? Size { get; set; }
    public string? SizeUnitMeasureCode { get; set; }
    public string? WeightUnitMeasureCode { get; set; }
    public decimal? Weight { get; set; }
    public int? ProductSubcategoryID { get; set; }
    public int? ProductModelID { get; set; }
    public DateTime SellStartDate { get; set; }
    public DateTime? SellEndDate { get; set; }
    public DateTime? DiscontinuedDate { get; set; }

    //[kepler(reason: "Test")]
    public Guid rowguid { get; set; }
    public DateTime ModifiedDate { get; set; }

    public ProductSubcategory? ProductSubcategory { get; set; }
    public ProductModel? ProductModel { get; set; }
    public UnitMeasure? SizeUnitMeasure { get; set; }
    public UnitMeasure? WeightUnitMeasure { get; set; }

    public ICollection<ProductProductPhoto> ProductProductPhotos { get; set; } = new List<ProductProductPhoto>();
    public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
    public ICollection<ProductCostHistory> ProductCostHistories { get; set; } = new List<ProductCostHistory>();
    public ICollection<ProductListPriceHistory> ProductListPriceHistories { get; set; } = new List<ProductListPriceHistory>();
    public ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
}

