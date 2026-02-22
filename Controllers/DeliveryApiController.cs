using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public DeliveryApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllDeliveries 
        [HttpGet]
        public IActionResult GetDelivery()
        {
            var delivery = _context.Deliveries.ToList();
            return Ok(delivery);
        }
        #endregion
        #region GetDeliveryById 
        [HttpGet("{id}")]
        public IActionResult GetDeliveryById(int id)
        {
            var delivery = _context.Deliveries.Find(id);
            if (delivery == null)
            {
                return NotFound();
            }
            return Ok(delivery);
        }
        #endregion
        #region DeleteDeliveryById 
        [HttpDelete("{id}")]
        public IActionResult DeleteDeliveryById(int id)
        {
            var delivery = _context.Deliveries.Find(id);
            if (delivery == null)
            {
                return NotFound();
            }
            _context.Deliveries.Remove(delivery);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertDelivery 
        [HttpPost]
        public IActionResult InsertDelivery(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateDelivery 
        [HttpPut("{id}")]
        public IActionResult UpdateDelivery(int id, Delivery delivery)
        {
            if (id != delivery.DeliveryId) // Ensure route ID matches the delivery ID
            {
                return BadRequest();
            }
            var existingDelivery = _context.Deliveries.Find(id);
            if (existingDelivery == null)
            {
                return NotFound();
            }
            existingDelivery.BillId = delivery.BillId;
            existingDelivery.CustomerId = delivery.CustomerId;
            existingDelivery.OrderId = delivery.OrderId;
            existingDelivery.DeliveryDate = delivery.DeliveryDate;
            existingDelivery.DeliveryMethod = delivery.DeliveryMethod;
            existingDelivery.DeliveryAddress = delivery.DeliveryAddress;
            existingDelivery.DeliveryStatus = delivery.DeliveryStatus;
            existingDelivery.DeliveredBy = delivery.DeliveredBy;
            existingDelivery.ContactNumber = delivery.ContactNumber;
            _context.Deliveries.Update(existingDelivery);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region Customerdropdown
        [HttpGet("dropdown/Customer")]
        public async Task<IActionResult> GetCustomerDropdown()
        {
            var customers = await _context.Customers
                .Select(c => new CustomerDropdown
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName
                })
                .ToListAsync();

            return Ok(customers); // must return a valid list
        }
        #endregion

        #region billdropdown
        [HttpGet("dropdown/Bill")]
        public async Task<IActionResult> GetBillDropdown()
        {
            var bills = await _context.Bills
                .Select(c => new BillDropdown
                {
                    BillId = c.BillId,
                    IsPaid = c.IsPaid
                })
                .ToListAsync();

            return Ok(bills); // must return a valid list
        }
        #endregion

        #region orderdropdown
        [HttpGet("dropdown/Order")]
        public async Task<IActionResult> GetOrderDropdown()
        {
            var orders = await _context.Orders
                .Select(c => new OrderDropdown
                {
                    OrderId = c.OrderId,
                    TotalAmount = c.TotalAmount ?? 0 // Handle null TotalAmount
                })
                .ToListAsync();
            return Ok(orders); // must return a valid list
        }
        #endregion
    }
}
