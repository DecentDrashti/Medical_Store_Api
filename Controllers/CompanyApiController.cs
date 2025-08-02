using Medical_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyApiController : ControllerBase
    {
        #region Configuration Fields 
        private readonly MedicalStoreContext _context;
        public CompanyApiController(MedicalStoreContext context)
        {
            _context = context;
        }
        #endregion
        #region GetAllCompanys 
        [HttpGet]
        public IActionResult GetCompany()
        {
            var Company = _context.Companies.ToList();
            return Ok(Company);
        }
        #endregion
        #region GetCompanyById 
        [HttpGet("{id}")]
        public IActionResult GetCompanyById(int id)
        {
            var Company = _context.Companies.Find(id);
            if (Company == null)
            {
                return NotFound();
            }
            return Ok(Company);
        }
        #endregion
        #region DeleteCompanyById 
        [HttpDelete("{id}")]
        public IActionResult DeleteCompanyById(int id)
        {
            var Company = _context.Companies.Find(id);
            if (Company == null)
            {
                return NotFound();
            }
            _context.Companies.Remove(Company);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region InsertCompany 
        [HttpPost]
        public IActionResult InsertCompany(Company Company)
        {
            _context.Companies.Add(Company);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion
        #region UpdateCompany 
        [HttpPut("{id}")]
        public IActionResult UpdateCompany(int id, Company Company)
        {
            if (id != Company.CompanyId) // Ensure route ID matches the Company ID
            {
                return BadRequest();
            }
            var existingCompany = _context.Companies.Find(id);
            if (existingCompany == null)
            {
                return NotFound();
            }
            existingCompany.CompanyName = Company.CompanyName;
            _context.Companies.Update(existingCompany);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion

    }
}
