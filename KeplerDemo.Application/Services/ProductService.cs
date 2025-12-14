using Dapper;
using Kepler.Core;
using Kepler.Core.Enums;
using Kepler.Core.Pagination;
using KeplerDemo.Application.Contracts;
using KeplerDemo.Application.DTOs;
using KeplerDemo.Domain.Entities;
using KeplerDemo.Infrastructure;
using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace KeplerDemo.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponse> GetProductsAsync(ProductFilterDto dto, CancellationToken cancellationToken)
    {
        var config = KeplerPolicyHelper.GetPolicyConfiguration(typeof(Product), "Nav", "Test1");

        var config2 = KeplerPolicyHelper.GetPolicyConfiguration(typeof(Product), "Nav", "Test");

        KeplerPolicyHelper.PrintPolicyConfiguration(typeof(Product), "Nav", "Test");


        var productSql1 = await _unitOfWork.GetAsQueryable<Product>()
               .ApplyKeplerPolicy(KeplerPolicyConfig.CreateWithFullDebug("Nav", dto, ignoreGlobalExceptions: true, role: "Test1"), out KeplerDebugInfo? de1)
               .ApplyKeplerOrdering(KeplerOrderingConfig.CreateWithSql("Nav", "Name", OrderOperationEnum.Descending, "Test1"), out string? sql1)
               .ApplyKeplerPagination()
               .ToListAsync(cancellationToken);


        var productSql = await _unitOfWork.GetAsQueryable<Product>()
               .ApplyKeplerPolicy(KeplerPolicyConfig.CreateWithFullDebug("Nav", dto, false, "Test"), out KeplerDebugInfo? de)
               .ApplyKeplerOrdering(KeplerOrderingConfig.CreateWithSql("Nav", "SellStartDate", OrderOperationEnum.Descending, "Test"), out string? sql)
               .ApplyKeplerPagination().ToListAsync(cancellationToken);

        return new CustomResponse()
        {
            Data = productSql1,
            IsSuccess = true,
            Message = "Returned",
            StatusCode = HttpStatusCode.OK,
            TotalCount = 1
        };

    }
}
