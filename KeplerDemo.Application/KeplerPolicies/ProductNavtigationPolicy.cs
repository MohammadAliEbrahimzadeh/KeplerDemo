using Kepler.Core.Attributes.Attributes;
using Kepler.Core.Builder;
using Kepler.Core.Enums;
using KeplerDemo.Domain.Entities;

namespace KeplerDemo.Application.KeplerPolicies;

[KeplerPolicyName("Nav")]
public class ProductNavtigationPolicy : IKeplerPolicy<Product>
{
    public void Configure(IKeplerPolicyBuilder<Product> builder)
    {
        // ------------------------------------------
        // Policy: Test1
        // ------------------------------------------
        builder.For("Test1")
            .AllowFields(
                x => x.Color!,
                x => x.Name!,
                x => x.MakeFlag,
                x => x.SellStartDate,
                x => x.ProductID
            )
            .AllowNestedFields(
                x => x.ProductCostHistories,
                nested => nested.SelectFields<ProductCostHistory>(
                    pch => pch.ProductID,
                    pch => pch.StartDate,
                    pch => pch.StandardCost
                )
            )
            .AllowFilteredNestedFields(
                x => x.ProductListPriceHistories.Where(pl => pl.ListPrice < 74),
                nested => nested.SelectFields<ProductListPriceHistory>(
                    pl => pl.ProductID,
                    pl => pl.StartDate,
                    pl => pl.EndDate!,
                    pl => pl.ListPrice,
                    pl => pl.ModifiedDate
                )
            )
            .AllowFilteredNestedFields(
                x => x.ProductInventories.Where(pi => pi.Quantity <= 324),
                nested => nested.SelectFields<ProductInventory>(
                    pi => pi.ProductID,
                    pi => pi.LocationID,
                    pi => pi.Shelf!,
                    pi => pi.Bin,
                    pi => pi.rowguid
                )
            )
            .AllowFilteredNestedFields(
                x => x.ProductReviews.Where(r => r.Rating == 5),
                nested => nested.SelectFields<ProductReview>(
                    r => r.Rating,
                    r => r.ProductReviewID
                )
            )
            .AllowNestedFields(
                x => x.ProductModel!,
                nested => nested.SelectFields<ProductModel>(
                    pm => pm.ProductModelID,
                    pm => pm.ModifiedDate!,
                    pm => pm.rowguid
                )
            )
            .AllowOrderBy(
                x => x.Name!,
                x => x.SellStartDate
            )
            .AllowFilter(x => x.MakeFlag, FilterOperationEnum.Equals)
            .AllowFilter(x => x.ProductID, FilterOperationEnum.Equals)
            .AllowFilter(x => x.Name, FilterOperationEnum.StartsWith);


        // ------------------------------------------
        // Policy: Test
        // ------------------------------------------
        builder.For("Test")
            .AllowFields(
                x => x.Color!,
                x => x.Name!,
                x => x.MakeFlag,
                x => x.SellStartDate,
                x => x.ProductID
            )
            .AllowOrderBy(
                x => x.Name!,
                x => x.SellStartDate
            )
            .AllowFilter(x => x.MakeFlag, FilterOperationEnum.Equals)
            .AllowFilter(x => x.ProductID, FilterOperationEnum.Equals)
            .AllowFilter(x => x.Name, FilterOperationEnum.StartsWith);
    }
}
