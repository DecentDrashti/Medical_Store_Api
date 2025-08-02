using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public SupplierApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllSuppliers 
        [HttpGet]
        public IActionResult GetSupplier()
        {
            var supplier = _context.Suppliers.ToList();
            return Ok(supplier);
        }
        #endregion
        #region GetSupplierById 
        [HttpGet("{id}")]
        public IActionResult GetSupplierById(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }
        #endregion
        #region DeleteSupplierById 
        [HttpDelete("{id}")]
        public IActionResult DeleteSupplierById(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertSupplier 
        [HttpPost]
        public IActionResult InsertSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateSupplier 
        [HttpPut("{id}")]
        public IActionResult UpdateSupplier(int id, Supplier supplier)
        {
            if (id != supplier.SupplierId) // Ensure route ID matches the supplier ID
            {
                return BadRequest();
            }
            var existingSupplier = _context.Suppliers.Find(id);
            if (existingSupplier == null)
            {
                return NotFound();
            }

            existingSupplier.SupplierName = supplier.SupplierName;
            existingSupplier.ContactNumber = supplier.ContactNumber;
            existingSupplier.Email = supplier.Email;
            existingSupplier.Address = supplier.Address;

            _context.Suppliers.Update(existingSupplier);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
