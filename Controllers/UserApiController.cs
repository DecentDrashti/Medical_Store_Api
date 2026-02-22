using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Medical_Store.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,Customer")]

    public class UserApiController : ControllerBase
    {
        #region Configuration Fields

        private readonly MedicalStoreContext _context;
        private readonly IConfiguration _configuration;

        public UserApiController(MedicalStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        #endregion
        #region GetAllUsers 
        [Authorize(Roles = "Admin")]
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
            existingUser.Password = user.Password;
            existingUser.RoleId = user.RoleId;
            _context.Users.Update(existingUser);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region RoleDropDown
        // Get all Users (for dropdown)
        [HttpGet("dropdown/Roles")]
        public async Task<ActionResult<IEnumerable<object>>> GetRoles()
        {
            return await _context.Roles
                .Select(c => new { c.RoleId, c.RoleName })
                .ToListAsync();
        }
        #endregion

        // Advanced Filtering Endpoint
        #region FilterUsers
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<User>>> Filter(
            [FromQuery] string? UName,
            [FromQuery] string? Email,
            [FromQuery] string? RName)
        {
            var query = _context.Users
                .Include(c => c.Role) // optional, if you want user info
                .AsQueryable();

            if (!string.IsNullOrEmpty(UName))
                query = query.Where(c => c.UserName.Contains(UName));

            if (!string.IsNullOrEmpty(Email))
                query = query.Where(c => c.Email != null && c.Email.Contains(Email));

            if (!string.IsNullOrEmpty(RName))
                query = query.Where(c => c.Role != null &&
                                         c.Role.RoleName.Contains(RName));

            return await query.ToListAsync();
        }
        #endregion

        #region AuthenticateUser
        // 🔑 Generate Token with Role & Expiry from appsettings.json
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim("RoleId", user.RoleId.ToString()) // store RoleId as custom claim
    };

            // If you want to use [Authorize(Roles="Admin")], then add RoleName also:
            if (user.Role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role.RoleName));
            }

            var expiryMinutes = Convert.ToDouble(jwtSettings["TokenExpiryMinutes"]);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

        #region Login
        [AllowAnonymous]
        // ✅ LOGIN API
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogin loginUser)
        {
            var user = await _context.Users
                .Include(u => u.Role) // load Role navigation (if exists)
                .FirstOrDefaultAsync(u =>
                    u.Email == loginUser.Email &&
                    u.Password == loginUser.Password &&
                    u.RoleId == loginUser.RoleId);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password" });

            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    user.UserId,
                    user.UserName,
                    user.Email,
                    user.RoleId
                    //RoleName = user.Role?.RoleName
                }
            });
        }

        #endregion

        #region Registration
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister user)
        {
            user.RoleId = 2; // default to Customer

            // Hash the password
            var hasher = new PasswordHasher<UserRegister>();
            user.Password = hasher.HashPassword(user, user.Password);

            await _context.Users.AddAsync(new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            });

            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully as Customer" });
        }
        #endregion

    }
}
