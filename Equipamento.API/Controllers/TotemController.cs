using AutoMapper;
using Equipamento.API.Services;
using Equipamento.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Equipamento.API.Controllers;

[ApiController]
[Route("totem")]
public class TotemController : ControllerBase
{
    private readonly ILogger<TotemController> _logger;
    
    private readonly ITotemService _totemService;

    public TotemController(ILogger<TotemController> logger, ITotemService totemService)
    {
        _logger = logger;
        _totemService = totemService;
    }

    [HttpPost]
    [Route("")]
    public IActionResult Create([FromBody] TotemInsertViewModel totem)
    {
        _logger.LogInformation("Criando Totem...");

        var result = _totemService.CreateTotem(totem);

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Edit([FromBody] TotemInsertViewModel totemNovo, int id)
    {
        _logger.LogInformation("Alterando totem...");

        if (_totemService.Contains(id))
        {
            var result = _totemService.UpdateTotem(totemNovo, id);
            return Ok(result);
        }
        else return NotFound();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deletando totem...");

        if (_totemService.Contains(id))
        {
            _totemService.DeleteTotem(id);
            return Ok();
        }
        else return NotFound();
    }

    [HttpGet]
    [Route("")]
    public IActionResult GetAll()
    {
        _logger.LogInformation("Retornando listas de bicicleta...");
        if (_totemService.IsEmpty())
        {
            return NotFound();
        }
        return Ok(_totemService.GetAll());
    }

    [HttpGet]
    [Route("{idTotem}/trancas")]
    public IActionResult GetTrancas(int idTotem) 
    {
        if (_totemService.Contains(idTotem))
        {
            return Ok(_totemService.GetTrancas(idTotem));
        }
        return NotFound();
    }

    [HttpGet]
    [Route("{idTotem}/bicicletas")]
    public IActionResult Getbicicletas(int idTotem)
    {
        if (_totemService.Contains(idTotem))
        {
            return Ok(_totemService.GetBicicletas(idTotem));
        }
        return NotFound();
    }
}
