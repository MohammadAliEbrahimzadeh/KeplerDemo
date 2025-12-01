using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeplerDemo.Domain.Entities;

[Table(name: "ProductReview", Schema = "Production")]
public class ProductReview
{
    [Key]
    public int ProductReviewID { get; set; }
    public int ProductID { get; set; }
    public string ReviewerName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public int Rating { get; set; }
    public string Comments { get; set; } = null!;
    public DateTime ReviewDate { get; set; }

    public Product Product { get; set; } = null!;
}


