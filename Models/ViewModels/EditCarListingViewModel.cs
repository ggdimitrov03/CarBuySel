using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CarBuySel.Models.ViewModels;

public class EditCarListingViewModel
{
    public int Id { get; set; }

    [Display(Name = "Заглавие")]
    [Required(ErrorMessage = "Моля, въведете заглавие")]
    [StringLength(80)]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Марка")]
    [Required(ErrorMessage = "Моля, въведете марка")]
    [StringLength(40)]
    public string Make { get; set; } = string.Empty;

    [Display(Name = "Модел")]
    [Required(ErrorMessage = "Моля, въведете модел")]
    [StringLength(40)]
    public string Model { get; set; } = string.Empty;

    [Display(Name = "Година")]
    [Range(1950, 2100)]
    public int Year { get; set; }

    [Display(Name = "Цена (€)")]
    [Range(0, 1_000_000)]
    public decimal Price { get; set; }

    [Display(Name = "Описание")]
    [Required(ErrorMessage = "Моля, добавете описание")]
    [StringLength(4000)]
    public string Description { get; set; } = string.Empty;

    public List<ExistingImageViewModel> ExistingImages { get; set; } = new();

    public List<int> ImagesToRemove { get; set; } = new();

    [Display(Name = "Добавете нови снимки (до лимита)")]
    public IList<IFormFile> NewImages { get; set; } = new List<IFormFile>();
}

