using System.ComponentModel.DataAnnotations;

namespace Library_WebApp.Model
{
    public class GetUser
    {
        [StringLength(20, ErrorMessage = "Firstname cannot be more than 100")]
        [Required(ErrorMessage = "Firstname is Mandatory")]
        public string firstname { get; set; } = null!;

        [StringLength(20, ErrorMessage = "Lastname cannot be more than 20")]
        [Required(ErrorMessage = "Lastname is Mandatory")]
        public string lastname { get; set; } = null!;

        [StringLength(20, ErrorMessage = "Username cannot be more than 20")]
        [Required(ErrorMessage = "Username is Mandatory")]
        public string username { get; set; } = null!;

        [StringLength(20, ErrorMessage = "Password cannot be more than 20")]
        [Required(ErrorMessage = "Password is Mandatory")]
        public string password { get; set; } = null!;

        [Required(ErrorMessage = "Department is Mandatory")]
        public string department { get; set; } = null!;

        public int year { get; set; } 
    }
}
