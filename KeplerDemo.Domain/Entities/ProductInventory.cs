using Kepler.Core.Attributes;
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
    public int ProductID { get; set; }

    public short LocationID { get; set; }  // Optional: Could change to `short` for exact match to `smallint`, but `int` works (widens safely).
    public string Shelf { get; set; } = null!;
    public byte Bin { get; set; }  // Optional: Could change to `byte` for exact match to `tinyint` (unsigned 0-255), but `short` works (widens safely).
    public short Quantity { get; set; }  // Changed from `byte` to `short`.

    [KeplerGlobalExclude(reason: "test")]
    public Guid rowguid { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Product Product { get; set; } = null!;
}
