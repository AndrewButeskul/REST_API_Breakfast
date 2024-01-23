using KubeBreakfast.Models;

namespace KubeBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    // TODO: implement supporting EF Core or sth other
    private static readonly Dictionary<Guid, Breakfast> _breakfast = new();
    public void CreateBreakfast(Breakfast breakfast)
    {
        _breakfast.Add(breakfast.Id, breakfast);
    }

    public void DeleteBreakfast(Guid id)
    {
        _breakfast.Remove(id);
    }

    public Breakfast GetBreakfast(Guid id)
    {
        return _breakfast[id];
    }

    public void UpsertBreakfast(Breakfast breakfast)
    {
        _breakfast[breakfast.Id] = breakfast;
    }
}
