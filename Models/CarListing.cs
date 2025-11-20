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
}

