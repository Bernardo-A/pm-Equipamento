using AutoMapper;
using Equipamento.API.Services;
using Equipamento.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("bicicleta")]
public class BicicletaController : ControllerBase
{
    private readonly ILogger<BicicletaController> _logger;

    private readonly IMapper _mapper;

    private readonly IBicicletaService _bicicletaService;

    public BicicletaController(ILogger<BicicletaController> logger, IMapper mapper, IBicicletaService bicicletaService)
    {
        _logger = logger;
        _mapper = mapper;
        _bicicletaService = bicicletaService;
    }

    [HttpPost]
    [Route("")]
    public IActionResult Create([FromBody] BicicletaInsertViewModel bicicleta)
    {
        _logger.LogInformation("Criando bicicleta...");
        var result = _mapper.Map<BicicletaInsertViewModel, BicicletaViewModel>(bicicleta);
        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Edit([FromBody] BicicletaInsertViewModel bicicletaNovo, int id)
    {

        _logger.LogInformation("Alterando bicicleta...");
        var bicicletaAntigo = _bicicletaService.GetBicicleta();
        var result = _mapper.Map(bicicletaNovo, bicicletaAntigo);
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deletando bicicleta...");

        var bicicleta = _bicicletaService.GetBicicleta();
        bicicleta.Id = id;
        bicicleta.Status = "Excluida";
        return Ok();
    }
}
