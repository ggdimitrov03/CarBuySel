using System.ComponentModel.DataAnnotations;

namespace CarBuySel.Models.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "Име за показване")]
    [Required(ErrorMessage = "Моля, въведете име"), StringLength(50, ErrorMessage = "До 50 символа")]
    public string DisplayName { get; set; } = string.Empty;

    [Display(Name = "Имейл")]
    [Required(ErrorMessage = "Имейлът е задължителен"), EmailAddress(ErrorMessage = "Невалиден имейл")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Парола")]
    [Required(ErrorMessage = "Моля, въведете парола"), DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Паролата трябва да е поне 6 символа")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Потвърдете паролата")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Паролите не съвпадат")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

