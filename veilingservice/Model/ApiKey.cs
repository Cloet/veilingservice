using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using veilingservice.Security;

namespace veilingservice.Model
{
    public class ApiKey
    {

        public int ID { get; private set; }
        public string Owner { get; private set; }
        public string Key { get; private set; }
        public DateTime Created { get; private set; }
        public IReadOnlyCollection<Role> Roles { get; private set; }

        public ApiKey(int id, string owner, string key, DateTime created, IReadOnlyCollection<Role> roles)
        {
            ID = id;
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Created = created;
            Roles = roles ?? throw new ArgumentNullException(nameof(key));
        }

        private ApiKey()
        {

        }

        public static string GenerateApiKey(string email, string hashedPassword) {
            var key = "";

            key += (email.Substring(0, email.Length >= 10 ? 10 : email.Length));
            key += (hashedPassword.Substring(0, hashedPassword.Length >= 10 ? 10 : hashedPassword.Length));

            return Hasher.GetHashString(key);
        }

    }
}
