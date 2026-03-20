using InsuranceClaims.Application.DTOs;
using InsuranceClaims.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaims.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class InsurancePolicyController : ControllerBase
{
    private readonly IInsurancePolicyService _service;

    public InsurancePolicyController(IInsurancePolicyService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var result = _service.GetById(id);
            if (result is null)
                return NotFound($"InsurancePolicy with Id {id} not found.");
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

    [HttpGet("holder/{holderId:int}")]
    public IActionResult GetByHolderId(int holderId)
    {
        try
        {
            return Ok(_service.GetByHolderId(holderId));
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

    [HttpGet("{id:int}/claims")]
    public IActionResult GetClaims(int id)
    {
        try
        {
            return Ok(_service.GetClaims(id));
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
    public IActionResult CreateOrUpdate([FromBody] InsurancePolicyDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = _service.CreateOrUpdate(dto);
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
