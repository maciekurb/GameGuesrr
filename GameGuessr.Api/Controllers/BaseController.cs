using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace GameGuessr.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : Controller
{
    protected IActionResult HandleCommandResult(Result result) =>
        result.IsSuccess
            ? NoContent()
            : BadRequest(result.Error);

    protected IActionResult HandleCommandResult<T>(Result<T> result) =>
        result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    
    //TODO: Refactor this with CommonError
    protected IActionResult HandleFailure(Result result)
    {
        return BadRequest(result.Error);
    }
}
