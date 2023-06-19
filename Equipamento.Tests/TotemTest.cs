using AutoMapper;
using Equipamento.API.Controllers;
using Equipamento.API.Services;
using Equipamento.API.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Bson;
using Xunit;

namespace Equipamento.Tests;

public class TotemTest
{
    private readonly Mock<ILogger<TotemController>> _logger = new();
    private readonly Mock<IMapper> _mapper = new();

    [Fact]
    public void CreateOnSucessReturnStatusCode200()
    {
        var mockTotemService = new Mock<ITotemService>();

        var sut = new TotemController(_logger.Object, _mapper.Object, mockTotemService.Object);

        var result = (OkObjectResult)sut.Create(new TotemInsertViewModel
        {
            Localizacao = "Rio de Janeiro",
            Descricao = "um totem"
        });

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void EditOnSucessReturnStatusCode200()
    {

        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.GetTotem()).Returns(new TotemViewModel());

        var sut = new TotemController(_logger.Object, _mapper.Object, mockTotemService.Object);
        
        var result = (OkObjectResult)sut.Edit(new TotemInsertViewModel
        {
            Localizacao = "Rio de Janeiro",
            Descricao = "um totem"
        }, 0);

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void DeleteOnSucessReturnStatusCode200()
    {
        var mockTotemService = new Mock<ITotemService>();
        mockTotemService.Setup(service => service.GetTotem()).Returns(new TotemViewModel());

        var sut = new TotemController(_logger.Object, _mapper.Object, mockTotemService.Object);

        var result = (OkResult)sut.Delete(0);

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void TotemServiceGetTotemReturnsTotem()
    {
        var sut = new TotemService();

        var result = sut.GetTotem();

        Assert.IsType<TotemViewModel>(result);
    }

    [Fact]
    public void TesttotemViewModelGet()
    {
        var totemService = new TotemService(); ;

        var totem = totemService.GetTotem();

        var result = new TotemViewModel();
        {
            result.Id = totem.Id;
            result.Localizacao = totem.Localizacao;
            result.Descricao = totem.Descricao;
            result.Status = totem.Status;
        }

        result.Should().BeEquivalentTo(totem);
    }

    [Fact]
    public void TestTotemInsertViewModelGet()
    {
        var totem = new TotemInsertViewModel
        {
            Localizacao = "Rio de Janeiro",
            Descricao = "um totem"
        };

        var result = new TotemInsertViewModel();
        {
            result.Localizacao = totem.Localizacao;
            result.Descricao = totem.Descricao;
        }

        result.Should().BeEquivalentTo(totem);
    }


}