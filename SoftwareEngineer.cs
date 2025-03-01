using System;

internal class SoftwareEngineer : Engineer
{
    private string _programmingLanguage;

    public SoftwareEngineer(int id, string name, string project, string programmingLanguage)
        : base(id, name, project)
    {
        this._programmingLanguage = programmingLanguage;
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();  // Call base class method
        Console.WriteLine($"Programming Language: {_programmingLanguage}");
    }

    public string ProgrammingLanguage
    {
        get { return _programmingLanguage; }
        set { _programmingLanguage = value; }
    }
}

