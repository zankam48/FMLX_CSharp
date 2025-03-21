using Microsoft.AspNetCore.Mvc;
using WebApiVilla.Models;

namespace WebApiVilla.Controllers;

[Route("api/WebApiVilla")]
[ApiController]
public class VillaController : ControllerBase
{
    // attributenya tambahin di endpoint leve (disini), bukan di controller
    [HttpGet]
    public IEnumerable<Villa> GetVillas()
    {
        return new List<Villa> {
            new Villa{Id=1, Name="Argomulyo"},
            new Villa{Id=2, Name="Salatiga"}
        };
    } 
}

