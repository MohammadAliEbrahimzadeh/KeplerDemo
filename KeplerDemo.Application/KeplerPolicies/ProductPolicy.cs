using Kepler.Core.Attributes.Attributes;
using Kepler.Core.Builder;
using Kepler.Core.Enums;
using KeplerDemo.Domain.Entities;

namespace KeplerDemo.Application.KeplerPolicies;

[KeplerPolicyName("Filter")]
public class ProductPolicy : IKeplerPolicy<Product>
{
    public void Configure(IKeplerPolicyBuilder<Product> builder)
    {
        builder
            .AllowFields(x => x.Color!, x => x.Name!, x => x.MakeFlag, x => x.SellStartDate)

            .AllowOrderBy(x => x.SellStartDate!)

            .AllowNestedFields(x => x.ProductCostHistories,
                nested => nested.SelectAllExcept(x => x.StandardCost))

            .AllowFilter(x => x.MakeFlag, FilterOperationEnum.Equals)
            .AllowFilter(x => x.Name, FilterOperationEnum.StartsWith);
    }
}
