using AutoMapper;
using Equipamento.API.Services;
using Equipamento.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Equipamento.API.Controllers;

[ApiController]
[Route("tranca")]
public class TrancaController : ControllerBase
{
    private readonly ILogger<TrancaController> _logger;
    private readonly ITrancaService _trancaService;

    public TrancaController(ILogger<TrancaController> logger, ITrancaService trancaService)
    {
        _logger = logger;
        _trancaService = trancaService; 
    }

    [HttpPost]
    [Route("")]
    public IActionResult Create([FromBody] TrancaInsertViewModel tranca)
    {
        _logger.LogInformation("Criando Tranca...");
        var result = _trancaService.CreateTranca(tranca);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation("Buscando tranca...");
        if (_trancaService.Contains(id))
        {
            var result = _trancaService.GetTranca(id);
            return Ok(result);
        }
        return NotFound();
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Edit([FromBody] TrancaInsertViewModel trancaNovo, int id)
    {

        _logger.LogInformation("Alterando tranca...");
        if (_trancaService.Contains(id))
        {
            var result = _trancaService.UpdateTranca(trancaNovo, id);
            return Ok(result);
        }
        return NotFound();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deletando tranca...");

        if (_trancaService.Contains(id))
        {
            _trancaService.DeleteTranca(id);
            return Ok();
        }
        else return NotFound();
    }

    [HttpGet]
    [Route("{id}/bicicleta")]
    public IActionResult GetBicicleta(int id)
    {
        if (_trancaService.Contains(id))
        {
            var tranca = _trancaService.GetTranca(id);
            var bicicleta = _trancaService.GetBicicleta(tranca.Id);
            if (bicicleta == null)
            {
                return NotFound();
            }
            return Ok(bicicleta);    
        }
        return NotFound();
    }

    [HttpPost]
    [Route("{id}/status/{acao}")]
    public IActionResult ChangeStatus(int id, string acao)
    {
        if(_trancaService.Contains(id))
        {
            var result = _trancaService.ChangeStatus(id, acao);
            return Ok(result);
        }
        else { return NotFound(); }
    }

    [HttpPost]
    [Route("{id}/destrancar")]
    public IActionResult Unlock([FromBody] int bicicleta ,int id)
    {
        if(_trancaService.Contains(id))
        {
            var result = _trancaService.Unlock(bicicleta, id);
            return Ok(result);
        }
        return NotFound();  
    }

    [HttpPost]
    [Route("{id}/trancar")]
    public IActionResult Lock([FromBody] int? bicicleta, int id)
    {
        if(_trancaService.Contains(id))
        {
            var result = _trancaService.Lock(bicicleta, id);
            return Ok(result);
        }
        return NotFound();  
    }

    [HttpPost]
    [Route("integrarNaRede")]
    public IActionResult AddToTotem([FromBody] TrancaRedeViewModel viewModel)
    {
        if (_trancaService.Contains(viewModel.TrancaId))
        {
            var tranca = _trancaService.GetTranca(viewModel.TrancaId);
            _trancaService.AddTrancaToTotem(tranca, viewModel.TotemId);
            return Ok();
        }
        return NotFound();
    }

    [HttpPost]
    [Route("retirarDaRede")]
    public IActionResult RemoveFromTotem([FromBody] TrancaRedeViewModel viewModel)
    {
        if (_trancaService.Contains(viewModel.TrancaId))
        {
            var tranca = _trancaService.GetTranca(viewModel.TrancaId);
            _trancaService.RemoveTrancaFromTotem(tranca, viewModel.TotemId);
            return Ok();
        }
        return NotFound();
    }

    [HttpPost]
    [Route("/bicicleta/integrarNaRede")]
    public IActionResult AddBicicletaToTranca([FromBody] BicicletaRedeAddViewModel viewModel)
    {
        if (_trancaService.Contains(viewModel.TrancaId))
        {
            var result = _trancaService.AddBicicletaToTranca(viewModel);
            if(result == null)
            {
                return BadRequest();
            }
        }
        return Ok();
    }
    [HttpPost]
    [Route("/bicicleta/retirarDaRede")]
    public IActionResult RemoveBicicletaFromTranca([FromBody] BicicletaRemoveViewModel viewModel)
    {
        if (_trancaService.Contains(viewModel.TrancaId))
        {
            var result = _trancaService.RemoveBicicletaFromTranca(viewModel);
            if (result == null)
            {
                return BadRequest();
            }
        }
        return Ok();
    }

}