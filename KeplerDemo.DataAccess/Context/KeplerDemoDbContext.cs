using KeplerDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace KeplerDemo.DataAccess.Context
{
    public class KeplerDemoDbContext : DbContext
    {
        public KeplerDemoDbContext(DbContextOptions<KeplerDemoDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<ProductSubcategory> ProductSubCategories { get; set; } = null!;
        public DbSet<ProductModel> ProductModels { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductPhoto> ProductPhotos { get; set; } = null!;
        public DbSet<ProductProductPhoto> ProductProductPhotos { get; set; } = null!;
        public DbSet<ProductReview> ProductReviews { get; set; } = null!;
        public DbSet<ProductCostHistory> ProductCostHistories { get; set; } = null!;
        public DbSet<ProductListPriceHistory> ProductListPriceHistories { get; set; } = null!;
        public DbSet<ProductInventory> ProductInventories { get; set; } = null!;
        public DbSet<ProductDocument> ProductDocuments { get; set; } = null!;
        public DbSet<UnitMeasure> UnitMeasures { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key for ProductProductPhoto
            modelBuilder.Entity<ProductProductPhoto>()
                .HasKey(ppp => new { ppp.ProductID, ppp.ProductPhotoID });

            // Composite key for ProductCostHistory
            modelBuilder.Entity<ProductCostHistory>()
                .HasKey(pch => new { pch.ProductID, pch.StartDate });

            // Composite key for ProductListPriceHistory
            modelBuilder.Entity<ProductListPriceHistory>()
                .HasKey(plph => new { plph.ProductID, plph.StartDate });

            // Relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductSubcategory)
                .WithMany(ps => ps.Products)
                .HasForeignKey(p => p.ProductSubcategoryID);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductModel)
                .WithMany(pm => pm.Products)
                .HasForeignKey(p => p.ProductModelID);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.SizeUnitMeasure)
                .WithMany(u => u.SizeProducts)
                .HasForeignKey(p => p.SizeUnitMeasureCode);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.WeightUnitMeasure)
                .WithMany(u => u.WeightProducts)
                .HasForeignKey(p => p.WeightUnitMeasureCode);

            // ProductProductPhoto relationships
            modelBuilder.Entity<ProductProductPhoto>()
                .HasOne(ppp => ppp.Product)
                .WithMany(p => p.ProductProductPhotos)
                .HasForeignKey(ppp => ppp.ProductID);

            modelBuilder.Entity<ProductProductPhoto>()
                .HasOne(ppp => ppp.ProductPhoto)
                .WithMany(pp => pp.ProductProductPhotos)
                .HasForeignKey(ppp => ppp.ProductPhotoID);

            // ProductReview relationship
            modelBuilder.Entity<ProductReview>()
                .HasOne(pr => pr.Product)
                .WithMany(p => p.ProductReviews)
                .HasForeignKey(pr => pr.ProductID);

            // ProductCostHistory relationship
            modelBuilder.Entity<ProductCostHistory>()
                .HasOne(pch => pch.Product)
                .WithMany(p => p.ProductCostHistories)
                .HasForeignKey(pch => pch.ProductID);

            // ProductListPriceHistory relationship
            modelBuilder.Entity<ProductListPriceHistory>()
                .HasOne(plph => plph.Product)
                .WithMany(p => p.ProductListPriceHistories)
                .HasForeignKey(plph => plph.ProductID);

            // ProductInventory relationship
            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductInventories)
                .HasForeignKey(pi => pi.ProductID);

            modelBuilder.Entity<ProductInventory>()
                .HasKey(pi => new { pi.ProductID, pi.LocationID });

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(KeplerConfiguration).Assembly);
        }
    }
}
