using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeplerDemo.Domain.Entities;

[Table(name: "ProductDocument", Schema = "Production")]
public class ProductDocument
{
    [Key]
    public int ProductID { get; set; }
    public string DocumentNode { get; set; } = null!;
    public DateTime ModifiedDate { get; set; }
}
