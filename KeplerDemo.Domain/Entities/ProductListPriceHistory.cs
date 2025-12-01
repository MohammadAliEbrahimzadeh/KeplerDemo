using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeplerDemo.Domain.Entities;

[Table(name: "ProductListPriceHistory", Schema = "Production")]
public class ProductListPriceHistory
{
    [Key]
    public int ProductID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal ListPrice { get; set; }
    public DateTime ModifiedDate { get; set; }

    public Product Product { get; set; } = null!;
}
