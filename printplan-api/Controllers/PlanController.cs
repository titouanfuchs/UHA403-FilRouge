using Microsoft.AspNetCore.Mvc;
using printplan_api.Models.DTO;

namespace printplan_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanController : ControllerBase
{
    [HttpPost()]
    public ActionResult<PrintPlanDto> Plan(PostPrintPlanDto Input)
    {
        return Ok();
    }
}