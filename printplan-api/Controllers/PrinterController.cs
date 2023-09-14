using Microsoft.AspNetCore.Mvc;
using printplan_api.Models.DTO;
using printplan_api.Models.DTO.Printer;

namespace printplan_api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class PrinterController : Controller
{
    /// <summary>
    /// Modifier les paramètres d'une imprimante 3D
    /// </summary>
    /// <param name="id">Identifiant de la planification à modifier</param>
    /// <param name="input">Paramètres de l'imprimante</param>
    /// <response code="200">Le ou les paramètres de l'imprimante ont été modifiés</response>
    /// <response code="400">Les données d'entrée sont incorrectes</response>
    /// <response code="404">L'imprimante avec l'id fournis n'éxiste pas</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(PrinterSettingsDTO),StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePrinterSettings(int id, PrinterSettingsDTO input)
    {
        return BadRequest(new BaseResponse() {Message = "Not Implemented"});
    }
}