using AutoMapper;
using Equipamento.API.ViewModels;
using static Equipamento.API.Services.TotemService;

namespace Equipamento.API.Services
{
    public interface ITotemService
    {
        public TotemViewModel CreateTotem(TotemInsertViewModel Totem);
        public TotemViewModel GetTotem(int id);
        public TotemViewModel UpdateTotem(TotemInsertViewModel Totem, int id);
        public TotemViewModel DeleteTotem(int id);
        public List<TotemViewModel> GetAll();
        public bool Contains(int id);
        public TotemViewModel ChangeStatus(int id, string status);
        public bool isEmpty();
    }

    public class TotemService : ITotemService
    {
        private static readonly Dictionary<int, TotemViewModel> dict = new();

        private readonly IMapper _mapper;

        public TotemService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TotemViewModel CreateTotem(TotemInsertViewModel Totem)
        {
            var result = _mapper.Map<TotemInsertViewModel, TotemViewModel>(Totem);
            result.Id = dict.Count;
            dict.Add(dict.Count, result);
            return (result);
        }

        public TotemViewModel GetTotem(int id)
        {
            return dict.ElementAt(id).Value;
        }

        public TotemViewModel UpdateTotem(TotemInsertViewModel TotemNovo, int id)
        {
            var TotemAntigo = dict.ElementAt(id).Value;
            var result = _mapper.Map(TotemNovo, TotemAntigo);
            dict[id] = result;
            return (result);
        }

        public TotemViewModel DeleteTotem(int id)
        {
            dict[id].Status = "Excluida";
            return dict.ElementAt(id).Value;
        }

        public List<TotemViewModel> GetAll()
        {
            List<TotemViewModel> result = new();
            Dictionary<int, TotemViewModel>.ValueCollection objects = dict.Values;
            foreach (var value in objects)
            {
                result.Add(value);
            }
            return result;
        }

        public TotemViewModel ChangeStatus(int id, string status)
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
            if (dict.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
