using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using veilingservice.Model;

namespace veilingservice.Security
{
    public interface IGetApiKeyQuery
    {
        Task<ApiKey> Execute(string providedHashedApiKey);
    }
}
