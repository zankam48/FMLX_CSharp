namespace CobaMVC.Models;

public class User
{
    public string? Id {get; set;}
    public string? Name {get; set;}
    public string? Role {get; set;}
}

public enum Role
{
    Admin,
    Staff
}
