using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace InlämningBankApi.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly AdDbContext _dbContext;

        public AdController(AdDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult<Ad> CreateAd(Ad ad)
        {
            _dbContext.Ads.Add(ad);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetAd), new { id = ad.Id }, ad);
        }
        
        [HttpGet("{id}")]
        public ActionResult<Ad> GetAd(int id)
        {
            var ad = _dbContext.Ads.Find(id);

            if (ad == null)
            {
                return NotFound();
            }

            return ad;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Ad>> GetAds()
        {
            return _dbContext.Ads.ToList();
        }

        [HttpDelete("{id}")]
        public ActionResult<Ad> DeleteAd(int id)
        {
            var ad = _dbContext.Ads.Find(id);

            if (ad == null)
            {
                return NotFound();
            }

            _dbContext.Ads.Remove(ad);
            _dbContext.SaveChanges();

            return ad;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAd(int id, Ad ad)
        {
            if (id != ad.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(ad).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
