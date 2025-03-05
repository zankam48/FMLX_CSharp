using System;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}

public class UserLogin : User
{
    public string Nickname { get; set; }
    
    private double _primogem;
    private double _gold;
    private int _pity;
    public double Primogem { get; set; }
    public double Gold { get; set; }

    public static void GetInfo(){
    }
    public void SaveGame(){}

}
