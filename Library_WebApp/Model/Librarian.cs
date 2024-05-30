using System.ComponentModel.DataAnnotations;

namespace Library_WebApp.Model
{
    public class Librarian
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

        [StringLength(100, ErrorMessage = "Email cannot be more than 100")]
        [Required(ErrorMessage = "Email is Mandatory")]
        public string email { get; set; } = null!;

        [StringLength(20, ErrorMessage = "Password cannot be more than 20")]
        [Required(ErrorMessage = "Password is Mandatory")]
        public string password { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is Mandatory")]
        [RegularExpression(@"^\(?\d{3}?\)??-??\(?\d{3}?\)??-??\(?\d{4}?\)??-?$", ErrorMessage = "Phone number should be in 111-111-1111 format")]
        [StringLength(10, ErrorMessage = "Phone number cannot be more than 10")]
        public string phone { get; set; } = null!;
    }
}
