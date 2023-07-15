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
    public class BicicletaServiceTest
    {
        [Fact]
        public void CreateOnSuccessReturnBicicletaModel()
        {
            var sut = new BicicletaService();

            var result = sut.CreateBicicleta(new BicicletaDTO
            {
                Marca = "caloi",
                Modelo = "caloi 1000",
                Numero = "000",
                Ano = "2023",
                Status = "nova",
            });

            Assert.Equal(typeof(BicicletaModel), result.GetType());
        }

        [Fact]
        public void DeleteOnSuccessReturnBicicletaModel()
        {
            var sut = new BicicletaService();
            sut.CreateBicicleta(new BicicletaDTO
            {
                Marca = "caloi",
                Modelo = "caloi 1000",
                Numero = "000",
                Ano = "2023",
                Status = "nova",
            });

            var result = sut.Deletebicicleta(It.IsAny<int>());

            Assert.Equal(typeof(BicicletaModel), result.GetType());
        }

        [Fact]
        public void GetOnSuccessReturnBicicletaModel()
        {
            var sut = new BicicletaService();
            sut.CreateBicicleta(new BicicletaDTO
            {
                Marca = "caloi",
                Modelo = "caloi 1000",
                Numero = "000",
                Ano = "2023",
                Status = "nova",
            });

            var result = sut.GetBicicleta(It.IsAny<int>());

            Assert.Equal(typeof(BicicletaModel), result.GetType());
        }

        [Fact]
        public void UpdateOnSuccessReturnBicicletaModel()
        {
            var sut = new BicicletaService();
            sut.CreateBicicleta(new BicicletaDTO
            {
                Marca = "caloi",
                Modelo = "caloi 1000",
                Numero = "000",
                Ano = "2023",
                Status = "nova",
            });

            var result = sut.UpdateBicicleta(new BicicletaDTO
            {
                Marca = "caloi",
                Modelo = "caloi 1000",
                Numero = "000",
                Ano = "2023",
                Status = "nova",
            }, It.IsAny<int>());

            Assert.Equal(typeof(BicicletaModel), result.GetType());
        }

        [Fact]
        public void GetAllOnSucessReturnsList()
        {
            var sut = new BicicletaService();

            var result = sut.GetAll();

            Assert.Equal(typeof(List<BicicletaModel>), result.GetType());
        }

        [Fact]
        public void ChangeStatusOnSuccessReturnBicicletaModel()
        {
            var sut = new BicicletaService();
            sut.CreateBicicleta(new BicicletaDTO
            {
                Marca = "caloi",
                Modelo = "caloi 1000",
                Numero = "000",
                Ano = "2023",
                Status = "nova",
            });

            var result = sut.ChangeStatus(It.IsAny<int>(), It.IsAny<string>());

            Assert.Equal(typeof(BicicletaModel), result.GetType());
        }

        [Fact]
        public void ContainsOnSucessReturnsTrue()
        {
            var sut = new BicicletaService();
            sut.CreateBicicleta(new BicicletaDTO
            {
                Marca = "caloi",
                Modelo = "caloi 1000",
                Numero = "000",
                Ano = "2023",
                Status = "nova",
            });

            var result = sut.Contains(It.IsAny<int>()); 

            Assert.True(result);
        }

        [Fact]
        public void IsEmptyOnEmptyReturnsFalse()
        {
            var sut = new BicicletaService();
            sut.CreateBicicleta(new BicicletaDTO
            {
                Marca = "caloi",
                Modelo = "caloi 1000",
                Numero = "000",
                Ano = "2023",
                Status = "nova",
            });

            var result = sut.IsEmpty();

            Assert.False(result);
        }
    }
}
    