using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
