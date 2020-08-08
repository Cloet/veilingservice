using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veilingservice.Data;
using veilingservice.Helpers;
using veilingservice.Model;

namespace veilingservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LotsController : ControllerBase
    {
        private readonly VeilingContext _context;

        public LotsController(VeilingContext context)
        {
            _context = context;
        }

        // GET: api/Lots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lot>>> GetLot([FromQuery(Name = "auction")] string auctionId)
        {
            if (!string.IsNullOrEmpty(auctionId) && int.TryParse(auctionId, out var id))
            {
                return await _context.Lot
                    .Include(a => a.Images)
                    .Where(x => x.AuctionID == id).ToListAsync();
            }

            return await _context.Lot.ToListAsync();
        }

        [HttpGet]
        [Route("{id}/images/{imageId}")]
        public async Task<ActionResult<FileContentResult>> DownloadImageLot(int id, int imageId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            var images = await _context.LotImage
                .Where(x => x.LotID == id && x.ID == imageId)
                .AsNoTracking()
                .ToListAsync();

            var path = Path.GetFullPath(images.FirstOrDefault()?.ImageLocation);
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, FileContentType.GetContentType(path));
        }

        [HttpGet]
        [Route("{id}/images")]
        public async Task<ActionResult<IEnumerable<LotImage>>> GetLotImages(int id)
        {
            return await _context.LotImage
                .Where(x => x.LotID == id)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpPost]
        [Route("{id}/images")]
        public async Task<IActionResult> UploadImageLot([FromForm] List<IFormFile> files, int id)
        {
            long size = files.Sum(f => f.Length);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var path = Path.GetTempFileName();

                    LotImage lastImage = null;

                    if (_context.LotImage.Count() > 0)
                        lastImage = await _context.LotImage
                            .OrderBy(x => x.ID)
                            .LastAsync();

                    int lastImageID = 1;
                    if (lastImage != null)
                        lastImageID = (lastImage.ID + 1);

                    using (var stream = System.IO.File.Create(path))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var newpath = VeilingConfiguration.LotImageLocation + $"/{id}/{lastImageID}{FileContentType.GetExtension(file.ContentType)}";
                    Directory.CreateDirectory(Path.GetDirectoryName(newpath));
                    System.IO.File.Delete(newpath);

                    _context.LotImage.Add(new LotImage() { LotID = id, ImageLocation = newpath, ID = lastImageID });
                    await _context.SaveChangesAsync();

                    System.IO.File.Move(path, newpath);
                    System.IO.File.Delete(path);
                }
            }

            return Ok(new { count = files.Count, size });
        }

        // GET: api/Lots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lot>> GetLot(int id)
        {
            var lot = await _context.Lot
                .Include(a => a.Images)
                .Where(x => x.ID == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (lot == null)
            {
                return NotFound();
            }

            return lot;
        }

        // PUT: api/Lots/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLot(int id, Lot lot)
        {
            if (id != lot.ID)
            {
                return BadRequest();
            }

            _context.Entry(lot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Lots
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Lot>> PostLot(Lot lot)
        {
            _context.Lot.Add(lot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLot", new { id = lot.ID }, lot);
        }

        // DELETE: api/Lots/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lot>> DeleteLot(int id)
        {
            var lot = await _context.Lot.FindAsync(id);
            if (lot == null)
            {
                return NotFound();
            }

            _context.Lot.Remove(lot);
            await _context.SaveChangesAsync();

            return lot;
        }

        private bool LotExists(int id)
        {
            return _context.Lot.Any(e => e.ID == id);
        }
    }
}
