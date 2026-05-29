using Kepler.Core.Attributes.Attributes;
using Kepler.Core.Builder;
using KeplerDemo.Domain.Entities;

namespace KeplerDemo.Application.KeplerPolicies;

[KeplerPolicyName("NavKKKK")]
public sealed class ProductDeepNestedPolicy : IKeplerPolicy<Product>
{
    public void Configure(IKeplerPolicyBuilder<Product> builder)
    {
        builder.For("Test1");

        builder.AllowFields(
            x => x.Name,
            x => x.ProductNumber
        );

        // LEVEL 1: Product -> SubCategory
        builder.AllowNestedFields(
            x => x.ProductSubcategory,
            sub =>
            {
                sub.SelectFields(
                    x => x.ProductSubcategoryID,
                    x => x.Name
                );

                // LEVEL 2: SubCategory -> Category
                sub.ThenInclude(
                    x => x.ProductCategory,
                    cat =>
                    {
                        cat.SelectFields(
                            x => x.ProductCategoryID,
                            x => x.Name
                        );

                        // LEVEL 3 (second branch): Category -> UnitMeasure (if exists in model OR swap)
                        cat.ThenInclude(
                            x => x.ProductSubcategories, // if not available, replace with another nav prop
                            um =>
                            {
                                um.SelectFields(
                                    x => x.Name
                                );
                            }
                        );
                    }
                );
            }
        );
    }
}