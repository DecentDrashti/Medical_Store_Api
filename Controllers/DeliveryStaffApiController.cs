using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryStaffApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public DeliveryStaffApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllDeliveryStaff 
        [HttpGet]
        public IActionResult GetDeliveryStaff()
        {
            var deliverystaff = _context.DeliveryStaffs.ToList();
            return Ok(deliverystaff);
        }
        #endregion
        #region GetDeliveryStaffById 
        [HttpGet("{id}")]
        public IActionResult GetDeliveryStaffById(int id)
        {
            var deliverystaff = _context.DeliveryStaffs.Find(id);
            if (deliverystaff == null)
            {
                return NotFound();
            }
            return Ok(deliverystaff);
        }
        #endregion
        #region DeleteDeliveryStaffById 
        [HttpDelete("{id}")]
        public IActionResult DeleteDeliveryStaffById(int id)
        {
            var deliverystaff = _context.DeliveryStaffs.Find(id);
            if (deliverystaff == null)
            {
                return NotFound();
            }
            _context.DeliveryStaffs.Remove(deliverystaff);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertDeliveryStaff 
        [HttpPost]
        public IActionResult InsertDeliveryStaff(DeliveryStaff deliverystaff)
        {
            _context.DeliveryStaffs.Add(deliverystaff);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateDelivery 
        [HttpPut("{id}")]
        public IActionResult UpdateDeliveryStaff(int id, DeliveryStaff deliverystaff)
        {
            if (id != deliverystaff.StaffId) // Ensure route ID matches the deliverystaff ID
            {
                return BadRequest();
            }
            var existingDeliveryStaff = _context.DeliveryStaffs.Find(id);
            if (existingDeliveryStaff == null)
            {
                return NotFound();
            }
            existingDeliveryStaff.FullName = deliverystaff.FullName;
            existingDeliveryStaff.ContactNumber = deliverystaff.ContactNumber;
            existingDeliveryStaff.Address = deliverystaff.Address;
            existingDeliveryStaff.JoinedDate = deliverystaff.JoinedDate;
            existingDeliveryStaff.IsActive = deliverystaff.IsActive;
            _context.DeliveryStaffs.Update(existingDeliveryStaff);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
