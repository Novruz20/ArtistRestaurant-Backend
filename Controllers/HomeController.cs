using Artist_api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Artist_api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly  ArtistContext _sql;

        public HomeController(ArtistContext sql)
        {
            _sql = sql;
        }

        [HttpGet("filter")]
        public ActionResult<List<Product>> GetByCategory(
      string? ProductMainCategory,
      string? ProductSubCategory,
      string? ProductAlcoholCategory)
        {
            var query = _sql.Products.AsQueryable();

            if (!string.IsNullOrEmpty(ProductMainCategory))
                query = query.Where(p => p.ProductMainCategory == ProductMainCategory);

            if (!string.IsNullOrEmpty(ProductSubCategory))
                query = query.Where(p => p.ProductSubCategory == ProductSubCategory);

            if (!string.IsNullOrEmpty(ProductAlcoholCategory))
                query = query.Where(p => p.ProductAlcoholCategory == ProductAlcoholCategory);

            return query.ToList();
        }


        [HttpPost]
        public IActionResult AddProduct([FromForm] Product product, IFormFile ProductPhoto)
        {
            if (ProductPhoto == null || ProductPhoto.Length == 0)
                return BadRequest("Photo is required.");

            string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(ProductPhoto.FileName);

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                ProductPhoto.CopyTo(stream);
            }

            product.ProductPhoto = "/image/" + fileName; 
            _sql.Products.Add(product);
            _sql.SaveChanges();

            return Ok(product);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _sql.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
                return NotFound("Продукт не найден");


            _sql.Products.Remove(product);
            _sql.SaveChanges();

            return Ok("Продукт удалён");
        }


      



        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _sql.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
                return NotFound("Продукт не найден");

            return Ok(product);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] Product updatedProduct, IFormFile? newPhoto)
        {
            var product = await _sql.Products.FindAsync(id);
            if (product == null)
                return NotFound("Продукт не найден");

            // Обновляем поля
            product.ProductName = updatedProduct.ProductName;
            product.ProductDescription = updatedProduct.ProductDescription;
            product.ProductMainCategory = updatedProduct.ProductMainCategory;
            product.ProductSubCategory = updatedProduct.ProductSubCategory;
            product.ProductAlcoholCategory = updatedProduct.ProductAlcoholCategory;
            product.ProductPrice = updatedProduct.ProductPrice;

            // Если загружена новая картинка
            if (newPhoto != null && newPhoto.Length > 0)
            {
                // Удаляем старую картинку
                if (!string.IsNullOrEmpty(product.ProductPhoto))
                {
                    string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ProductPhoto.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                // Сохраняем новую
                string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(newPhoto.FileName);
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image");
                Directory.CreateDirectory(uploadPath);
                string filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await newPhoto.CopyToAsync(stream);
                }

                product.ProductPhoto = "/image/" + fileName;
            }

            await _sql.SaveChangesAsync();

            return Ok(product);
        }













    }
}
