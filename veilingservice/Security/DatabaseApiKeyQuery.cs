using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using veilingservice.Data;
using veilingservice.Model;

namespace veilingservice.Security
{
    public class DatabaseApiKeyQuery: IGetApiKeyQuery
    {
        private readonly VeilingContext _context;

        public DatabaseApiKeyQuery(VeilingContext context)
        {
            _context = context;
        }

        //public DatabaseApiKeyQuery()
        //{
        //    var existingApiKeys = new List<ApiKey>
        //    {

        //        new ApiKey(1, "Finance", "C5BFF7F0-B4DF-475E-A331-F737424F013C", new DateTime(2019, 01, 01),
        //            new List<Role>
        //            {
        //                new Role { ApiKeyID = 1, ID = 1, Title = "Admin"},
        //            })
        //    };

        //    _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
        //}

        public async Task<ApiKey> Execute(string providedApiKey)
        {
            var hashedKey = Hasher.GetHashString(providedApiKey);

            return await _context.ApiKey
                .Include(r => r.Roles)
                .Where(x => x.Key == hashedKey)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
