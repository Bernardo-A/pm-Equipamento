using Equipamento.API.Controllers;
using Equipamento.API.Services;
using Equipamento.API.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Equipamento.Tests;

public class TrancaControllerTest
{

    private readonly Mock<ILogger<TrancaController>> _logger = new();

    [Fact]
    public void CreateOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        
        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.Create(It.IsAny<TrancaDto>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetOnSuccessReturnStatuscode200()
    {   
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.Get(It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetOnFailureReturnStatuscode404()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (NotFoundResult)sut.Get(It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void EditOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.Edit(It.IsAny<TrancaDto>(), It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void EditOnFailureReturnStatuscode404()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (NotFoundResult)sut.Edit(It.IsAny<TrancaDto>(), It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void DeleteOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkResult)sut.Delete(It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void DeleteOnFailureReturnStatuscode404()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (NotFoundResult)sut.Delete(It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void GetBicicletaOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockTrancaService.Setup(service => service.GetTranca(It.IsAny<int>())).Returns(new TrancaModel());
        mockTrancaService.Setup(service => service.GetBicicleta(It.IsAny<int>())).Returns(new BicicletaModel());

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.GetBicicleta(It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void GetBicicletaOnFailureReturnStatuscode404()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (NotFoundResult)sut.GetBicicleta(It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }


    [Fact]
    public void GetBicicletaOnEmptyTrancaReturnStatuscode404()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);
        mockTrancaService.Setup(service => service.GetTranca(It.IsAny<int>())).Returns(new TrancaModel());

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (NotFoundResult)sut.GetBicicleta(It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void ChangeStatusOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.ChangeStatus(It.IsAny<int>(), It.IsAny<string>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void ChangeStatusOnFailureReturnStatuscode404()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (NotFoundResult)sut.ChangeStatus(It.IsAny<int>(), It.IsAny<string>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void UnlockOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.Unlock(It.IsAny<int>(), It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void UnlockOnFailureReturnStatuscode404()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (NotFoundResult)sut.Unlock(It.IsAny<int>(), It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void LockOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.Lock(It.IsAny<int>(), It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void LockOnFailureReturnStatuscode404()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (NotFoundResult)sut.Lock(It.IsAny<int>(), It.IsAny<int>());

        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public void AddtoTotemOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockTrancaService.Setup(service => service.GetTranca(It.IsAny<int>())).Returns(new TrancaModel());
        mockTrancaService.Setup(service => service.AddTrancaToTotem(It.IsAny<TrancaModel>(), It.IsAny<int>())).Returns(true);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.AddToTotem(It.IsAny<TrancaRedeDto>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void RemoveFromTotemOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockTrancaService.Setup(service => service.GetTranca(It.IsAny<int>())).Returns(new TrancaModel());

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.RemoveFromTotem(It.IsAny<TrancaRedeDto>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void RemoveBicicletaFromTrancaOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockTrancaService.Setup(service => service.RemoveBicicletaFromTranca(It.IsAny<BicicletaRemoveDto>())).Returns(new TrancaModel());

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.RemoveBicicletaFromTranca(It.IsAny<BicicletaRemoveDto>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void AddBicicletaToTrancaOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockTrancaService.Setup(service => service.AddBicicletaToTranca(It.IsAny<BicicletaRemoveDto>())).Returns(new TrancaModel());

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.AddBicicletaToTranca(It.IsAny<BicicletaRemoveDto>());

        result.StatusCode.Should().Be(200);
    }


}