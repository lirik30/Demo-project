using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFModel
{
    public class User
    {
        public User() => Comments = new List<Comment>();

        public int UserId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Image { get; set; }

        public int RoleId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
