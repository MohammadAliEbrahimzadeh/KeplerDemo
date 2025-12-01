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
using System.Net;

namespace KeplerDemo.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<CustomResponse> GetProductsAsync(CancellationToken cancellationToken)
    {
        var product = _unitOfWork.GetAsQueryable<Product>()
            .ApplyKeplerOrdering("Filter", x => x.SellStartDate, OrderOperationEnum.Descending)
            .ThenApplyKeplerOrdering("Filter", x => x.ProductID, OrderOperationEnum.Ascending)
            .ApplyKeplerPolicy("Filter", new { MakeFlag = true });

        var count = await product.CountAsync();

        var products = await product.ApplyKeplerPagination()
            .ProjectToType<ProductTestDto>().ToListAsync(cancellationToken);

        return new CustomResponse()
        {
            Data = products,
            IsSuccess = true,
            Message = "Returned",
            StatusCode = HttpStatusCode.OK,
            TotalCount = count
        };

    }


    public async Task<CustomResponse> GetProductsBenchMarkAsync(CancellationToken cancellationToken)
    {
        const int iterations = 1000;

        long keplerCountTotal = 0;
        long keplerPagingTotal = 0;
        long efCountTotal = 0;
        long efPagingTotal = 0;
        long dapperCountTotal = 0;
        long dapperPagingTotal = 0;

        var sw = new Stopwatch();
        var connectionString = "Server=localhost;Database=AdventureWorks2022;Trusted_Connection=True;TrustServerCertificate=True;"; // <-- Replace with your DB connection string

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("========== WARMING UP DATABASE ==========");
        Console.ResetColor();

        // Warm-up (not measured)
        _ = await _unitOfWork.GetAsQueryable<Product>().CountAsync(cancellationToken);
        _ = await _unitOfWork.GetAsQueryable<Product>()
            .Skip(0).Take(10)
            .Select(p => new ProductDto { Name = p.Name, Color = p.Color, MakeFlag = p.MakeFlag })
            .ToListAsync(cancellationToken);
        _ = await _unitOfWork.GetAsQueryable<Product>()
            .ApplyKeplerPolicy("Filter", new { Name = "Decal" })
            .CountAsync(cancellationToken);
        _ = await _unitOfWork.GetAsQueryable<Product>()
            .ApplyKeplerPolicy("Filter", new { Name = "Decal" })
            .ApplyKeplerPagination()
            .ProjectToType<ProductDto>()
            .ToListAsync(cancellationToken);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Warmup completed.");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("========== BENCHMARK START ==========");
        Console.ResetColor();

        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync(cancellationToken);

        for (int i = 0; i < iterations; i++)
        {
            // ------------------- KEPLER COUNT -------------------
            sw.Restart();
            var keplerQuery = _unitOfWork.GetAsQueryable<Product>()
                .ApplyKeplerPolicy("Filter", new { Name = "Decal" });
            await keplerQuery.CountAsync(cancellationToken);
            sw.Stop();
            keplerCountTotal += sw.ElapsedMilliseconds;

            // ------------------- KEPLER PAGING -------------------
            sw.Restart();
            await keplerQuery
                .ApplyKeplerPagination()
                .ProjectToType<ProductDto>()
                .ToListAsync(cancellationToken);
            sw.Stop();
            keplerPagingTotal += sw.ElapsedMilliseconds;

            // ------------------- EF COUNT -------------------
            sw.Restart();
            var efQuery = _unitOfWork.GetAsQueryable<Product>()
                .Where(p => p.Name.Contains("Decal"));
            await efQuery.CountAsync(cancellationToken);
            sw.Stop();
            efCountTotal += sw.ElapsedMilliseconds;

            // ------------------- EF PAGING -------------------
            sw.Restart();
            await efQuery
                .Skip(0).Take(10)
                .Select(p => new ProductDto { Name = p.Name, Color = p.Color, MakeFlag = p.MakeFlag })
                .ToListAsync(cancellationToken);
            sw.Stop();
            efPagingTotal += sw.ElapsedMilliseconds;

            // ------------------- DAPPER COUNT -------------------
            sw.Restart();
            var dapperCount = await conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM [Production].[Product] WHERE [Name] LIKE @Name",
                new { Name = "Decal%" });
            sw.Stop();
            dapperCountTotal += sw.ElapsedMilliseconds;

            // ------------------- DAPPER PAGING -------------------
            sw.Restart();
            var dapperPaging = (await conn.QueryAsync<ProductDto>(
                @"SELECT [Name], [Color], [MakeFlag] 
              FROM [Production].[Product] 
              WHERE [Name] LIKE @Name 
              ORDER BY [ProductId] 
              OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY",
                new { Name = "Decal%" })).ToList();
            sw.Stop();
            dapperPagingTotal += sw.ElapsedMilliseconds;
        }

        // Calculate averages
        double avgKeplerCount = keplerCountTotal / (double)iterations;
        double avgKeplerPaging = keplerPagingTotal / (double)iterations;
        double avgEfCount = efCountTotal / (double)iterations;
        double avgEfPaging = efPagingTotal / (double)iterations;
        double avgDapperCount = dapperCountTotal / (double)iterations;
        double avgDapperPaging = dapperPagingTotal / (double)iterations;

        // ========================= TABLE OUTPUT =========================
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("                   AVERAGE EXECUTION TIMES (ms)");
        Console.ResetColor();
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine("| Component                |    Kepler     |    EF Core    |   Dapper  |");
        Console.WriteLine("-----------------------------------------------------------------------");

        PrintRow("Count()", avgKeplerCount, avgEfCount, avgDapperCount);
        PrintRow("Pagination + Projection", avgKeplerPaging, avgEfPaging, avgDapperPaging);

        Console.WriteLine("-----------------------------------------------------------------------");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Benchmark completed successfully.");
        Console.ResetColor();

        // Return actual Kepler result
        var finalProducts = await _unitOfWork.GetAsQueryable<Product>()
            .ApplyKeplerPolicy("Filter", new { Name = "Decal" })
            .ApplyKeplerPagination()
            .ProjectToType<ProductDto>()
            .ToListAsync(cancellationToken);

        return new CustomResponse
        {
            Data = finalProducts,
            IsSuccess = true,
            Message = "Benchmark completed",
            StatusCode = HttpStatusCode.OK,
            TotalCount = finalProducts.Count
        };

        void PrintRow(string label, double kepler, double ef, double dapper)
        {
            Console.Write("| ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{label.PadRight(24)}");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{kepler.ToString("0.00").PadLeft(10)}");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"{ef.ToString("0.00").PadLeft(12)}");
            Console.ResetColor();

            Console.Write(" | ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{dapper.ToString("0.00").PadLeft(10)}");
            Console.ResetColor();
            Console.WriteLine(" |");
        }
    }
}
