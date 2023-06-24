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
        public bool isEmpty();
        public BicicletaViewModel ChangeStatus(int id, string status);
    }

    public class BicicletaService : IBicicletaService
    {
        private static readonly Dictionary<int, BicicletaViewModel> dict = new();

        private readonly IMapper _mapper;

        public BicicletaService(IMapper mapper) 
        {
            _mapper = mapper;
        }

        public BicicletaViewModel CreateBicicleta(BicicletaInsertViewModel bicicleta)
        {
            var result = _mapper.Map<BicicletaInsertViewModel, BicicletaViewModel>(bicicleta);
            result.Id = dict.Count;
            dict.Add(dict.Count, result);
            return (result);
        }

        public BicicletaViewModel GetBicicleta(int id)
        {
            return dict.ElementAt(id).Value;
        }

        public BicicletaViewModel UpdateBicicleta(BicicletaInsertViewModel bicicletaNovo, int id)
        {
            var bicicletaAntigo = dict.ElementAt(id).Value;
            var result = _mapper.Map(bicicletaNovo, bicicletaAntigo);
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

        public bool isEmpty()
        {
            if(dict.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
