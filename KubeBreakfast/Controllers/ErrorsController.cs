
using Microsoft.AspNetCore.Mvc;

namespace KubeBreakfast.Controllers;
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}