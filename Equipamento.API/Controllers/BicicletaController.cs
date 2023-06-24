using AutoMapper;
using Equipamento.API.Services;
using Equipamento.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Equipamento.API.Controllers;

[ApiController]
[Route("bicicleta")]
public class BicicletaController : ControllerBase
{
    private readonly ILogger<BicicletaController> _logger;

    private readonly IBicicletaService _bicicletaService;

    public BicicletaController(ILogger<BicicletaController> logger, IBicicletaService bicicletaService)
    {
        _logger = logger;
        _bicicletaService = bicicletaService;
    }

    [HttpPost]
    [Route("")]
    public IActionResult Create([FromBody] BicicletaInsertViewModel bicicleta)
    {
        _logger.LogInformation("Criando bicicleta...");
        var result = _bicicletaService.CreateBicicleta(bicicleta);
        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Edit([FromBody] BicicletaInsertViewModel bicicletaNovo, int id)
    {

        _logger.LogInformation("Alterando bicicleta...");
        if (_bicicletaService.Contains(id))
        {
            var result = _bicicletaService.UpdateBicicleta(bicicletaNovo, id);
            return Ok(result);
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deletando bicicleta...");

        if (_bicicletaService.Contains(id))
        {
            _bicicletaService.Deletebicicleta(id);
            return Ok();
        }
        return NotFound();
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation("Buscando bicicleta...");
        if (_bicicletaService.Contains(id))
        {
            var result = _bicicletaService.GetBicicleta(id);
            return Ok(result);
        }
        return NotFound();

    }

    [HttpPost]
    [Route("{id}/status/{statusEnum}")]
    public IActionResult ChangeStatus(int id, string status)
    {
        _logger.LogInformation("Alterando status da bicicleta...");
        if(_bicicletaService.Contains(id))
        {
            var result = _bicicletaService.ChangeStatus(id, status);
            return Ok(result);
        }
        else { return NotFound(); }
    }

    [HttpGet]
    [Route("")]
    public IActionResult GetAll()
    {
        _logger.LogInformation("Retornando listas de bicicleta...");
        if (_bicicletaService.isEmpty())
        {
            return NotFound();
        }
        return Ok(_bicicletaService.GetAll());
    }

    //[HttpPost]
    //[Route("integrarNaRed")] 
    //public IActionResult Retrive(){
}

