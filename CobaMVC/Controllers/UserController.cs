using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CobaMVC.Models;

namespace CobaMVC.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetUser()
    {
        return View();
    }

    // [HttpPost]
    // public IActionResult 
}
