using Microsoft.AspNetCore.Mvc;

namespace CobaMVC.Controllers;

public class ItemController : Controller
{
    private readonly ILogger<ItemController> _logger;

    public ItemController(ILogger<ItemController> logger)
    {
        _logger = logger;
    }

    
}