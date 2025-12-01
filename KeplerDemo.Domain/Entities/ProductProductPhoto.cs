using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeplerDemo.Domain.Entities;

[Table(name: "ProductProductPhoto", Schema = "Production")]
public class ProductProductPhoto
{
    [Key]
    public int ProductID { get; set; }
    public int ProductPhotoID { get; set; }
    public bool Primary { get; set; }

    public Product Product { get; set; } = null!;
    public ProductPhoto ProductPhoto { get; set; } = null!;
}
