namespace CobaMVC.Models;

public class Item
{
    public string? Id {get; set;}
    public string? Name {get; set;}
    public int? Qty {get; set;}
}

public enum ItemStatus
{
    Available,
    Borrowed,
    Broken,
    Lost
}