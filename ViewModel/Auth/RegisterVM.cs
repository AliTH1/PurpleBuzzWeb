using System.ComponentModel.DataAnnotations;

namespace PurpleBuzzWeb.ViewModel.Auth
{
    public class RegisterVM
    {
        [Required, MaxLength(60)]
        public string FullName { get; set; }
        [Required, MaxLength(30)]
        public string Username { get; set; }
        [Required, MaxLength(255), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MinLength(8), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MinLength(8), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
