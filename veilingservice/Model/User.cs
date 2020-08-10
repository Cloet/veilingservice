using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using veilingservice.Data;
using veilingservice.Security;

namespace veilingservice.Model
{
    public class User
    {

        public int ID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ApiKey Key { get; set; }

        public User(int id, string mail, string password, int lastApiId, int lastRoleId)
        {
            ID = id;
            Email = mail;
            Password = Hasher.GetHashString(password);
            var collection = new List<Role>();
            collection.Add(new Role() { ApiKeyID = lastApiId, Title = "User", ID = lastRoleId });
            Key = new ApiKey(lastApiId, Email, Hasher.GetHashString(ApiKey.GenerateApiKey(mail,password)) , DateTime.Now, collection);
        }

        private User()
        {

        }

    }
}
