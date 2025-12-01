using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeplerDemo.Domain.Entities;

[Table(name: "UnitMeasure", Schema = "Production")]
public class UnitMeasure
{
    [Key]
    public string UnitMeasureCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime ModifiedDate { get; set; }

    public ICollection<Product> SizeProducts { get; set; } = new List<Product>();
    public ICollection<Product> WeightProducts { get; set; } = new List<Product>();
}
