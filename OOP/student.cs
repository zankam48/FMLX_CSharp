public class Student
{
    private int _id;
    public string Name { get; set; }
    public string Major { get; set; }

    private double _gpa = 0.0f;
    private double GPA { get => _gpa; 
        set {
            if (value >= 0 && value <= 4.0)
                _gpa = value;
            else
                throw new ArgumentException("GPA must be between 0 and 4.0.");
        }}
    
    public Student(string name, string major, double gpa) { 
        Name = name;
        Major = major;
        GPA = gpa;
    }

    public string GetInfo()
    {
        return $"Name: {Name}, Major: {Major}, GPA: {GPA}";
    }
}

// class UndergraduateStudent : Student
// {
    
// }

// class GraduateStudent : Student
// {

// }

