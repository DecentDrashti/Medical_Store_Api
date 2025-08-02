using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            existingAdmin.Username = Admin.Username;
            existingAdmin.PasswordHash = Admin.PasswordHash;
            _context.Admins.Update(existingAdmin);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
