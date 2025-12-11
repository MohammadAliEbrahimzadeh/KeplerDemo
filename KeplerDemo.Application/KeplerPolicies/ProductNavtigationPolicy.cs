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
        builder
         .AllowFields(x => x.Color!, x => x.Name!, x => x.MakeFlag, x => x.SellStartDate, x => x.ProductID)

         .AllowNestedFields(x => x.ProductCostHistories, x => x.SelectFields<ProductCostHistory>(x => x.ProductID, x => x.StartDate, x => x.StandardCost))

    .AllowFilteredNestedFields(x => x.ProductListPriceHistories.Where(x => x.ListPrice < 74),
    x => x.SelectFields<ProductListPriceHistory>(
        plph => plph.ProductID,
        plph => plph.StartDate,
        plph => plph.EndDate!,
        plph => plph.ListPrice,
        plph => plph.ModifiedDate
    ))

        .AllowFilteredNestedFields(x => x.ProductInventories.Where(x => x.Quantity <= 324),
    x => x.SelectFields<ProductInventory>(
        plph => plph.ProductID,
        plph => plph.LocationID,
        plph => plph.Shelf!,
        plph => plph.Bin,
        plph => plph.rowguid
    ))

         .AllowFilteredNestedFields(x => x.ProductReviews.Where(x => x.Rating == 5), x => x.SelectFields<ProductReview>(x => x.Rating, x => x.ProductReviewID))

         .AllowNestedFields(x => x.ProductModel!, x => x.SelectFields<ProductModel>(x => x.ProductModelID, x => x.ModifiedDate!, x => x.rowguid))

         .AllowOrderBy(x => x.Name!, x => x.SellStartDate)

         .AllowFilter(x => x.MakeFlag, FilterOperationEnum.Equals)

         .AllowFilter(x => x.ProductID, FilterOperationEnum.Equals)

         .AllowFilter(x => x.Name, FilterOperationEnum.StartsWith);
    }
}
