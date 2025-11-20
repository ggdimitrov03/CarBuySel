using System.ComponentModel.DataAnnotations;

namespace CarBuySel.Models.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Имейл")]
    [Required(ErrorMessage = "Моля, въведете имейл"), EmailAddress(ErrorMessage = "Моля, въведете валиден имейл")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Парола")]
    [Required(ErrorMessage = "Моля, въведете парола"), DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Запомни ме")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}

