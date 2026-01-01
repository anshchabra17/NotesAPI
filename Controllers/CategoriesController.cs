using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
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


        [HttpGet]

        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _context.Categories.ToListAsync();

           

            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCategoryByID(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c=>c.CategoryID ==id);

            if(category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]

        public async Task <IActionResult> PostCategory(Category inputCategory)
        {
            if(inputCategory == null)
            {
                return BadRequest();
            }

            inputCategory.CreatedAt = DateTime.Now;

            await _context.Categories.AddAsync(inputCategory);
            await _context.SaveChangesAsync();

            return Ok(inputCategory);
        }

        [HttpPut("{id}")]

        public async Task <IActionResult> UpdateCategory(int id, Category inputCategory)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c=>c.CategoryID == id);

            if(existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.Name = inputCategory.Name;
            existingCategory.Description = inputCategory.Description;
            

            await _context.SaveChangesAsync();

            return Ok(existingCategory);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var cat = await _context.Categories.FirstOrDefaultAsync(d=>d.CategoryID==id);

            if(cat == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}