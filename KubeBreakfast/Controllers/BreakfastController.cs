using Microsoft.AspNetCore.Mvc;
using KubeBreakfast.Contracts.Breakfast;
using KubeBreakfast.Models;
using KubeBreakfast.Services.Breakfasts;
using KubeBreakfast.ServiceErrors;
using ErrorOr;

namespace KubeBreakfast.Controllers;

public class BreakfastController : ApiController
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        // Validation:
        var requestToBreakfast = Breakfast.Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Meal,
            request.Sweet);
        
        if(requestToBreakfast.IsError)
            return Problem(requestToBreakfast.Errors);

        var breakfast = requestToBreakfast.Value;

        // Designing:
        ErrorOr<Created> createdBreakfast = _breakfastService.CreateBreakfast(breakfast);

        return createdBreakfast.Match(
            created => CreatedAtBreakfastResult(breakfast),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfast = _breakfastService.GetBreakfast(id);

        return getBreakfast.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors));
    }
    
    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var requestToBreakfast = Breakfast.Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.Meal,
            request.Sweet,
            id
        );

        if(requestToBreakfast.IsError)
            return Problem(requestToBreakfast.Errors);

        var breakfast = requestToBreakfast.Value;

        ErrorOr<UpsertedBreakfast> upsertedBreakfast = _breakfastService.UpsertBreakfast(breakfast);

        // TODO: return 201 if a new breakfast was created
        return upsertedBreakfast.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtBreakfastResult(breakfast) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        ErrorOr<Deleted> deletedBreakfast = _breakfastService.DeleteBreakfast(id);

        return deletedBreakfast.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }
    private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Meal,
            breakfast.Sweet);
    }
    private IActionResult CreatedAtBreakfastResult(Breakfast breakfast)
    {
        return CreatedAtAction(
            nameof(GetBreakfast),
            new { id = breakfast.Id },
            MapBreakfastResponse(breakfast));
    }
}
