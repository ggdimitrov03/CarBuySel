using System.ComponentModel.DataAnnotations;

namespace CarBuySel.Models;

public class CarImage
{
    public int Id { get; set; }

    [Required, StringLength(260)]
    public string Path { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public int CarListingId { get; set; }

    public CarListing? CarListing { get; set; }
}

