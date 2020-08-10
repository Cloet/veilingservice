using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veilingservice.Data;
using veilingservice.Model;
using veilingservice.Security;

namespace veilingservice.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly VeilingContext _context;

        public UsersController(VeilingContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<PostMessage>> RegisterUser([FromForm] string email, [FromForm] string hashedpassword)
        {

            if (string.IsNullOrEmpty(email))
                return new PostMessage("The email is empty.");

            if (string.IsNullOrEmpty(hashedpassword))
                return new PostMessage("No password was defined.");

            if (!email.Contains("@") || email.Length < 10)
                return new PostMessage("Invalid email.");

            var user = await _context.Users.Where(x => x.Email.Trim() == email.Trim())
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user != null)
                return new PostMessage("There already exists a user with that email.");

            int lastUserId = 1;
            int lastApiId = 1;
            int lastRoleId = 1;

            var lastUser = await _context.Users.OrderByDescending(x => x.ID).FirstOrDefaultAsync();
            if (lastUser != null)
                lastUserId = lastUser.ID + 1;

            var lastApi = await _context.ApiKey.OrderByDescending(x => x.ID).FirstOrDefaultAsync();
            if (lastApi != null)
                lastApiId = lastApi.ID + 1;

            var lastRole = await _context.Role.OrderByDescending(x => x.ID).FirstOrDefaultAsync();
            if (lastRole != null)
                lastRoleId = lastRole.ID + 1;

            var newUser = new User(lastUserId, email, hashedpassword, lastApiId, lastRoleId);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new PostMessage();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<PostMessage>> Login([FromForm] string email, [FromForm] string hashedpassword)
        {
            var user = await _context.Users
                .Where(x => x.Email.Trim() == email.Trim())
                .AsNoTracking().FirstOrDefaultAsync();

            if (user == null)
                return new PostMessage($"No user with email {email.Trim()} exists.");

            if (user.Password != Hasher.GetHashString(hashedpassword))
            {
                return new PostMessage("Invalid password");
            }

            return new PostMessage();
        }

    }
}
