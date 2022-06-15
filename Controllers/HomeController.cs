using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.AllDishes = _context.Dishes.OrderBy(a => a.CreatedAt).ToList();
        return View();
    }
    ////////////////////////
    [HttpGet("add")]
    public IActionResult Add()
    {
        return View();
    }
    ////////////////////////
    [HttpPost("add/dish")]
    public IActionResult AddDish(Dish newDish)
    {
        if( ModelState.IsValid )
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");

        } else {
            return View("add");
        } 
    }
    ////////////////////////
    ////////////////////////
    [HttpPost("update/{dishId}")]
    public IActionResult UpdateDish(int dishId, Dish UpdatedDish)
    {
        if( ModelState.IsValid )
        {
            Dish? OldDish = _context.Dishes.SingleOrDefault(a => a.DishId == dishId);
            OldDish.Chef = UpdatedDish.Chef;
            OldDish.Name = UpdatedDish.Name;
            OldDish.Calories = UpdatedDish.Calories;
            OldDish.Tastiness = UpdatedDish.Tastiness;
            OldDish.Description = UpdatedDish.Description;
            OldDish.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index");

        } else {
            return View($"edit/{dishId}");
        }
    }
    ////////////////////////
    [HttpGet("delete/{dishId}")]
    public IActionResult DeleteDish(int dishId)
    {
        Dish? DishToDelete = _context.Dishes.SingleOrDefault(a => a.DishId == dishId);      
        _context.Dishes.Remove(DishToDelete);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    ////////////////////////
    ////////////////////////
    [HttpGet("display/{dishId}")]
    public IActionResult DisplayDish(int dishId)
    {
        Dish? DishToDisplay = _context.Dishes.SingleOrDefault(a => a.DishId == dishId);      
        return View(DishToDisplay);
    }
    ////////////////////////
    ////////////////////////
    [HttpGet("edit/{dishId}")]
    public IActionResult EditDish(int dishId)
    {
        Dish? DishToEdit = _context.Dishes.SingleOrDefault(a => a.DishId == dishId);      
        return View(DishToEdit);
    }
    ////////////////////////

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
