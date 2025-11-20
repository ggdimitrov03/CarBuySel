namespace CarBuySel.Models.ViewModels;

public class CarSearchViewModel
{
    public IReadOnlyCollection<CarListing> Listings { get; init; } = Array.Empty<CarListing>();

    public string? SearchTerm { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public decimal? MaxPrice { get; set; }
}

