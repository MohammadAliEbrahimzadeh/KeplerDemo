using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeplerDemo.Application.DTOs;

public class ProductFilterDto
{
    public bool? MakeFlag { get; set; }
    public int? ProductID { get; set; }
    public string? Name { get; set; }
    public int PageNo { get; set; } = 1;
    public int SizeNo { get; set; } = 10; 
}
