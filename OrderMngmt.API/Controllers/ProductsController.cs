using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderMngmt.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using OrderMngmt.Business.Models;

namespace OrderMngmt.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductModel>> GetProducts([FromQuery] PaginationFilter? paginationFilter, [FromQuery] SortFilter? sortFilter)
        {
            var products = _productService.GetProducts(paginationFilter, sortFilter);

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProduct(ProductModel product)
        {
            await _productService.AddProduct(product);
            await _productService.SaveChanges();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

    }
}