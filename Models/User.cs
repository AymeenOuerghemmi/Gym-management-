using System.ComponentModel.DataAnnotations;

namespace GymMGT.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
    }
}
