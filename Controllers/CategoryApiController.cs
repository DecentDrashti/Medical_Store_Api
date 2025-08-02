using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
            #region Configuration Fields 
            private readonly MedicalStoreContext _context;
            public CategoryApiController(MedicalStoreContext context)
            {
                _context = context;
            }
            #endregion
            #region GetAllCategorys 
            [HttpGet]
            public IActionResult GetCategory()
            {
                var Category = _context.Categories.ToList();
                return Ok(Category);
            }
            #endregion
            #region GetCategoryById 
            [HttpGet("{id}")]
            public IActionResult GetCategoryById(int id)
            {
                var Category = _context.Categories.Find(id);
                if (Category == null)
                {
                    return NotFound();
                }
                return Ok(Category);
            }
            #endregion
            #region DeleteCategoryById 
            [HttpDelete("{id}")]
            public IActionResult DeleteCategoryById(int id)
            {
                var Category = _context.Categories.Find(id);
                if (Category == null)
                {
                    return NotFound();
                }
                _context.Categories.Remove(Category);
                _context.SaveChanges();
                return NoContent();
            }
            #endregion
            #region InsertCategory 
            [HttpPost]
            public IActionResult InsertCategory(Category Category)
            {
                _context.Categories.Add(Category);
                _context.SaveChanges();
                return NoContent();
            }
            #endregion
            #region UpdateCategory 
            [HttpPut("{id}")]
            public IActionResult UpdateCategory(int id, Category Category)
            {
                if (id != Category.CategoryId) // Ensure route ID matches the Category ID
                {
                    return BadRequest();
                }
                var existingCategory = _context.Categories.Find(id);
                if (existingCategory == null)
                {
                    return NotFound();
                }
                existingCategory.CategoryName = Category.CategoryName;
                _context.Categories.Update(existingCategory);
                _context.SaveChanges();
                return NoContent();
            }
            #endregion

        }
    }
