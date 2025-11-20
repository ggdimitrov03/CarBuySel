using Microsoft.AspNetCore.Identity;

namespace CarBuySel.Models;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }

    public ICollection<CarListing> CarListings { get; set; } = new List<CarListing>();
}

