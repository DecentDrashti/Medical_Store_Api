using Medical_Store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public CustomerApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllCustomers 
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCustomer()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }
        #endregion
        #region GetCustomerById 
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetCustomerById(int id)
        {
            var customers = _context.Customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }
        #endregion
        #region DeleteCustomerById 
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomerById(int id)
        {
            var customers = _context.Customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customers);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertCustomer 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region UpdateCustomer
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId) // Ensure route ID matches the customer ID
            {
                return BadRequest();
            }
            var existingCustomer = _context.Customers.Find(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.UserId = customer.UserId;
            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.ContactNumber = customer.ContactNumber;
            existingCustomer.Address = customer.Address;
            existingCustomer.City = customer.City;
            _context.Customers.Update(existingCustomer);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region UserDropDown
        // Get all Users (for dropdown)
        [HttpGet("dropdown/Users")]
        [Authorize(Roles = "Admin ,Customer")]
        public async Task<ActionResult<IEnumerable<object>>> GetUsers()
        {
            return await _context.Users
                .Select(c => new { c.UserId, c.UserName })
                .ToListAsync();
        }
        #endregion

        // Advanced Filtering Endpoint
        #region FilterCustomers
        [HttpGet("filter")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<IEnumerable<Customer>>> Filter(
            [FromQuery] string? name,
            [FromQuery] string? city,
            [FromQuery] string? username)
        {
            var query = _context.Customers
                .Include(c => c.User) // optional, if you want user info
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.CustomerName.Contains(name));

            if (!string.IsNullOrEmpty(city))
                query = query.Where(c => c.City != null && c.City.Contains(city));

            if (!string.IsNullOrEmpty(username))
                query = query.Where(c => c.User != null &&
                                         c.User.UserName.Contains(username));

            return await query.ToListAsync();
        }
        #endregion

    }
}
