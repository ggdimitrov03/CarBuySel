using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CarBuySel.Models.ViewModels;

public class CreateCarListingViewModel
{
    [Display(Name = "Заглавие")]
    [Required(ErrorMessage = "Моля, въведете заглавие")]
    [StringLength(80, ErrorMessage = "Заглавието може да бъде до 80 символа")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Марка")]
    [Required(ErrorMessage = "Моля, въведете марка")]
    [StringLength(40, ErrorMessage = "Максимум 40 символа")]
    public string Make { get; set; } = string.Empty;

    [Display(Name = "Модел")]
    [Required(ErrorMessage = "Моля, въведете модел")]
    [StringLength(40, ErrorMessage = "Максимум 40 символа")]
    public string Model { get; set; } = string.Empty;

    [Display(Name = "Година")]
    [Range(1950, 2100, ErrorMessage = "Моля, изберете валидна година")]
    public int Year { get; set; } = DateTime.UtcNow.Year;

    [Display(Name = "Цена (€)")]
    [Range(0, 1_000_000, ErrorMessage = "Моля, въведете валидна цена")]
    public decimal Price { get; set; }

    [Display(Name = "Описание")]
    [Required(ErrorMessage = "Моля, добавете описание")]
    [StringLength(4000, ErrorMessage = "Описание до 4000 символа")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Снимки (до 10 файла)")]
    public IList<IFormFile> ImageUploads { get; set; } = new List<IFormFile>();
}

