using Equipamento.API.ViewModels;

namespace Equipamento.API.Services
{
    public interface IBicicletaService
    {
           public BicicletaViewModel GetBicicleta();
    }

    public class BicicletaService : IBicicletaService
    {
        public BicicletaViewModel GetBicicleta()
        {
            return new BicicletaViewModel
            {
                Id = 0,
                Marca = "Caloi",
                Modelo = "Caloi 1000",
                Numero = "000",
                Ano = "2023",
                Status = "nova",
            };
        }
    }
}
