namespace KubeBreakfast.Contracts.Breakfast;
public record CreateBreakfastRequest(
    string Name,
    string Description,
    DateTime StartDateTime,
    DateTime EndDateTime,
    List<string> Meal,
    List<string> Sweet
);