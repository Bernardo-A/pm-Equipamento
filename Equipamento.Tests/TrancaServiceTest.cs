using Equipamento.API.Models;
using Equipamento.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Equipamento.Tests
{
    public class TrancaServiceTest
    {
        private readonly Mock<IBicicletaService> _bicicletaService = new();

        private readonly Mock<ITotemService> _totemService = new();

        [Fact]
        public void CreateOnSuccessReturnTrancaModel()
        {
            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);

            var result = sut.CreateTranca(new TrancaDto());

            Assert.Equal(typeof(TrancaModel), result.GetType());
        }

        [Fact]
        public void DeleteOnSuccessReturnTrancaModel()
        {
            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);

            sut.CreateTranca(new TrancaDto());

            sut.DeleteTranca(It.IsAny<int>());
            var result = sut.GetTranca(It.IsAny<int>());

            Assert.True(result.Status == "Excluida");
        }

        [Fact]
        public void GetOnSuccessReturnTrancaModel()
        {
            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);

            sut.CreateTranca(new TrancaDto());
            var result = sut.GetTranca(It.IsAny<int>());

            Assert.Equal(typeof(TrancaModel), result.GetType());
        }

        [Fact]
        public void UpdateOnSuccessReturnTrancaModel()
        {
            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);

            sut.CreateTranca(new TrancaDto());
            var result = sut.UpdateTranca(new TrancaDto(), It.IsAny<int>());

            Assert.Equal(typeof(TrancaModel), result.GetType());
        }

        [Fact]
        public void GetAllOnSuccessReturnsTrancaList()
        {
            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);

            var result = sut.GetAll();

            Assert.Equal(typeof(List<TrancaModel>), result.GetType());
        }

        [Fact]
        public void ContainsOnSuccessReturnsTrue()
        {
            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);
            sut.CreateTranca(new TrancaDto());

            var result = sut.Contains(It.IsAny<int>());

            Assert.True(result);
        }

        [Fact]
        public void ContainsOnFailureReturnsFalse()
        {
            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);

            var result = sut.Contains(It.IsAny<int>());

            Assert.False(result);
        }

        [Fact]
        public void IsEmptyOnSuccessReturnsTrue()
        {
            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);

            var result = sut.IsEmpty();

            Assert.True(result);
        }

        [Fact]
        public void IsEmptyOnFailureReturnsFalse()
        {
            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);

            sut.CreateTranca(new TrancaDto());

            var result = sut.IsEmpty();

            Assert.False(result);
        }

        [Fact]
        public void GetTotemOnSuccessReturnsTotem()
        {
            var mockTotemService = new Mock<ITotemService>();
            mockTotemService.Setup(service => service.GetTotem(It.IsAny<int>())).Returns(new TotemModel());

            var sut = new TrancaService(_bicicletaService.Object, mockTotemService.Object);

            var result = sut.GetTotem(It.IsAny<int>());

            Assert.NotNull(result);
            Assert.Equal(typeof(TotemModel), result.GetType());
        }

        [Fact]
        public void TrancaModelTest()
        {
            var tranca = new TrancaRedeDto
            {
                FuncionarioId = 0,
                TotemId = 0,
                TrancaId = 0,
            };
            Assert.Equal(typeof(TrancaRedeDto), tranca.GetType());
        }

        [Fact]
        public void ChangeStatusTest()
        {
            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);

            sut.CreateTranca(new TrancaDto());

            var result = sut.ChangeStatus(It.IsAny<int>(), It.IsAny<string>());

            Assert.Equal(typeof(TrancaModel), result.GetType());
        }

        [Fact]
        public async void GetBicicletaTest()
        {
            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);

            sut.CreateTranca(new TrancaDto());

            await sut.AddBicicletaToTranca(new BicicletaRedeDto());

            var result = sut.GetBicicleta(It.IsAny<int>());

            Assert.Equal(typeof(BicicletaModel), result?.GetType());

        }

        [Fact]
        public void LockTest()
        {
            var mockBicicletaService = new Mock<IBicicletaService>();
            mockBicicletaService.Setup(service => service.GetBicicleta(It.IsAny<int>())).Returns(new BicicletaModel());

            var sut = new TrancaService(_bicicletaService.Object, _totemService.Object);
            var result = sut.Lock(It.IsAny<int>(), It.IsAny<int>());

            Assert.Equal(typeof(BicicletaModel), result.GetType());
        }
    }
}
