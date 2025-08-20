using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetCustomer()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }
        #endregion
        #region GetCustomerById 
        [HttpGet("{id}")]
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
        [HttpPost]
        public IActionResult InsertCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateCustomer 
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
    }
}
