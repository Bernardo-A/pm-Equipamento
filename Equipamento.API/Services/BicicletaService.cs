using AutoMapper;
using Equipamento.API.ViewModels;
using static Equipamento.API.Services.BicicletaService;

namespace Equipamento.API.Services
{
    public interface IBicicletaService
    {
        public BicicletaViewModel CreateBicicleta(BicicletaInsertViewModel bicicleta);
        public BicicletaViewModel GetBicicleta(int id);
        public BicicletaViewModel UpdateBicicleta(BicicletaInsertViewModel bicicleta, int id);
        public BicicletaViewModel Deletebicicleta(int id);
        public List<BicicletaViewModel> GetAll();
        public bool Contains(int id);
        public bool IsEmpty();
        public BicicletaViewModel ChangeStatus(int id, string status);
    }

    public class BicicletaService : IBicicletaService
    {
        private static readonly Dictionary<int, BicicletaViewModel> dict = new();

        public BicicletaService() 
        {
        }

        public BicicletaViewModel CreateBicicleta(BicicletaInsertViewModel bicicleta)
        {
            var result = new BicicletaViewModel()
            {
                Id = dict.Count,
                Marca = bicicleta.Marca,
                Modelo = bicicleta.Modelo,
                Ano = bicicleta.Ano,
                Numero = bicicleta.Numero,
                Status = bicicleta.Status,
            };
            dict.Add(dict.Count, result);
            return (result);
        }

        public BicicletaViewModel GetBicicleta(int id)
        {
            return dict.ElementAt(id).Value;
        }

        public BicicletaViewModel UpdateBicicleta(BicicletaInsertViewModel bicicleta, int id)
        {
            var result = new BicicletaViewModel()
            {
                Id = id,
                Marca = bicicleta.Marca,
                Modelo = bicicleta.Modelo,
                Ano = bicicleta.Ano,
                Numero = bicicleta.Numero,
                Status = bicicleta.Status,
            };
            dict[id] = result;
            return (result);
        }

        public BicicletaViewModel Deletebicicleta(int id)
        {
            dict[id].Status = "Excluida";
            return dict.ElementAt(id).Value;
        }

        public List<BicicletaViewModel> GetAll()
        {
            List<BicicletaViewModel> result = new();
            Dictionary<int, BicicletaViewModel>.ValueCollection objects = dict.Values;
            foreach (var value in objects)
            {
                result.Add(value);
            }
            return result;
        }

        public BicicletaViewModel ChangeStatus(int id, string status)
        {
            dict[id].Status = status;
            return dict.ElementAt(id).Value;
        }

        public bool Contains(int id)
        {
            if (dict.ContainsKey(id))
            {
                return true;
            }
            return false;
        }

        public bool IsEmpty()
        {
            if(dict.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
