using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarBuySel.Models;

public class CarListing
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(40)]
    public string Make { get; set; } = string.Empty;

    [Required, StringLength(40)]
    public string Model { get; set; } = string.Empty;

    [Range(1950, 2100)]
    public int Year { get; set; }

    [Range(0, 1_000_000)]
    public decimal Price { get; set; }

    [Required, StringLength(4000)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string OwnerId { get; set; } = string.Empty;

    public ApplicationUser? Owner { get; set; }

    public ICollection<CarImage> Images { get; set; } = new List<CarImage>();

    [StringLength(200)]
    public string? SpecEngine { get; set; }

    [StringLength(200)]
    public string? SpecPower { get; set; }

    [StringLength(200)]
    public string? SpecEconomy { get; set; }

    [StringLength(200)]
    public string? SpecTransmission { get; set; }

    [StringLength(200)]
    public string? SpecExterior { get; set; }

    [StringLength(200)]
    public string? SpecInterior { get; set; }

    [StringLength(200)]
    public string? SpecAssist { get; set; }

    [StringLength(200)]
    public string? SpecConnectivity { get; set; }

    [StringLength(120)]
    public string? History1Title { get; set; }

    [StringLength(200)]
    public string? History1Subtitle { get; set; }

    [StringLength(60)]
    public string? History1Date { get; set; }

    [StringLength(120)]
    public string? History2Title { get; set; }

    [StringLength(200)]
    public string? History2Subtitle { get; set; }

    [StringLength(60)]
    public string? History2Date { get; set; }

    [StringLength(120)]
    public string? History3Title { get; set; }

    [StringLength(200)]
    public string? History3Subtitle { get; set; }

    [StringLength(60)]
    public string? History3Date { get; set; }

    [StringLength(200)]
    public string? Condition1 { get; set; }

    [StringLength(200)]
    public string? Condition2 { get; set; }

    [StringLength(200)]
    public string? Condition3 { get; set; }

    [StringLength(200)]
    public string? Condition4 { get; set; }
}

