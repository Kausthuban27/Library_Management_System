using System.ComponentModel.DataAnnotations;

namespace Library_WebApp.Model
{
    public class GetExistingLibrarian
    {
        [StringLength(20, ErrorMessage = "Username cannot be more than 20")]
        [Required(ErrorMessage = "Username is Mandatory")]
        public string Username { get; set; } = null!;

        [StringLength(20, ErrorMessage = "Password cannot be more than 20")]
        [Required(ErrorMessage = "Password is Mandatory")]
        public string Password { get; set; } = null!;
    }
}
