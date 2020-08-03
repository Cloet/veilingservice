using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using veilingservice.Data;
using veilingservice.Helpers;
using veilingservice.Model;
using veilingservice.Security;

namespace veilingservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuctionsController : ControllerBase
    {
        private readonly VeilingContext _context;

        public AuctionsController(VeilingContext context)
        {
            _context = context;
        }

        // GET: api/Auctions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuction()
        {
            return await _context.Auction.ToListAsync();
        }

        private async Task<ActionResult<IEnumerable<Auction>>> GetAuctionByStatus(AuctionStatus status)
        {
            return await _context.Auction
                    .Where(x => x.Status == status)
                    .ToListAsync();
        }

        private async Task<ActionResult<Auction>> ChangeAuctionStatus(int id, AuctionStatus status) {
            var auction = await _context.Auction
                .Where(x => x.ID == id)
                .FirstOrDefaultAsync();

            if (auction == null)
                return NotFound();

            auction.Status = status;
            _context.Entry(auction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return auction;

        }


        // All auctions in edit mode.
        [HttpGet]
        [Route("/edit")]
        public async Task<ActionResult<IEnumerable<Auction>>> GetEditAuctions() => await GetAuctionByStatus(AuctionStatus.Edit);

        [HttpPost]
        [Route("{id}/edit")]
        public async Task<ActionResult<Auction>> PostAuctionToEdit(int id) => await ChangeAuctionStatus(id, AuctionStatus.Edit);

        [HttpGet]
        [Route("/active")]
        public async Task<ActionResult<IEnumerable<Auction>>> GetActiveAuctions() => await GetAuctionByStatus(AuctionStatus.Active);

        [HttpPost]
        [Route("{id}/active")]
        public async Task<ActionResult<Auction>> PostAuctionToActive(int id) => await ChangeAuctionStatus(id, AuctionStatus.Active);

        [HttpGet]
        [Route("/archive")]
        public async Task<ActionResult<IEnumerable<Auction>>> GetArchivedAuctions() => await GetAuctionByStatus(AuctionStatus.Archive);

        [HttpPost]
        [Route("{id}/archive")]
        public async Task<ActionResult<Auction>> PostAuctionToArchive(int id) => await ChangeAuctionStatus(id, AuctionStatus.Archive);

        [HttpGet]
        [Route("{id}/images/{imageId}")]
        public async Task<IActionResult> DownloadImageAuction(int id, int imageId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            var images = await _context.AuctionImage
                .Where(x => x.AuctionID == id && x.ID == imageId)
                .AsNoTracking()
                .ToListAsync();

            var path = Path.GetFullPath(@"C:\Users\Mathias\Downloads\Test.png");
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, FileContentType.GetContentType(path));
        }

        [HttpGet]
        [Route("{id}/images")]
        public async Task<ActionResult<IEnumerable<AuctionImage>>> GetAuctionImages(int id)
        {
            return await _context.AuctionImage
                .Where(x => x.AuctionID == id)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpPost]
        [Route("{id}/images")]
        public async Task<IActionResult> UploadImageAuction([FromForm] List<IFormFile> files, int id)
        {
            long size = files.Sum(f => f.Length);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var path = Path.GetTempFileName();

                    AuctionImage lastImage = null;

                    if (_context.AuctionImage.Count() > 0)
                        lastImage = await _context.AuctionImage
                            .OrderBy(x => x.ID)                    
                            .LastAsync();

                    int lastImageID = 1;
                    if (lastImage != null)
                        lastImageID = (lastImage.ID + 1);

                    using (var stream = System.IO.File.Create(path))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var newpath = VeilingConfiguration.AuctionImageLocation + $"/{id}/{lastImageID}{FileContentType.GetExtension(file.ContentType)}";
                    Directory.CreateDirectory(Path.GetDirectoryName(newpath));
                    System.IO.File.Delete(newpath);

                    _context.AuctionImage.Add(new AuctionImage() { AuctionID = id, ImageLocation = newpath, ID = lastImageID });
                    await _context.SaveChangesAsync();

                    System.IO.File.Move(path, newpath);
                    System.IO.File.Delete(path);
                }
            }

            return Ok(new { count = files.Count, size });
        }

        // GET: api/Auctions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Auction>> GetAuction(int id)
        {
            var auction = await _context.Auction.FindAsync(id);

            if (auction == null)
            {
                return NotFound();
            }

            return auction;
        }

        // PUT: api/Auctions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuction(int id, Auction auction)
        {
            if (id != auction.ID)
            {
                return BadRequest();
            }

            _context.Entry(auction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionExists(id))
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

        // POST: api/Auctions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Auction>> PostAuction(Auction auction)
        {
            _context.Auction.Add(auction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuction", new { id = auction.ID }, auction);
        }

        // DELETE: api/Auctions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Auction>> DeleteAuction(int id)
        {
            var auction = await _context.Auction.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            _context.Auction.Remove(auction);
            await _context.SaveChangesAsync();

            return auction;
        }

        private bool AuctionExists(int id)
        {
            return _context.Auction.Any(e => e.ID == id);
        }
    }
}
