using KeplerDemo.Application.Contracts;
using KeplerDemo.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kepler.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("getProducts")]
        public async Task<CustomResponse> GetProducts(CancellationToken cancellationToken)
            => await _productService.GetProductsAsync(cancellationToken);

        [HttpGet]
        [Route("getProductsBecnhmark")]
        public async Task<CustomResponse> GetProductsBecnhmark(CancellationToken cancellationToken)
            => await _productService.GetProductsBenchMarkAsync(cancellationToken);
    }
}
