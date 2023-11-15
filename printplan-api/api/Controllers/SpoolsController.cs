using System.Net;
using Microsoft.AspNetCore.Mvc;
using printplan_api.Models.Core;
using printplan_api.Models.DTO;
using printplan_api.Models.DTO.Spools;
using printplan_api.Services;

namespace printplan_api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class SpoolsController : Controller
{
    private readonly SpoolsService _spoolsService;
    
    public SpoolsController(SpoolsService service)
    {
        _spoolsService = service;
    }
    
    [HttpDelete]
    public async Task<IActionResult> Clear()
    {
        await _spoolsService.ClearSpools();

        return Accepted(new BaseResponse { Message = "Spools cleared" });
    }
    
    [HttpPost("/Reset")]
    public async Task<IActionResult> Reset()
    {
        await _spoolsService.ResetSpools();

        return Accepted(new BaseResponse { Message = "Spools reseted" });
    }

    [HttpPost]
    public async Task<ActionResult<FilamentSpool>> CreateSpool(PostSpoolDto input)
    {
        FilamentSpool newSpool = await _spoolsService.CreateSpool(input);

        return Created("", newSpool);
    }
}