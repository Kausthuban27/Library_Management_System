using System.ComponentModel.DataAnnotations;

namespace Library_WebApp.Model
{
    public class AddUser
    { 
        [StringLength(20, ErrorMessage = "Username cannot be more than 20")]
        [Required(ErrorMessage ="Username is Mandatory")]
        public string username { get; set; } = null!;

        [StringLength(20, ErrorMessage = "Password cannot be more than 20")]
        [Required(ErrorMessage = "Password is Mandatory")]
        public string password { get; set; } = null!;
    }
}
