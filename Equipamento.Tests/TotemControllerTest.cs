using AutoMapper;
using Equipamento.API.Controllers;
using Equipamento.API.Services;
using Equipamento.API.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Bson;
using Xunit;

namespace Equipamento.Tests;

public class TotemControllerTest
{
    private readonly Mock<ILogger<TotemController>> _logger = new();

    [Fact]
    public void CreateOnSuccessReturnStatusCode200()
    {
        var mockTotemService = new Mock<ITotemService>();

        var sut = new TotemController(_logger.Object, mockTotemService.Object);

        var result = (OkObjectResult)sut.Create(new TotemDTO
        {
            Localizacao = "Rio de Janeiro",
            Descricao = "um totem"
        });

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void EditOnSuccessReturnsStatusCode200()
    {

        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);

        var sut = new TotemController(_logger.Object, mockTotemService.Object);

        var result = (OkObjectResult)sut.Edit(new TotemDTO
        {
            Localizacao = "rio de janeiro",
            Descricao = "um totem"
        }, (It.IsAny<int>()));

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void EditOnFailureReturnsStatusCode404()
    {

        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TotemController(_logger.Object, mockTotemService.Object);

        var result = (NotFoundResult)sut.Edit(new TotemDTO
        {
            Localizacao = "rio de janeiro",
            Descricao = "um totem"
        }, (It.IsAny<int>()));

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void DeleteOnSuccessReturnStatusCode200() 
    {
        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockTotemService.Setup(service => service.DeleteTotem(It.IsAny<int>()));

        var sut = new TotemController(_logger.Object, mockTotemService.Object);

        var result = (OkResult)sut.Delete(It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void DeleteOnFailureReturnStatusCode404()
    {
        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TotemController(_logger.Object, mockTotemService.Object);

        var result = (NotFoundResult)sut.Delete(It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void GetAllOnSuccessReturnStatusCode200()
    {

        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.IsEmpty()).Returns(false);

        var sut = new TotemController(_logger.Object, mockTotemService.Object);

        var result = (OkObjectResult)sut.GetAll();

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetAllOnFailureReturnStatusCode404()
    {

        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.IsEmpty()).Returns(true);

        var sut = new TotemController(_logger.Object, mockTotemService.Object);

        var result = (NotFoundResult)sut.GetAll();

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void GetTrancasOnSuccessReturnStatusCode200()
    {

        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockTotemService.Setup(service => service.GetTrancas(It.IsAny<int>())).Returns(new List<TrancaModel>());

        var sut = new TotemController(_logger.Object, mockTotemService.Object);

        var result = (OkObjectResult)sut.GetTrancas(It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetTrancasOnFailureReturnStatusCode404()
    {

        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TotemController(_logger.Object, mockTotemService.Object);

        var result = (NotFoundResult)sut.GetTrancas(It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }



    [Fact]
    public void GetBicicletasOnSuccessReturnStatusCode200()
    {

        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);

        var sut = new TotemController(_logger.Object, mockTotemService.Object);

        var result = (OkObjectResult)sut.GetBicicletas(It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetBicicletasOnFailureReturnStatusCode404()
    {

        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TotemController(_logger.Object, mockTotemService.Object);

        var result = (NotFoundResult)sut.GetBicicletas(It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }

}