using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public AdminApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllAdmins 
        [HttpGet]
        public IActionResult GetAdmin()
        {
            var admins = _context.Admins.ToList();
            return Ok(admins);
        }
        #endregion
        #region GetAdminById 
        [HttpGet("{id}")]
        public IActionResult GetAdminById(int id)
        {
            var admins = _context.Admins.Find(id);
            if (admins == null)
            {
                return NotFound();
            }
            return Ok(admins);
        }
        #endregion
        #region DeleteAdminById 
        [HttpDelete("{id}")]
        public IActionResult DeleteAdminById(int id)
        {
            var admins = _context.Admins.Find(id);
            if (admins == null)
            {
                return NotFound();
            }
            _context.Admins.Remove(admins);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertAdmin 
        [HttpPost]
        public IActionResult InsertAdmin(Admin Admin)
        {
            _context.Admins.Add(Admin);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateAdmin 
        [HttpPut("{id}")]
        public IActionResult UpdateAdmin(int id, Admin Admin)
        {
            if (id != Admin.AdminId) // Ensure route ID matches the Admin ID
            {
                return BadRequest();
            }
            var existingAdmin = _context.Admins.Find(id);
            if (existingAdmin == null)
            {
                return NotFound();
            }
            existingAdmin.UserId = Admin.UserId;
            existingAdmin.FullName = Admin.FullName;
            _context.Admins.Update(existingAdmin);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region UserDropDown
        // Get all Users (for dropdown)
        [HttpGet("dropdown/Users")]
        public async Task<ActionResult<IEnumerable<object>>> GetUsers()
        {
            return await _context.Users
                .Select(c => new { c.UserId, c.UserName })
                .ToListAsync();
        }
        #endregion

        #region FilterAdmin
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Admin>>> Filter(
            [FromQuery] string? FullName,
            [FromQuery] string? UserName)
        {
            var query = _context.Admins
                .Include(c => c.User) // optional, if you want user info
                .AsQueryable();

            if (!string.IsNullOrEmpty(FullName))
                query = query.Where(c => c.FullName.Contains(FullName));

            if (!string.IsNullOrEmpty(UserName))
                query = query.Where(c => c.User != null &&
                                         c.User.UserName.Contains(UserName));

            return await query.ToListAsync();
        }
        #endregion
    }
}
