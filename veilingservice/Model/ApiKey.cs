using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
}
