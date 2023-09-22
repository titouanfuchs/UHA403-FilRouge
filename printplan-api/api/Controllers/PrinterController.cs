using Microsoft.AspNetCore.Mvc;
using printplan_api.Models.DTO;
using printplan_api.Models.DTO.Printer;
using printplan_api.Services;

namespace printplan_api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class PrinterController : Controller
{
    private readonly PrinterService _printerService;
    public PrinterController(PrinterService printerService)
    {
        _printerService = printerService;
    }
    
    /// <summary>
    /// Modifier les paramètres d'une imprimante 3D
    /// </summary>
    /// <param name="id">Identifiant de la planification à modifier</param>
    /// <param name="input">Paramètres de l'imprimante</param>
    /// <response code="200">Aucunes modifications aux paramètres de l'imprimante</response>
    /// <response code="202">Le ou les paramètres de l'imprimante ont été modifiés</response>
    /// <response code="400">Les données d'entrée sont incorrectes</response>
    /// <response code="404">L'imprimante avec l'id fournis n'éxiste pas</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(PrinterSettingsDTO),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PrinterSettingsDTO),StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePrinterSettings(int id, PrinterSettingsDTO input)
    {
        try
        {
            var result = await _printerService.PatchSettings(id, input);
            return Accepted(result);
        }
        catch (NullReferenceException ne)
        {
            return NotFound(new BaseResponse() { Message = ne.Message });

        }
        catch (ArgumentException ae)
        {
            return BadRequest(new BaseResponse() { Message = ae.Message });
        }
        catch (Exception e)
        {
            return Ok(new BaseResponse() { Message = e.Message });
        }
    }
}