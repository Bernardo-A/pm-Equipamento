using Equipamento.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Equipamento.Tests
{
    public class IntegrationTests
    {
        private const string aluguelAddress = "https://pmaluguel.herokuapp.com";
        private const string externoAddress = "https://pmexterno.herokuapp.com";

        [Fact]
        public async Task GetFuncionarioIntegration()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(aluguelAddress + "/funcionario/" + 0);
            response.EnsureSuccessStatusCode();
            Assert.True(response.IsSuccessStatusCode);
        }
        [Fact]
        public async Task PostEmailIntegration()
        {
            var client = new HttpClient();
            var body = JsonContent.Create(new EmailDto
            {
                Email = "bernardo.agrelos@edu.unirio.br",
                Assunto = "Teste unitario",
                Mensagem = "Teste unitario equipamento"
            });

            var result = await client.PostAsync(externoAddress + "/enviarEmail", body);
            result.EnsureSuccessStatusCode();
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
