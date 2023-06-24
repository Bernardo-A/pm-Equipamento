//using AutoMapper;
//using Equipamento.API.Controllers;
//using Equipamento.API.Services;
//using Equipamento.API.ViewModels;
//using FluentAssertions;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using Newtonsoft.Json.Bson;
//using Xunit;

//namespace Equipamento.Tests;

//public class BicicletaTest
//{
//    private readonly Mock<ILogger<BicicletaController>> _logger = new();
//    private readonly Mock<IMapper> _mapper = new();

//    [Fact]
//    public void CreateOnSucessReturnStatusCode200()
//    {
//        var mockBicicletaService = new Mock<IBicicletaService>();

//        var sut = new BicicletaController(_logger.Object, _mapper.Object, mockBicicletaService.Object);

//        var result = (OkObjectResult)sut.Create(new BicicletaInsertViewModel
//        {
//            Marca = "Caloi",
//            Modelo = "Caloi 1000",
//            Numero = "000",
//            Ano = "2023",
//            Status = "nova",
//        });

//        result.StatusCode.Should().Be(200);
//    }

//    [Fact]
//    public void EditOnSucessReturnStatusCode200()
//    {

//        var mockBicicletaService = new Mock<IBicicletaService>();
//        mockBicicletaService.Setup(service => service.GetBicicleta()).Returns(new BicicletaViewModel());

//        var sut = new BicicletaController(_logger.Object, _mapper.Object, mockBicicletaService.Object);
        
//        var result = (OkObjectResult)sut.Edit(new BicicletaInsertViewModel
//        {
//            Marca = "Caloi",
//            Modelo = "Caloi 1000",
//            Numero = "000",
//            Ano = "2023",
//            Status = "nova",
//        }, 0);

//        result.StatusCode.Should().Be(200);
//    }

//    [Fact]
//    public void DeleteOnSucessReturnStatusCode200()
//    {
//        var mockBicicletaService = new Mock<IBicicletaService>();
//        mockBicicletaService.Setup(service => service.GetBicicleta()).Returns(new BicicletaViewModel());

//        var sut = new BicicletaController(_logger.Object, _mapper.Object, mockBicicletaService.Object);

//        var result = (OkResult)sut.Delete(0);

//        result.StatusCode.Should().Be(200);
//    }

//    [Fact]
//    public void BicicletaServiceGetBicicletaReturnsBicicleta()
//    {
//        var sut = new BicicletaService();

//        var result = sut.GetBicicleta();

//        Assert.IsType<BicicletaViewModel>(result);
//    }

//    [Fact]
//    public void TestBicicletaViewModelGet()
//    {
//        var bicicletaService = new BicicletaService(); ;

//        var bicicleta = bicicletaService.GetBicicleta();

//        var result = new BicicletaViewModel();
//        {
//            result.Id = bicicleta.Id;
//            result.Ano = bicicleta.Ano;
//            result.Marca = bicicleta.Marca;
//            result.Modelo = bicicleta.Modelo;
//            result.Numero = bicicleta.Numero;
//            result.Status = bicicleta.Status;
//        }

//        result.Should().BeEquivalentTo(bicicleta);
//    }

//    [Fact]
//    public void TestBicicletaInsertViewModelGet()
//    {
//        var bicicleta = new BicicletaInsertViewModel
//        {
//            Marca = "Caloi",
//            Modelo = "Caloi 1000",
//            Numero = "000",
//            Ano = "2023",
//            Status = "nova",
//        };

//        var result = new BicicletaInsertViewModel();
//        {
//            result.Ano = bicicleta.Ano;
//            result.Marca = bicicleta.Marca;
//            result.Modelo = bicicleta.Modelo;
//            result.Numero = bicicleta.Numero;
//            result.Status = bicicleta.Status;
//        }

//        result.Should().BeEquivalentTo(bicicleta);
//    }


//}