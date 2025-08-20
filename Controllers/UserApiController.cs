using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public UserApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllUsers 
        [HttpGet]
        public IActionResult GetUser()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
        #endregion
        #region GetUserById 
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var users = _context.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        #endregion
        #region DeleteUserById 
        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            var users = _context.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }
            _context.Users.Remove(users);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertUser 
        [HttpPost]
        public IActionResult InsertUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateUser 
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (id != user.UserId) // Ensure route ID matches the user ID
            {
                return BadRequest();
            }
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.RoleId = user.RoleId;
            _context.Users.Update(existingUser);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
