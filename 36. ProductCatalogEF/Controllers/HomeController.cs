using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogEF.Data;

namespace ProductCatalogEF.Controllers;

public class HomeController : Controller
{
    private readonly ProductCatalogDbContext _db;

    public HomeController(ProductCatalogDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _db.Products
            .Include(p => p.Category)
            .OrderBy(p => p.Id)
            .ToListAsync();

        return View(products);
    }
}
