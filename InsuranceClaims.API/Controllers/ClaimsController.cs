using InsuranceClaims.Application.DTOs;
using InsuranceClaims.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaims.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ClaimsController : ControllerBase
{
    private readonly IClaimsService _service;

    public ClaimsController(IClaimsService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_service.GetAll());
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

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var result = _service.GetById(id);
            if (result is null)
                return NotFound($"Claim with Id {id} not found.");
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
    public IActionResult Submit([FromBody] ClaimDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = _service.Submit(dto);
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

    [HttpPost("{id:int}/lineitems")]
    public IActionResult AddLineItem(int id, [FromBody] ClaimLineItemDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (dto.ClaimId != 0 && dto.ClaimId != id)
            return BadRequest($"Route id {id} does not match body ClaimId {dto.ClaimId}.");

        dto.ClaimId = id;

        try
        {
            var result = _service.AddLineItem(dto);
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

    [HttpPost("{id:int}/assess")]
    public IActionResult Assess(int id, [FromBody] ClaimAssessmentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (dto.ClaimId != 0 && dto.ClaimId != id)
            return BadRequest($"Route id {id} does not match body ClaimId {dto.ClaimId}.");

        dto.ClaimId = id;

        try
        {
            var result = _service.Assess(dto);
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

    [HttpGet("{id:int}/assessment")]
    public IActionResult GetAssessment(int id)
    {
        try
        {
            var result = _service.GetAssessment(id);
            if (result is null)
                return NotFound($"Assessment for Claim Id {id} not found.");
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
