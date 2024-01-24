using ErrorOr;
using KubeBreakfast.Models;
using KubeBreakfast.ServiceErrors;

namespace KubeBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    // TODO: implement supporting EF Core or sth other
    private static readonly Dictionary<Guid, Breakfast> _breakfast = new();
    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        _breakfast.Add(breakfast.Id, breakfast);
        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakfast.Remove(id);
        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        if(_breakfast.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }
        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast)
    {
        var IsNewlyCreated = !_breakfast.ContainsKey(breakfast.Id);
        _breakfast[breakfast.Id] = breakfast;
        return new UpsertedBreakfast(IsNewlyCreated);
    }
}
