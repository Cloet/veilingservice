using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace veilingservice.Security
{
    public static class Policies
    {
        public const string Admin = nameof(Admin);
        public const string Write = nameof(Write);
        public const string Read = nameof(Read);
    }
}
