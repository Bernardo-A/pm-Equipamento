using AutoMapper;
using Equipamento.API.ViewModels;
using static Equipamento.API.Services.TrancaService;

namespace Equipamento.API.Services
{
    public interface ITrancaService
    {
        public TrancaViewModel CreateTranca(TrancaInsertViewModel Tranca);
        public TrancaViewModel GetTranca(int id);
        public TrancaViewModel UpdateTranca(TrancaInsertViewModel Tranca, int id);
        public TrancaViewModel DeleteTranca(int id);
        public List<TrancaViewModel> GetAll();
        public bool Contains(int id);
        public TrancaViewModel ChangeStatus(int id, string status);
        public bool IsEmpty();
        public TrancaViewModel Unlock(int bicicletaId, int trancaId);
    }

    public class TrancaService : ITrancaService
    {
        private static readonly Dictionary<int, TrancaViewModel> dict = new();

        private readonly IMapper _mapper;

        public TrancaService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TrancaViewModel CreateTranca(TrancaInsertViewModel Tranca)
        {
            var result = _mapper.Map<TrancaInsertViewModel, TrancaViewModel>(Tranca);
            result.Id = dict.Count;
            dict.Add(dict.Count, result);
            return (result);
        }

        public TrancaViewModel GetTranca(int id)
        {
            return dict.ElementAt(id).Value;
        }

        public TrancaViewModel UpdateTranca(TrancaInsertViewModel TrancaNovo, int id)
        {
            var TrancaAntigo = dict.ElementAt(id).Value;
            var result = _mapper.Map(TrancaNovo, TrancaAntigo);
            dict[id] = result;
            return (result);
        }

        public TrancaViewModel DeleteTranca(int id)
        {
            dict[id].Status = "Excluida";
            return dict.ElementAt(id).Value;
        }

        public List<TrancaViewModel> GetAll()
        {
            List<TrancaViewModel> result = new();
            Dictionary<int, TrancaViewModel>.ValueCollection objects = dict.Values;
            foreach (var value in objects)
            {
                result.Add(value);
            }
            return result;
        }

        public TrancaViewModel ChangeStatus(int id, string status)
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
            if (dict.Count == 0)
            {
                return true;
            }
            return false;
        }

        public TrancaViewModel Unlock(int bicicletaId, int trancaId)
        {

        }
    }
}
