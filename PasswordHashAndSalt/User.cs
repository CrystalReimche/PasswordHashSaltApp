using System.ComponentModel.DataAnnotations;

namespace PasswordHashAndSalt
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
