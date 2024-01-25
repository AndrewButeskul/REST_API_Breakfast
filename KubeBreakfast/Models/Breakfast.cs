using ErrorOr;
using KubeBreakfast.ServiceErrors;

namespace KubeBreakfast.Models;
public class Breakfast{
    public const int MinLengthForName = 2;
    public const int MaxLengthForName = 50;
    public const int MinLengthForDescription = 20;
    public const int MaxLengthForDescription = 150;
    public Guid Id {get;}
    public string Name {get;}
    public string Description { get;}
    public DateTime StartDateTime { get;}
    public DateTime EndDateTime { get;}
    public DateTime LastModifiedDateTime { get;}
    public List<string> Meal { get;}
    public List<string> Sweet { get;}

    private Breakfast(
        Guid id,
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime lastModifiedDateTime,
        List<string> meal,
        List<string> sweet)
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Meal = meal;
        Sweet = sweet;
    }

    public static ErrorOr<Breakfast> Create(string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        List<string> meal,
        List<string> sweet,
        Guid? id = null)
    {
        List<Error> errors = new();

        if(name.Length is > MaxLengthForName or < MinLengthForName)
            errors.Add(Errors.Breakfast.InvalidName);

        if(description.Length is > MaxLengthForDescription or < MinLengthForDescription)
            errors.Add(Errors.Breakfast.InvalidDescription);

        if(errors.Count > 0)
            return errors;

        return new Breakfast( id ?? Guid.NewGuid(),
            name, 
            description,
            startDateTime,
            endDateTime,
            DateTime.UtcNow,
            meal,
            sweet);
    }

}