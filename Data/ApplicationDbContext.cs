using CarBuySel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarBuySel.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CarListing> CarListings => Set<CarListing>();
    public DbSet<CarImage> CarImages => Set<CarImage>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CarListing>()
            .HasOne(listing => listing.Owner)
            .WithMany(user => user.CarListings)
            .HasForeignKey(listing => listing.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CarListing>()
            .Property(listing => listing.Price)
            .HasColumnType("decimal(18,2)");

        builder.Entity<CarImage>()
            .HasOne(image => image.CarListing)
            .WithMany(listing => listing.Images)
            .HasForeignKey(image => image.CarListingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

