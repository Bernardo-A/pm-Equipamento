using Equipamento.API.ViewModels;

namespace Equipamento.API.Services
{
    public interface ITotemService
    {
           public TotemViewModel GetTotem();
    }

    public class TotemService : ITotemService
    {
        public TotemViewModel GetTotem()
        {
            return new TotemViewModel
            {
                Id = 0,
                Localizacao = "Rio de Janeiro",
                Descricao = "um totem",
                Status = "Habilitado"
            };
        }
    }
}
