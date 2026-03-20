using InsuranceClaims.Application.DTOs;
using InsuranceClaims.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaims.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ClaimSettlementController : ControllerBase
{
    private readonly IClaimSettlementService _service;

    public ClaimSettlementController(IClaimSettlementService service)
    {
        _service = service;
    }

    [HttpGet("{claimId:int}")]
    public IActionResult GetByClaimId(int claimId)
    {
        try
        {
            var result = _service.GetByClaimId(claimId);
            if (result is null)
                return NotFound($"Settlement for Claim Id {claimId} not found.");
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Process([FromBody] ClaimSettlementDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = _service.Process(dto);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
