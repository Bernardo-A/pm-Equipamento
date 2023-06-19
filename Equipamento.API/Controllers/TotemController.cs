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

    private readonly IMapper _mapper;

    private readonly ITotemService _totemService;

    public TotemController(ILogger<TotemController> logger, IMapper mapper, ITotemService totemService)
    {
        _logger = logger;
        _mapper = mapper;
        _totemService = totemService;
    }

    [HttpPost]
    [Route("")]
    public IActionResult Create([FromBody] TotemInsertViewModel totem)
    {
        _logger.LogInformation("Criando Totem...");
        var result = _mapper.Map<TotemInsertViewModel, TotemViewModel>(totem);
        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Edit([FromBody] TotemInsertViewModel totemNovo, int id)
    {

        _logger.LogInformation("Alterando totem...");
        var totemAntigo = _totemService.GetTotem();
        var result = _mapper.Map(totemNovo, totemAntigo);
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deletando totem...");

        var totem = _totemService.GetTotem();
        totem.Id = id;
        totem.Status = "Excluida";
        return Ok();
    }
}
