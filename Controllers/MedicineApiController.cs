using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public MedicineApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllMedicines 
        [HttpGet]
        public IActionResult GetMedicine()
        {
            var medicine = _context.Medicines.ToList();
            return Ok(medicine);
        }
        #endregion
        #region GetMedicineById 
        [HttpGet("{id}")]
        public IActionResult GetMedicineById(int id)
        {
            var medicine = _context.Medicines.Find(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return Ok(medicine);
        }
        #endregion
        #region DeleteMedicineById 
        [HttpDelete("{id}")]
        public IActionResult DeleteMedicineById(int id)
        {
            var medicine = _context.Medicines.Find(id);
            if (medicine == null)
            {
                return NotFound();
            }
            _context.Medicines.Remove(medicine);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertMedicine 
        [HttpPost]
        public IActionResult InsertMedicine(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateMedicine 
        [HttpPut("{id}")]
        public IActionResult UpdateMedicine(int id, Medicine medicine)
        {
            if (id != medicine.MedicineId) // Ensure route ID matches the medicine ID
            {
                return BadRequest();
            }
            var existingMedicine = _context.Medicines.Find(id);
            if (existingMedicine == null)
            {
                return NotFound();
            }
            existingMedicine.MedicineName = medicine.MedicineName;
            existingMedicine.CompanyId = medicine.CompanyId;
            existingMedicine.CategoryId = medicine.CategoryId;
            existingMedicine.Manufacturer = medicine.Manufacturer;
            existingMedicine.Type = medicine.Type;
            existingMedicine.MfgDate = medicine.MfgDate;
            existingMedicine.ExpiryDate = medicine.ExpiryDate;
            existingMedicine.Ptr = medicine.Ptr;
            existingMedicine.Mrp = medicine.Mrp;
            existingMedicine.Cdpercent = medicine.Cdpercent;
            existingMedicine.FreeQuantity = medicine.FreeQuantity;
            existingMedicine.IsPrescriptionRequired = medicine.IsPrescriptionRequired;
            _context.Medicines.Update(existingMedicine);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
