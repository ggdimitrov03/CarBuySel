using System.Collections.Generic;
using CarBuySel.Data;
using CarBuySel.Models;
using CarBuySel.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace CarBuySel.Controllers;

public class CarsController : Controller
{
    private const int MaxImagesPerListing = 10;

    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWebHostEnvironment _environment;

    public CarsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
    {
        _context = context;
        _userManager = userManager;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? searchTerm, string? make, string? model, int? year, decimal? maxPrice)
    {
        var query = _context.CarListings
            .Include(listing => listing.Owner)
            .Include(listing => listing.Images)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(listing =>
                listing.Title.Contains(searchTerm) ||
                listing.Description.Contains(searchTerm) ||
                listing.Make.Contains(searchTerm) ||
                listing.Model.Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(make))
        {
            query = query.Where(listing => listing.Make.Contains(make));
        }

        if (!string.IsNullOrWhiteSpace(model))
        {
            query = query.Where(listing => listing.Model.Contains(model));
        }

        if (year.HasValue)
        {
            query = query.Where(listing => listing.Year == year.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(listing => listing.Price <= maxPrice.Value);
        }

        var listings = await query
            .OrderByDescending(listing => listing.CreatedAt)
            .ToListAsync();

        var viewModel = new CarSearchViewModel
        {
            Listings = listings,
            SearchTerm = searchTerm,
            Make = make,
            Model = model,
            Year = year,
            MaxPrice = maxPrice
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateCarListingViewModel());
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCarListingViewModel input)
    {
        var uploads = FilterValidUploads(input.ImageUploads);

        if (uploads.Count > MaxImagesPerListing)
        {
            ModelState.AddModelError(nameof(CreateCarListingViewModel.ImageUploads),
                $"Можете да качите до {MaxImagesPerListing} снимки.");
        }

        if (!ModelState.IsValid)
        {
            return View(input);
        }

        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return Challenge();
        }

        var listing = new CarListing
        {
            Title = input.Title,
            Make = input.Make,
            Model = input.Model,
            Year = input.Year,
            Price = input.Price,
            Description = input.Description,
            OwnerId = user.Id,
            CreatedAt = DateTime.UtcNow,
            SpecEngine = input.SpecEngine,
            SpecPower = input.SpecPower,
            SpecEconomy = input.SpecEconomy,
            SpecTransmission = input.SpecTransmission,
            SpecExterior = input.SpecExterior,
            SpecInterior = input.SpecInterior,
            SpecAssist = input.SpecAssist,
            SpecConnectivity = input.SpecConnectivity,
            History1Title = input.History1Title,
            History1Subtitle = input.History1Subtitle,
            History1Date = input.History1Date,
            History2Title = input.History2Title,
            History2Subtitle = input.History2Subtitle,
            History2Date = input.History2Date,
            History3Title = input.History3Title,
            History3Subtitle = input.History3Subtitle,
            History3Date = input.History3Date,
            Condition1 = input.Condition1,
            Condition2 = input.Condition2,
            Condition3 = input.Condition3,
            Condition4 = input.Condition4
        };

        for (var index = 0; index < uploads.Count; index++)
        {
            var imagePath = await SaveImageAsync(uploads[index]);
            listing.Images.Add(new CarImage
            {
                Path = imagePath,
                SortOrder = index
            });
        }

        _context.CarListings.Add(listing);
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "Обявата ви е активна!";

        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> MyListings()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            return Challenge();
        }

        var listings = await _context.CarListings
            .Include(listing => listing.Images)
            .Where(listing => listing.OwnerId == user.Id)
            .OrderByDescending(listing => listing.CreatedAt)
            .ToListAsync();

        return View(listings);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var listing = await _context.CarListings
            .Include(car => car.Owner)
            .Include(car => car.Images)
            .FirstOrDefaultAsync(car => car.Id == id);

        if (listing is null)
        {
            return NotFound();
        }

        return View(listing);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var listing = await GetOwnedListingAsync(id);

        if (listing is null)
        {
            return NotFound();
        }

        var viewModel = new EditCarListingViewModel
        {
            Id = listing.Id,
            Title = listing.Title,
            Make = listing.Make,
            Model = listing.Model,
            Year = listing.Year,
            Price = listing.Price,
            Description = listing.Description,
            SpecEngine = listing.SpecEngine,
            SpecPower = listing.SpecPower,
            SpecEconomy = listing.SpecEconomy,
            SpecTransmission = listing.SpecTransmission,
            SpecExterior = listing.SpecExterior,
            SpecInterior = listing.SpecInterior,
            SpecAssist = listing.SpecAssist,
            SpecConnectivity = listing.SpecConnectivity,
            History1Title = listing.History1Title,
            History1Subtitle = listing.History1Subtitle,
            History1Date = listing.History1Date,
            History2Title = listing.History2Title,
            History2Subtitle = listing.History2Subtitle,
            History2Date = listing.History2Date,
            History3Title = listing.History3Title,
            History3Subtitle = listing.History3Subtitle,
            History3Date = listing.History3Date,
            Condition1 = listing.Condition1,
            Condition2 = listing.Condition2,
            Condition3 = listing.Condition3,
            Condition4 = listing.Condition4,
            ExistingImages = listing.Images
                .OrderBy(image => image.SortOrder)
                .Select(image => new ExistingImageViewModel
                {
                    Id = image.Id,
                    Path = image.Path
                })
                .ToList()
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditCarListingViewModel input)
    {
        var listing = await GetOwnedListingAsync(input.Id);

        if (listing is null)
        {
            return NotFound();
        }

        var uploads = FilterValidUploads(input.NewImages);
        var imagesToRemove = input.ImagesToRemove?.Distinct().ToList() ?? new List<int>();

        var remainingImages = listing.Images.Count(image => !imagesToRemove.Contains(image.Id));

        if (remainingImages + uploads.Count > MaxImagesPerListing)
        {
            var availableSlots = Math.Max(0, MaxImagesPerListing - remainingImages);
            ModelState.AddModelError(nameof(EditCarListingViewModel.NewImages),
                availableSlots == 0
                    ? "Премахнете снимка преди да качите нова."
                    : $"Можете да добавите още {availableSlots} снимки.");
        }

        if (!ModelState.IsValid)
        {
            input.ExistingImages = listing.Images
                .OrderBy(image => image.SortOrder)
                .Select(image => new ExistingImageViewModel
                {
                    Id = image.Id,
                    Path = image.Path
                })
                .ToList();

            return View(input);
        }

        listing.Title = input.Title;
        listing.Make = input.Make;
        listing.Model = input.Model;
        listing.Year = input.Year;
        listing.Price = input.Price;
        listing.Description = input.Description;
        listing.SpecEngine = input.SpecEngine;
        listing.SpecPower = input.SpecPower;
        listing.SpecEconomy = input.SpecEconomy;
        listing.SpecTransmission = input.SpecTransmission;
        listing.SpecExterior = input.SpecExterior;
        listing.SpecInterior = input.SpecInterior;
        listing.SpecAssist = input.SpecAssist;
        listing.SpecConnectivity = input.SpecConnectivity;
        listing.History1Title = input.History1Title;
        listing.History1Subtitle = input.History1Subtitle;
        listing.History1Date = input.History1Date;
        listing.History2Title = input.History2Title;
        listing.History2Subtitle = input.History2Subtitle;
        listing.History2Date = input.History2Date;
        listing.History3Title = input.History3Title;
        listing.History3Subtitle = input.History3Subtitle;
        listing.History3Date = input.History3Date;
        listing.Condition1 = input.Condition1;
        listing.Condition2 = input.Condition2;
        listing.Condition3 = input.Condition3;
        listing.Condition4 = input.Condition4;

        if (imagesToRemove.Count > 0)
        {
            var removable = listing.Images
                .Where(image => imagesToRemove.Contains(image.Id))
                .ToList();

            foreach (var image in removable)
            {
                DeleteImage(image.Path);
                listing.Images.Remove(image);
                _context.CarImages.Remove(image);
            }
        }

        var sortOrder = listing.Images.Count == 0
            ? 0
            : listing.Images.Max(image => image.SortOrder) + 1;

        foreach (var upload in uploads)
        {
            var imagePath = await SaveImageAsync(upload);
            listing.Images.Add(new CarImage
            {
                Path = imagePath,
                SortOrder = sortOrder++
            });
        }

        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "Обявата беше обновена.";

        return RedirectToAction(nameof(MyListings));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var listing = await GetOwnedListingAsync(id);

        if (listing is null)
        {
            return NotFound();
        }

        foreach (var image in listing.Images.ToList())
        {
            DeleteImage(image.Path);
        }

        _context.CarListings.Remove(listing);
        await _context.SaveChangesAsync();

        TempData["StatusMessage"] = "Обявата е изтрита.";

        return RedirectToAction(nameof(MyListings));
    }

    private async Task<string> SaveImageAsync(IFormFile file)
    {
        var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
        Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/images/{fileName}";
    }

    private static List<IFormFile> FilterValidUploads(IList<IFormFile>? files)
    {
        return files?
                   .Where(file => file is not null && file.Length > 0)
                   .ToList()
               ?? new List<IFormFile>();
    }

    private async Task<CarListing?> GetOwnedListingAsync(int id)
    {
        var userId = _userManager.GetUserId(User);

        if (string.IsNullOrWhiteSpace(userId))
        {
            return null;
        }

        return await _context.CarListings
            .Include(listing => listing.Images)
            .FirstOrDefaultAsync(listing => listing.Id == id && listing.OwnerId == userId);
    }

    private void DeleteImage(string? relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            return;
        }

        var sanitizedPath = relativePath.TrimStart('/', '\\');
        var physicalPath = Path.Combine(_environment.WebRootPath, sanitizedPath.Replace('/', Path.DirectorySeparatorChar));

        if (System.IO.File.Exists(physicalPath))
        {
            try
            {
                System.IO.File.Delete(physicalPath);
            }
            catch
            {
                // Ignore cleanup errors.
            }
        }
    }
}

