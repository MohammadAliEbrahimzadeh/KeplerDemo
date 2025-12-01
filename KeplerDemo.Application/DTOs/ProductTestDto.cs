using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeplerDemo.Application.DTOs;

public class ProductTestDto
{
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public string? Color { get; set; }
    public bool MakeFlag { get; set; }
    public DateTime SellStartDate { get; set; }
}
