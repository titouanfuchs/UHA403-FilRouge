using Microsoft.AspNetCore.Mvc;
using printplan_api.Models.DTO;

namespace printplan_api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class PlanController : ControllerBase
{
    /// <summary>
    /// Créer une planification d'impression
    /// </summary>
    /// <param name="input">DTO planification</param>
    /// <returns>Estimation d'impression à la chaine</returns>
    /// <response code="201">Retourne l'estimation d'impression à la chaine</response>
    /// <response code="400">Les données d'entrée sont incorrectes</response>
    /// <remarks>
    /// Sample Request :
    /// 
    ///     POST /Plan
    ///     {
    ///         "quantity": 12,
    ///         "printModelId": 5 
    ///     }
    /// 
    /// </remarks>
    [HttpPost()]
    [ProducesResponseType(typeof(PrintPlanDto),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PrintPlanDto>> Plan(PostPrintPlanDto input)
    {
        return BadRequest(new BaseResponse() {Message = "Not Implemented"});
    }
}