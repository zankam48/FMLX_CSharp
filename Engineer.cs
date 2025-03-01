using System; 

internal class Engineer
{
    private string _name;
    private int _id;
    protected string _project;

    public Engineer(int id, string name, string project)
    {
        this._id = id;
        this._name = name;
        this._project = project;
    }

    public virtual void DisplayDetails()
    {
        Console.WriteLine($"Engineer ID: {_id}, Name: {_name}, Project: {_project}");
    }

    public string Name { 
        get{ return _name;}
        set{
            _name = value;
        } }
    
    public int ID { 
        get{ return _id ;} 
        set{
            if (_id != value)
            {
                _id = value;
                Console.WriteLine("ID updated successfully.");
            }
            else
            {
                Console.WriteLine("ID is already set to this value.");
            };
        } }
    
    public string Project {
        get{ return _project; } 
        set{
            _project = value;
        } }
    
}