using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyStore.WebApp.Models;

using MyStore.Services;
using MyStore.Business.Entities;

namespace MyStore.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
    {
        _logger = logger;
        _productService = productService;
        _categoryService = categoryService;
    }

    public IActionResult Index()
    {
        var products = _productService.GetAllProducts();
        var categories = _categoryService.GetAllCategories();
        
        ViewBag.FeaturedProducts = products.Take(8).ToList();
        ViewBag.Categories = categories.ToList();
        
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
