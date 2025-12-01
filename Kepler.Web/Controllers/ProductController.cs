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
        public async Task<CustomResponse> GetProducts([FromQuery] ProductFilterDto dto, CancellationToken cancellationToken)
            => await _productService.GetProductsAsync(dto, cancellationToken);

        [HttpGet]
        [Route("getProductsBecnhmark")]
        public async Task<CustomResponse> GetProductsBecnhmark(CancellationToken cancellationToken)
            => await _productService.GetProductsBenchMarkAsync(cancellationToken);

        [HttpGet]
        [Route("getProductsBecnhmarkV2")]
        public async Task<CustomResponse> GetProductsBecnhmarkV2(CancellationToken cancellationToken)
            => await _productService.GetProductsBenchmarkV2Async(cancellationToken);
    }
}
