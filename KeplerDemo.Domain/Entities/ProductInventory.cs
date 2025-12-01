using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeplerDemo.Domain.Entities;

[Table(name: "ProductInventory", Schema = "Production")]
public class ProductInventory
{
    [Key]
    public int ProductID { get; set; }
    public int LocationID { get; set; }
    public string Shelf { get; set; } = null!;
    public short Bin { get; set; }
    public short Quantity { get; set; }
    public Guid rowguid { get; set; }
    public DateTime ModifiedDate { get; set; }

    public Product Product { get; set; } = null!;
}
