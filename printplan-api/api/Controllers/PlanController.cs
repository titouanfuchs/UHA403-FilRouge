using Microsoft.AspNetCore.Mvc;
using printplan_api.Models.DTO;

namespace printplan_api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class PlanController : ControllerBase
{
    /// <summary>
    ///     Récupération de toutes les plannifications en base de données
    /// </summary>
    /// <returns>Toutes les Planifications en base de données</returns>
    /// <response code="200">Retourne l'estimation d'impression à la chaine</response>
    [HttpGet]
    [ProducesResponseType(typeof(PrintPlanDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<PrintPlanDto>> GetPlans()
    {
        return BadRequest(new BaseResponse { Message = "Not Implemented" });
    }

    /// <summary>
    ///     Créer une planification d'impression
    /// </summary>
    /// <param name="input">DTO planification</param>
    /// <returns>Estimation d'impression à la chaine</returns>
    /// <response code="201">Retourne l'estimation d'impression à la chaine</response>
    /// <response code="400">Les données d'entrée sont incorrectes</response>
    [HttpPost]
    [ProducesResponseType(typeof(PrintPlanDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PrintPlanDto>> Plan(PostPrintPlanDto input)
    {
        return BadRequest(new BaseResponse { Message = "Not Implemented" });
    }

    /// <summary>
    ///     Modifier les paramètres d'une planification
    /// </summary>
    /// <param name="id">Identifiant de la planification à modifier</param>
    /// <param name="input">DTO planification</param>
    /// <returns>Estimation d'impression à la chaine</returns>
    /// <response code="202">Retourne l'estimation d'impression à la chaine modifiée et à jour</response>
    /// <response code="400">Les données d'entrée sont incorrectes</response>
    /// <response code="404">La planification avec l'id fournis n'éxiste pas</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(PrintPlanDto), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PrintPlanDto>> ModifyPlan(PostPrintPlanDto input, int id)
    {
        return BadRequest(new BaseResponse { Message = "Not Implemented" });
    }

    /// <summary>
    ///     Supprimer / Annuler une planification existante
    /// </summary>
    /// <param name="id">Identifiant de la planification à modifier</param>
    /// <response code="200">Retourne l'estimation d'impression à la chaine</response>
    /// <response code="404">La planification avec l'id fournis n'éxiste pas</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BaseResponse>> DeletePlan(int id)
    {
        return BadRequest(new BaseResponse { Message = "Not Implemented" });
    }
}