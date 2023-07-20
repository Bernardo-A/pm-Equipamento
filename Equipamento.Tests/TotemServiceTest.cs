using Equipamento.API.Models;
using Equipamento.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Equipamento.Tests
{
    public class TotemServiceTest
    {
        [Fact]
        public void CreateOnSuccessReturnTotemModel()
        {
            var sut = new TotemService();

            var result = sut.CreateTotem(new TotemDto
            {
                Localizacao = "Rio",
                Descricao = "totem",
            });

            Assert.Equal(typeof(TotemModel), result.GetType());
        }

        [Fact]
        public void DeleteOnSuccessReturnTotemModel()
        {
            var sut = new TotemService();

            sut.CreateTotem(new TotemDto
            {
                Localizacao = "Rio",
                Descricao = "totem",
            });

            sut.DeleteTotem(It.IsAny<int>());
            var result = sut.Contains(It.IsAny<int>());

            Assert.False(result);
        }

        [Fact]
        public void GetOnSuccessReturnTotemModel()
        {
            var sut = new TotemService();

            var result = sut.GetTotem(It.IsAny<int>());

            Assert.Equal(typeof(TotemModel), result.GetType());
        }

        [Fact]
        public void UpdateOnSuccessReturnTotemModel()
        {
            var sut = new TotemService();

            var result = sut.UpdateTotem((new TotemDto
            {
                Localizacao = "Rio",
                Descricao = "totem",
            }), It.IsAny<int>());

            Assert.Equal(typeof(TotemModel), result.GetType());
        }

        [Fact]
        public void GetAllOnSuccessReturnsTotemList()
        {
            var sut = new TotemService();

            var result = sut.GetAll();

            Assert.Equal(typeof(List<TotemModel>), result.GetType());
        }

        [Fact]
        public void ContainsOnSuccessReturnsTrue()
        {
            var sut = new TotemService();
            sut.CreateTotem(new TotemDto
            {
                Localizacao = "Rio",
                Descricao = "totem",
            });

            var result = sut.Contains(It.IsAny<int>());

            Assert.True(result);
        }

        [Fact]
        public void ContainsOnFailureReturnsFalse()
        {
            var sut = new TotemService();

            var result = sut.Contains(It.IsAny<int>());

            Assert.False(result);
        }

        [Fact]
        public void IsEmptyOnSuccessReturnsTrue()
        {
            var sut = new TotemService();

            var result = sut.IsEmpty();

            Assert.True(result);
        }

        [Fact]
        public void IsEmptyOnFailureReturnsFalse()
        {
            var sut = new TotemService();

            sut.CreateTotem(new TotemDto
            {
                Localizacao = "Rio",
                Descricao = "totem",
            });

            var result = sut.IsEmpty();

            Assert.False(result);
        }

        [Fact]
        public void AddOnSuccessAddsTrancaToTotem()
        {
            var sut = new TotemService();
            sut.CreateTotem(new TotemDto
            {
                Localizacao = "Rio",
                Descricao = "totem",
            });

            sut.AddTranca(It.IsAny<TrancaModel>(), It.IsAny<int>());

            Assert.NotNull(sut.GetTotem(It.IsAny<int>()).Trancas);

        }

        [Fact]
        public void GetTrancasOnSuccessReturnsTranca()
        {
            var sut = new TotemService();
            sut.CreateTotem(new TotemDto
            {
                Localizacao = "Rio",
                Descricao = "totem",
            });

            sut.AddTranca(It.IsAny<TrancaModel>(), It.IsAny<int>());

            var result = sut.GetTrancas(It.IsAny<int>());

            Assert.NotNull(result);
            Assert.Equal(typeof(List<TrancaModel>), result.GetType());
        }


        [Fact]
        public void GetBicicletaOnSuccessReturnsBicicleta()
        {
            var sut = new TotemService();
            sut.CreateTotem(new TotemDto
            {
                Localizacao = "Rio",
                Descricao = "totem",
            });


            sut.AddTranca(new TrancaModel { Bicicleta = new BicicletaModel()}, It.IsAny<int>());

            var result = sut.GetBicicletas(It.IsAny<int>());

            Assert.Equal(typeof(List<BicicletaModel>), result.GetType());
        }
        [Fact]
        public void RemoveTrancaOnSuccessReturnsBicicleta()
        {
            var sut = new TotemService();
            sut.CreateTotem(new TotemDto
            {
                Localizacao = "Rio",
                Descricao = "totem",
            });

            sut.AddTranca(new TrancaModel { Bicicleta = new BicicletaModel() }, It.IsAny<int>());
            sut.RemoveTranca(It.IsAny<int>(), It.IsAny<int>());

            Assert.Empty(sut.GetTrancas(It.IsAny<int>()));
        }

        [Fact]
        public void IsTrancaAssignedOnFailureReturnsTrue()
        {
            var sut = new TotemService();
            sut.CreateTotem(new TotemDto
            {
                Localizacao = "Rio",
                Descricao = "totem",
            });

            Assert.False(sut.IsTrancaAssigned(It.IsAny<int>()));
        }
    }
}
