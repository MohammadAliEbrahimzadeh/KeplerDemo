using Kepler.Web.Filters;
using KeplerDemo.Application.Contracts;
using KeplerDemo.Application.KeplerPolicies;
using KeplerDemo.Application.Mappers;
using KeplerDemo.Application.Services;
using KeplerDemo.DataAccess.Context;
using KeplerDemo.Infrastructure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<StatusCodeActionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;


var db = builder.Services.AddDbContext<KeplerDemoDbContext>(options =>
    options.UseSqlServer(connectionString, SqlOptions =>
    {
        SqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null
        );

        SqlOptions.CommandTimeout(30);
    }));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

TypeAdapterConfig.GlobalSettings.Scan(typeof(ProductMapper).Assembly);

builder.Services.AddKepler()
    .AddKeplerPoliciesFromAssembly(typeof(ProductPolicy).Assembly)
    .ValidateKeplerPolicies();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
