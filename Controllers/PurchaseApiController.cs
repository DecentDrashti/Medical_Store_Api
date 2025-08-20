using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public PurchaseApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllPurchases 
        [HttpGet]
        public IActionResult GetPurchase()
        {
            var purchase = _context.Purchases.ToList();
            return Ok(purchase);
        }
        #endregion
        #region GetPurchaseById 
        [HttpGet("{id}")]
        public IActionResult GetPurchaseById(int id)
        {
            var purchase = _context.Purchases.Find(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return Ok(purchase);
        }
        #endregion
        #region DeletePurchaseById 
        [HttpDelete("{id}")]
        public IActionResult DeletePurchaseById(int id)
        {
            var purchase = _context.Purchases.Find(id);
            if (purchase == null)
            {
                return NotFound();
            }
            _context.Purchases.Remove(purchase);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertPurchase 
        [HttpPost]
        public IActionResult InsertPurchase(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdatePurchase 
        [HttpPut("{id}")]
        public IActionResult UpdatePurchase(int id, Purchase purchase)
        {
            if (id != purchase.PurchaseId) // Ensure route ID matches the purchase ID
            {
                return BadRequest();
            }
            var existingPurchase = _context.Purchases.Find(id);
            if (existingPurchase == null)
            {
                return NotFound();
            }

            existingPurchase.SupplierId = purchase.SupplierId;
            existingPurchase.PurchaseDate = purchase.PurchaseDate;
            existingPurchase.TotalAmount = purchase.TotalAmount;
            existingPurchase.CreatedBy = purchase.CreatedBy;

            _context.Purchases.Update(existingPurchase);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
    }
}
