using InsuranceClaims.Application.DTOs;
using InsuranceClaims.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaims.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PolicyHolderController : ControllerBase
{
    private readonly IPolicyHolderService _service;

    public PolicyHolderController(IPolicyHolderService service)
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
                return NotFound($"PolicyHolder with Id {id} not found.");
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

    [HttpGet("{id:int}/policies")]
    public IActionResult GetPolicies(int id)
    {
        try
        {
            return Ok(_service.GetPolicies(id));
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
    public IActionResult CreateOrUpdate([FromBody] PolicyHolderDto dto)
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
