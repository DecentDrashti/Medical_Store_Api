using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        #region categorydropdown
        [HttpGet("dropdown/Category")]
        public async Task<IActionResult> GetCategoryDropdown()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDropdown
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                })
                .ToListAsync();

            return Ok(categories); // must return a valid list
        }
        #endregion

        #region companydropdown
        [HttpGet("dropdown/Company")]
        public async Task<IActionResult> GetCompanyDropdown()
        {
            var companies = await _context.Companies
                .Select(c => new CompanyDropdown
                {
                    CompanyId = c.CompanyId,
                    CompanyName = c.CompanyName
                })
                .ToListAsync();

            return Ok(companies); // must return a valid list
        }
        #endregion

        #region Filtered Medicines
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Medicine>>> Filter(
         [FromQuery] string? name,
         [FromQuery] string? companyName,
         [FromQuery] string? categoryName,
         [FromQuery] string? manufacturer,
         [FromQuery] string? type)
        {
            var query = _context.Medicines
                .Include(m => m.Company)   // assumes you have Company entity
                .Include(m => m.Category)  // assumes you have Category entity
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(m => m.MedicineName.Contains(name));

            if (!string.IsNullOrEmpty(companyName))
                query = query.Where(m => m.Company != null &&
                                         m.Company.CompanyName.Contains(companyName));

            if (!string.IsNullOrEmpty(categoryName))
                query = query.Where(m => m.Category != null &&
                                         m.Category.CategoryName.Contains(categoryName));

            if (!string.IsNullOrEmpty(manufacturer))
                query = query.Where(m => m.Manufacturer != null &&
                                         m.Manufacturer.Contains(manufacturer));

            if (!string.IsNullOrEmpty(type))
                query = query.Where(m => m.Type != null && m.Type.Contains(type));

            return await query.ToListAsync();
        }

        #endregion
    }
}
