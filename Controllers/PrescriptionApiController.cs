using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public PrescriptionApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllprescription 
        [HttpGet]
        public IActionResult GetPrescription()
        {
            var order = _context.Prescriptions.ToList();
            return Ok(order);
        }
        #endregion
        #region DeleteOrderById 
        [HttpDelete("{id}")]
        public IActionResult DeletePrescriptionById(int id)
        {
            var order = _context.Prescriptions.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Prescriptions.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertOrder 
        [HttpPost]
        public IActionResult InsertOrder(Prescription order)
        {
            _context.Prescriptions.Add(order);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateOrder 
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Prescription order)
        {
            if (id != order.PrescriptionId) // Ensure route ID matches the order ID
            {
                return BadRequest();
            }
            var existingPrescription = _context.Prescriptions.Find(id);
            if (existingPrescription == null)
            {
                return NotFound();
            }

            existingPrescription.CustomerId = order.CustomerId;
            existingPrescription.MedicineId = order.MedicineId;
            existingPrescription.FilePath = order.FilePath;
            existingPrescription.UploadedOn = order.UploadedOn;
            existingPrescription.IsApproved = order.IsApproved;

            _context.Prescriptions.Update(existingPrescription);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
