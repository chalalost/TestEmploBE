using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestEmploSkill.Model.Entities;
using TestEmploSkill.Service;

namespace TestEmploSkill.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet("getAll")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("getDetail/{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost("createProduct")]
        public ActionResult<Product> PostProduct(Product product)
        {
            var newProduct = _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
        }

        [HttpPost("updateProduct/{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            var productUpdate = _productService.UpdateProduct(id, product);
            if (!productUpdate)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("deleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var productDelete = _productService.DeleteProduct(id);
            if (!productDelete)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
