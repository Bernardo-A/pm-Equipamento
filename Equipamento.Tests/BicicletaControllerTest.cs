using Equipamento.API.Controllers;
using Equipamento.API.Services;
using Equipamento.API.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Equipamento.Tests;

public class BicicletaControllerTest
{

    private readonly Mock<ILogger<BicicletaController>> _logger = new();

    [Fact]
    public void CreateOnSuccessReturnStatuscode200()
    {
        var mockBicicletaService = new Mock<BicicletaService>();
        
        var sut = new BicicletaController(_logger.Object, mockBicicletaService.Object);

        var result = (OkObjectResult)sut.Create(new BicicletaDTO
        {
            Marca = "caloi",
            Modelo = "caloi 1000",
            Numero = "000",
            Ano = "2023",
            Status = "nova",
        });

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void EditOnSuccessReturnStatuscode200()
    {
        var mockBicicletaService = new Mock<BicicletaService>();
        mockBicicletaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);

        var sut = new BicicletaController(_logger.Object, mockBicicletaService.Object);

        var result = (OkObjectResult)sut.Edit(new BicicletaDTO
        {
            Marca = "caloi",
            Modelo = "caloi 1000",
            Numero = "000",
            Ano = "2023",
            Status = "nova",
        }, 0);

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void EditOnFailureReturnStatuscode403()
    {
        var mockBicicletaService = new Mock<BicicletaService>();
        mockBicicletaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new BicicletaController(_logger.Object, mockBicicletaService.Object);

        var result = (NotFoundResult)sut.Edit(new BicicletaDTO(), It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void DeleteOnSuccessReturnStatusCode200() 
    {
        var mockBicicletaService = new Mock<BicicletaService>();
        mockBicicletaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockBicicletaService.Setup(service => service.Deletebicicleta(It.IsAny<int>())).Returns(new BicicletaModel());

        var sut = new BicicletaController(_logger.Object, mockBicicletaService.Object);

        var result = (OkResult)sut.Delete(It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void DeleteWhenNotFoundReturnStatuscode404() 
    {
        var mockBicicletaService = new Mock<BicicletaService>();   
        mockBicicletaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new BicicletaController(_logger.Object, mockBicicletaService.Object);

        var result = (NotFoundResult)sut.Delete(It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void GetOnSuccessRetursStatusCode200()
    {
        var mockBicicletaService = new Mock<BicicletaService>();
        mockBicicletaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockBicicletaService.Setup(service => service.GetBicicleta(It.IsAny<int>())).Returns(new BicicletaModel());

        var sut = new BicicletaController(_logger.Object, mockBicicletaService.Object);

        var result = (OkObjectResult)sut.Get(It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetOnFailureRetursStatusCode404()
    {
        var mockBicicletaService = new Mock<BicicletaService>();
        mockBicicletaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new BicicletaController(_logger.Object, mockBicicletaService.Object);

        var result = (NotFoundResult)sut.Get(It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void ChangeStatusOnSuccessReturns200()
    {
        var mockBicicletaService = new Mock<BicicletaService>();
        mockBicicletaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockBicicletaService.Setup(service => service.ChangeStatus(It.IsAny<int>(), It.IsAny<string>())).Returns(new BicicletaModel());

        var sut = new BicicletaController(_logger.Object, mockBicicletaService.Object);

        var result = (OkObjectResult)sut.ChangeStatus(It.IsAny<int>(), It.IsAny<string>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void ChangeStatusOnFailureReturns200()
    {
        var mockBicicletaService = new Mock<BicicletaService>();
        mockBicicletaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new BicicletaController(_logger.Object, mockBicicletaService.Object);

        var result = (NotFoundResult)sut.ChangeStatus(It.IsAny<int>(), It.IsAny<string>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void GetAllOnSuccessReturns200()
    {
        var mockBicicletaService = new Mock<BicicletaService>();
        mockBicicletaService.Setup(service => service.IsEmpty()).Returns(false);
        mockBicicletaService.Setup(service => service.GetAll()).Returns(new List<BicicletaModel>());

        var sut = new BicicletaController(_logger.Object, mockBicicletaService.Object);

        var result = (OkObjectResult)sut.GetAll();

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetAllOnFailureReturns404()
    {
        var mockBicicletaService = new Mock<BicicletaService>();
        mockBicicletaService.Setup(service => service.IsEmpty()).Returns(true);

        var sut = new BicicletaController(_logger.Object, mockBicicletaService.Object);

        var result = (NotFoundResult)sut.GetAll();

        result.StatusCode.Should().Be(404);
    }


}