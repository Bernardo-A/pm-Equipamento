using Equipamento.API.Controllers;
using Equipamento.API.Services;
using Equipamento.API.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace equipamento.tests;

public class TrancaControllerTest
{

    private readonly Mock<ILogger<TrancaController>> _logger = new();

    [Fact]
    public void CreateOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        
        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.Create(It.IsAny<TrancaInsertViewModel>());

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

        var result = (OkObjectResult)sut.Edit(It.IsAny<TrancaInsertViewModel>(), It.IsAny<int>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void EditOnFailureReturnStatuscode404()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(false);

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (NotFoundResult)sut.Edit(It.IsAny<TrancaInsertViewModel>(), It.IsAny<int>());

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
        mockTrancaService.Setup(service => service.GetTranca(It.IsAny<int>())).Returns(new TrancaViewModel());
        mockTrancaService.Setup(service => service.GetBicicleta(It.IsAny<int>())).Returns(new BicicletaViewModel());

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
        mockTrancaService.Setup(service => service.GetTranca(It.IsAny<int>())).Returns(new TrancaViewModel());

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
        mockTrancaService.Setup(service => service.GetTranca(It.IsAny<int>())).Returns(new TrancaViewModel());

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.AddToTotem(It.IsAny<TrancaRedeViewModel>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void RemoveFromTotemOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockTrancaService.Setup(service => service.GetTranca(It.IsAny<int>())).Returns(new TrancaViewModel());

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.RemoveFromTotem(It.IsAny<TrancaRedeViewModel>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void RemoveBicicletaFromTrancaOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockTrancaService.Setup(service => service.RemoveBicicletaFromTranca(It.IsAny<BicicletaRemoveViewModel>())).Returns(new TrancaViewModel());

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.RemoveBicicletaFromTranca(It.IsAny<BicicletaRemoveViewModel>());

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void AddBicicletaToTrancaOnSuccessReturnStatuscode200()
    {
        var mockTrancaService = new Mock<ITrancaService>();
        mockTrancaService.Setup(service => service.Contains(It.IsAny<int>())).Returns(true);
        mockTrancaService.Setup(service => service.AddBicicletaToTranca(It.IsAny<BicicletaRemoveViewModel>())).Returns(new TrancaViewModel());

        var sut = new TrancaController(_logger.Object, mockTrancaService.Object);

        var result = (OkObjectResult)sut.AddBicicletaToTranca(It.IsAny<BicicletaRemoveViewModel>());

        result.StatusCode.Should().Be(200);
    }


}