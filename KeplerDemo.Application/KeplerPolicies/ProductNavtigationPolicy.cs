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

         .AllowNestedFields(x => x.ProductModel!, x => x.SelectFields<ProductModel>(x => x.ProductModelID, x => x.ModifiedDate!))

         .AllowOrderBy(x => x.Name!, x => x.SellStartDate)

         .AllowFilter(x => x.MakeFlag, FilterOperationEnum.Equals)

         .AllowFilter(x => x.ProductID, FilterOperationEnum.Equals)

         .AllowFilter(x => x.Name, FilterOperationEnum.StartsWith);
    }
}
