using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public StockApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllStocks 
        [HttpGet]
        public IActionResult GetStock()
        {
            var stock = _context.Stocks.ToList();
            return Ok(stock);
        }
        #endregion
        #region GetStockById 
        [HttpGet("{id}")]
        public IActionResult GetStockById(int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
        #endregion
        #region DeleteStockById 
        [HttpDelete("{id}")]
        public IActionResult DeleteStockById(int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertStock 
        [HttpPost]
        public IActionResult InsertStock(Stock stock)
        {
            _context.Stocks.Add(stock);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateStock 
        [HttpPut("{id}")]
        public IActionResult UpdateStock(int id, Stock stock)
        {
            if (id != stock.StockId) // Ensure route ID matches the stock ID
            {
                return BadRequest();
            }
            var existingStock = _context.Stocks.Find(id);
            if (existingStock == null)
            {
                return NotFound();
            }

            existingStock.MedicineId = stock.MedicineId;
            existingStock.BatchNo = stock.BatchNo;
            existingStock.QuantityAvailable = stock.QuantityAvailable;
            existingStock.ExpiryDate = stock.ExpiryDate;

            _context.Stocks.Update(existingStock);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

        #region MedicineDropDown
        // Get all Users (for dropdown)
        [HttpGet("dropdown/Medicine")]
        public async Task<ActionResult<IEnumerable<object>>> GetMedicines()
        {
            return await _context.Medicines
                .Select(c => new { c.MedicineId, c.MedicineName })
                .ToListAsync();
        }
        #endregion

        // Advanced Filtering Endpoint
        //#region FilterStocks
        //[HttpGet("filter")]
        //public async Task<ActionResult<IEnumerable<Stock>>> Filter(
        //    [FromQuery] string? MedicineName,
        //    //[FromQuery] string? QuantityAvailable,
        //    [FromQuery] string? ExpiryDate)
        //{
        //    var query = _context.Stocks
        //        .Include(c => c.Medicine) // optional, if you want user info
        //        .AsQueryable();

        //    if (!string.IsNullOrEmpty(MedicineName))
        //        query = query.Where(c => c.Medicine != null &&
        //                                 c.Medicine.MedicineName.Contains(MedicineName));

        //    //if (!string.IsNullOrEmpty(QuantityAvailable))
        //    //    query = query.Where(c => c.QuantityAvailable!=0 && c.QuantityAvailable.Contains(QuantityAvailable));

        //    if (!string.IsNullOrEmpty(ExpiryDate))
        //        query = query.Where(c => c.ExpiryDate != null && c.ExpiryDate.Contains(ExpiryDate));

        //    return await query.ToListAsync();
        //}
        //#endregion
    }
}
