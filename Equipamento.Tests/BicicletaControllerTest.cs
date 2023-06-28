using Equipamento.API.Controllers;
using Equipamento.API.Services;
using Equipamento.API.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace equipamento.tests;

public class BicicletaControllerTest
{

    private readonly ILogger<BicicletaController> _logger;

    [Fact]
    public void CreateonSucessReturnStatuscode200()
    {
        var mockBicicletaService = new Mock<BicicletaService>();
        

        var sut = new BicicletaController(_logger, mockBicicletaService.Object);

        var result = (OkObjectResult)sut.Create(new BicicletaViewModel
        {
            Marca = "caloi",
            Modelo = "caloi 1000",
            Numero = "000",
            Ano = "2023",
            Status = "nova",
        });

        result.StatusCode.Should().Be(200);
    }

    //[fact]
    //public void editonsucessreturnstatuscode200()
    //{

    //    var mockbicicletaservice = new mock<ibicicletaservice>();
    //    mockbicicletaservice.setup(service => service.getbicicleta()).returns(new bicicletaviewmodel());

    //    var sut = new bicicletacontroller(_logger.object, _mapper.object, mockbicicletaservice.object);

    //    var result = (okobjectresult)sut.edit(new bicicletainsertviewmodel
    //    {
    //        marca = "caloi",
    //        modelo = "caloi 1000",
    //        numero = "000",
    //        ano = "2023",
    //        status = "nova",
    //    }, 0);

    //    result.statuscode.should().be(200);
    //}

    //[fact]
    //public void deleteonsucessreturnstatuscode200()
    //{
    //    var mockbicicletaservice = new mock<ibicicletaservice>();
    //    mockbicicletaservice.setup(service => service.getbicicleta()).returns(new bicicletaviewmodel());

    //    var sut = new bicicletacontroller(_logger.object, _mapper.object, mockbicicletaservice.object);

    //    var result = (okresult)sut.delete(0);

    //    result.statuscode.should().be(200);
    //}

    //[fact]
    //public void bicicletaservicegetbicicletareturnsbicicleta()
    //{
    //    var sut = new bicicletaservice();

    //    var result = sut.getbicicleta();

    //    assert.istype<bicicletaviewmodel>(result);
    //}

    //[fact]
    //public void testbicicletaviewmodelget()
    //{
    //    var bicicletaservice = new bicicletaservice(); ;

    //    var bicicleta = bicicletaservice.getbicicleta();

    //    var result = new bicicletaviewmodel();
    //    {
    //        result.id = bicicleta.id;
    //        result.ano = bicicleta.ano;
    //        result.marca = bicicleta.marca;
    //        result.modelo = bicicleta.modelo;
    //        result.numero = bicicleta.numero;
    //        result.status = bicicleta.status;
    //    }

    //    result.should().beequivalentto(bicicleta);
    //}

    //[fact]
    //public void testbicicletainsertviewmodelget()
    //{
    //    var bicicleta = new bicicletainsertviewmodel
    //    {
    //        marca = "caloi",
    //        modelo = "caloi 1000",
    //        numero = "000",
    //        ano = "2023",
    //        status = "nova",
    //    };

    //    var result = new bicicletainsertviewmodel();
    //    {
    //        result.ano = bicicleta.ano;
    //        result.marca = bicicleta.marca;
    //        result.modelo = bicicleta.modelo;
    //        result.numero = bicicleta.numero;
    //        result.status = bicicleta.status;
    //    }

    //    result.should().beequivalentto(bicicleta);
    //}


}