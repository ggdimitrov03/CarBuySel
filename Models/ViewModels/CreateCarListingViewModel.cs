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

    // Specs
    [Display(Name = "Двигател")]
    public string? SpecEngine { get; set; }

    [Display(Name = "Мощност")]
    public string? SpecPower { get; set; }

    [Display(Name = "Разход")]
    public string? SpecEconomy { get; set; }

    [Display(Name = "Трансмисия")]
    public string? SpecTransmission { get; set; }

    [Display(Name = "Екстериор")]
    public string? SpecExterior { get; set; }

    [Display(Name = "Интериор")]
    public string? SpecInterior { get; set; }

    [Display(Name = "Асистенти")]
    public string? SpecAssist { get; set; }

    [Display(Name = "Свързаност")]
    public string? SpecConnectivity { get; set; }

    // History
    [Display(Name = "История 1 - заглавие")]
    public string? History1Title { get; set; }

    [Display(Name = "История 1 - описание")]
    public string? History1Subtitle { get; set; }

    [Display(Name = "История 1 - дата")]
    public string? History1Date { get; set; }

    [Display(Name = "История 2 - заглавие")]
    public string? History2Title { get; set; }

    [Display(Name = "История 2 - описание")]
    public string? History2Subtitle { get; set; }

    [Display(Name = "История 2 - дата")]
    public string? History2Date { get; set; }

    [Display(Name = "История 3 - заглавие")]
    public string? History3Title { get; set; }

    [Display(Name = "История 3 - описание")]
    public string? History3Subtitle { get; set; }

    [Display(Name = "История 3 - дата")]
    public string? History3Date { get; set; }

    // Condition checklist
    [Display(Name = "Състояние 1")]
    public string? Condition1 { get; set; }

    [Display(Name = "Състояние 2")]
    public string? Condition2 { get; set; }

    [Display(Name = "Състояние 3")]
    public string? Condition3 { get; set; }

    [Display(Name = "Състояние 4")]
    public string? Condition4 { get; set; }
}

