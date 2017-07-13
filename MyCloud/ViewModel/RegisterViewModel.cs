using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyCloud.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Nazwa użytkownika powinna posiadać min. 3 znaki oraz max. 20.", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "Hasło powinno posiadać min. 8 znaków oraz max. 20.", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasło oraz jego potwierdzenie nie są takie same.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
