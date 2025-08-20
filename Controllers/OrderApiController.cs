using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public OrderApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllOrders 
        [HttpGet]
        public IActionResult GetOrder()
        {
            var order = _context.Orders.ToList();
            return Ok(order);
        }
        #endregion
        #region GetOrderById 
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        #endregion
        #region DeleteOrderById 
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderById(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertOrder 
        [HttpPost]
        public IActionResult InsertOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateOrder 
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {
            if (id != order.OrderId) // Ensure route ID matches the order ID
            {
                return BadRequest();
            }
            var existingOrder = _context.Orders.Find(id);
            if (existingOrder == null)
            {
                return NotFound();
            }
            
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.Feedback = order.Feedback;
            existingOrder.Remarks = order.Remarks;
            existingOrder.CreatedBy = order.CreatedBy;
            
            _context.Orders.Update(existingOrder);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
