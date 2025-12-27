using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using NotesAPI.Data;

namespace NotesAPI.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]

        public IActionResult CreateCategory(Category category)
        {

            if(category == null)
            {
                return BadRequest("Invalid input");
            }

            if(category.Name.IsNullOrEmpty())
            {
                return BadRequest("Name can not be empty");
            }

            if(category.Name.Length < 3)
            {
                return BadRequest("Name Length can not be less than 3 chars");
            }
            


            _context.Categories.Add(category);
            _context.SaveChanges();
            


            return Ok(category);
        }


        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _context.Categories.ToList();

            return Ok(categories);
        }

        [HttpGet("{id}")]

        public IActionResult GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryID == id);

            if(category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateCategory(int id, Category updatedcategory)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c=> c.CategoryID ==id);
              
            if(existingCategory == null)
            {
                return NotFound();
            }
            
           existingCategory.Name = updatedcategory.Name;
           existingCategory.Description = updatedcategory.Description;
           

            _context.SaveChanges();

            return Ok(existingCategory);

        }
        
    }
}