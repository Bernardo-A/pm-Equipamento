using AutoMapper;
using Equipamento.API.Models;
using static Equipamento.API.Services.BicicletaService;

namespace Equipamento.API.Services
{
    public interface IBicicletaService
    {
        public BicicletaModel CreateBicicleta(BicicletaDto bicicleta);
        public BicicletaModel GetBicicleta(int id);
        public BicicletaModel UpdateBicicleta(BicicletaDto bicicleta, int id);
        public BicicletaModel Deletebicicleta(int id);
        public List<BicicletaModel> GetAll();
        public bool Contains(int id);
        public bool IsEmpty();
        public BicicletaModel ChangeStatus(int id, string status);
    }

    public class BicicletaService : IBicicletaService
    {
        private static readonly Dictionary<int, BicicletaModel> dict = new();

        public BicicletaService() 
        {
        }

        public virtual BicicletaModel CreateBicicleta(BicicletaDto bicicleta)
        {
            var result = new BicicletaModel()
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

        public virtual BicicletaModel GetBicicleta(int id)
        {
            return dict.ElementAt(id).Value;
        }

        public virtual BicicletaModel UpdateBicicleta(BicicletaDto bicicleta, int id)
        {
            var result = new BicicletaModel()
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

        public virtual BicicletaModel Deletebicicleta(int id)
        {
            dict[id].Status = "Excluida";
            return dict.ElementAt(id).Value;
        }

        public virtual List<BicicletaModel> GetAll()
        {
            List<BicicletaModel> result = new();
            Dictionary<int, BicicletaModel>.ValueCollection objects = dict.Values;
            foreach (var value in objects)
            {
                result.Add(value);
            }
            return result;
        }

        public virtual BicicletaModel ChangeStatus(int id, string status)
        {
            dict[id].Status = status;
            return dict.ElementAt(id).Value;
        }

        public virtual bool Contains(int id)
        {
            if (dict.ContainsKey(id))
            {
                return true;
            }
            return false;
        }

        public virtual bool IsEmpty()
        {
            if(dict.Count == 0)
            {
                return true;
            }
            return false;
        }

        
    }
}
