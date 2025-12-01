using KeplerDemo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeplerDemo.Application.Contracts;

public interface IProductService
{
    Task<CustomResponse> GetProductsAsync(CancellationToken cancellationToken);

    Task<CustomResponse> GetProductsBenchMarkAsync(CancellationToken cancellationToken);
}
