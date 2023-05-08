using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace InlämningBankApi.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("[controller]")]

    public class AdController : ControllerBase
    {
        private readonly AdDbContext _dbContext;

        public AdController(AdDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<Ad> CreateAd(Ad ad)
        {
            _dbContext.Ads.Add(ad);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetAd), new { id = ad.Id }, ad);
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<Ad> GetAd(int id)
        {
            var ad = _dbContext.Ads.Find(id);

            if (ad == null)
            {
                return NotFound();
            }

            return ad;
        }


        // READ ALL ///////////////////////////////////////////////////////
        /// <summary>
        /// Retrieve ALL Ads from the database
        /// </summary>
        /// <returns>
        /// A full list of ALL Ads
        /// </returns>
        /// <remarks>
        /// Example end point: GET /api/Ads
        /// </remarks>
        /// <response code="200">
        /// Successfully returned a full list of ALL Ads
        /// </response>

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public ActionResult<IEnumerable<Ad>> GetAds()
        {
            return _dbContext.Ads.ToList();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

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
        [Authorize(Roles = "Admin")]

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

        [HttpPatch]
        [Authorize(Roles = "Admin")]

        [Route("{id}")]
        public async Task<ActionResult<Ad>>
        PatchHero(JsonPatchDocument hero, int id)
        {
            // OBS: PUT Uppdaterar SuperHero (VISSA properties)
            var adToUpdate = await
                _dbContext.Ads.FindAsync(id);

            if (adToUpdate == null)
            {
                return BadRequest("Ad not found");
            }

            hero.ApplyTo(adToUpdate);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.Ads.ToListAsync());
        }

    }
}
