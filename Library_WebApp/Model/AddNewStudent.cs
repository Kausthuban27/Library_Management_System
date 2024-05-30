using System.ComponentModel.DataAnnotations;

namespace Library_WebApp.Model
{
    public class AddNewStudent
    {
        [StringLength(50, ErrorMessage = "Firstname cannot be more than 50")]
        [Required(ErrorMessage = "Firstname is Mandatory")]
        public string Firstname { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Lastname cannot be more than 20")]
        [Required(ErrorMessage = "Lastname is Mandatory")]
        public string Lastname { get; set; } = null!;

        [StringLength(20, ErrorMessage = "Username cannot be more than 20")]
        [Required(ErrorMessage ="Username is Mandatory")]
        public string Username { get; set; } = null!;

        [StringLength(20, ErrorMessage = "Password cannot be more than 20")]
        [Required(ErrorMessage = "Password is Mandatory")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Department is Mandatory")]
        public string Department { get; set; } = null!;

        [Required(ErrorMessage = "Year is Mandatory")]
        public int Year { get; set; }
    }
}
