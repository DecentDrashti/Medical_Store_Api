using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public BillApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllBills 
        [HttpGet]
        public IActionResult GetBill()
        {
            var bills = _context.Bills.ToList();
            return Ok(bills);
        }
        #endregion
        #region GetBillById 
        [HttpGet("{id}")]
        public IActionResult GetBillById(int id)
        {
            var bills = _context.Bills.Find(id);
            if (bills == null)
            {
                return NotFound();
            }
            return Ok(bills);
        }
        #endregion
        #region DeleteBillById 
        [HttpDelete("{id}")]
        public IActionResult DeleteBillById(int id)
        {
            var bills = _context.Bills.Find(id);
            if (bills == null)
            {
                return NotFound();
            }
            _context.Bills.Remove(bills);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertBill 
        [HttpPost]
        public IActionResult InsertBill(Bill bill)
        {
            _context.Bills.Add(bill);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateBill 
        [HttpPut("{id}")]
        public IActionResult UpdateBill(int id, Bill bill)
        {
            if (id != bill.BillId) // Ensure route ID matches the bill ID
            {
                return BadRequest();
            }
            var existingBill = _context.Bills.Find(id);
            if (existingBill == null)
            {
                return NotFound();
            }
            existingBill.InvoiceNumber = bill.InvoiceNumber;
            existingBill.CustomerId = bill.CustomerId;
            existingBill.BillDate = bill.BillDate;
            existingBill.PaymentMode= bill.PaymentMode;
            existingBill.DiscountAmount = bill.DiscountAmount;
            existingBill.TaxAmount= bill.TaxAmount;
            existingBill.GrandTotal = bill.GrandTotal;
            existingBill.IsPaid = bill.IsPaid;
            _context.Bills.Update(existingBill);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

    }
}
