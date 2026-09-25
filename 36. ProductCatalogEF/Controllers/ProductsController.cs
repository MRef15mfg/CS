using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogEF.Data;

namespace ProductCatalogEF.Controllers;

public class ProductsController : Controller
{
    private readonly ProductCatalogDbContext _db;

    public ProductsController(ProductCatalogDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _db.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            return NotFound();

        return View(product);
    }
}
