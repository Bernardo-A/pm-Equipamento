using AutoMapper;
using Equipamento.API.ViewModels;
using static Equipamento.API.Services.TotemService;
using System.Linq;

namespace Equipamento.API.Services
{
    public interface ITotemService
    {
        public TotemViewModel CreateTotem(TotemInsertViewModel totem);
        public TotemViewModel GetTotem(int id);
        public TotemViewModel UpdateTotem(TotemInsertViewModel totemNovo, int id);
        public TotemViewModel DeleteTotem(int id);
        public List<TotemViewModel> GetAll();
        public bool Contains(int id);
        public bool IsEmpty();

        public List<TrancaViewModel>? GetTrancas(int totemId);

        public List<BicicletaViewModel> GetBicicletas(int totemId);

        public void AddTranca(TrancaViewModel tranca, int totemId);
        public void RemoveTranca(int totemId, int trancaId);
        public bool IsTrancaAssigned(int trancaId);
    }

    public class TotemService : ITotemService
    {
        private static readonly Dictionary<int, TotemViewModel> dict = new();
        public TotemService()
        {
        }

        public TotemViewModel CreateTotem(TotemInsertViewModel totem)
        {
            var result = new TotemViewModel()
            {
                Id = dict.Count,
            };
            result.Id = dict.Count;
            dict.Add(dict.Count, result);
            return (result);
        }

        public TotemViewModel GetTotem(int id)
        {
            return dict.ElementAt(id).Value;
        }

        public TotemViewModel UpdateTotem(TotemInsertViewModel totemNovo, int id)
        {
            var totemAntigo = dict.ElementAt(id).Value;
            var result = new TotemViewModel
            {
                Id = totemAntigo.Id,
                Trancas = totemAntigo.Trancas,
                Localizacao = totemNovo.Localizacao,
                Descricao = totemNovo.Descricao,
            };
            dict[id] = result;
            return (result);
        }

        public virtual TotemViewModel DeleteTotem(int id)
        {
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

        public virtual bool Contains(int id)
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

        public void AddTranca(TrancaViewModel tranca, int totemId)
        {
            if (!dict.ContainsKey(totemId))
            {
                return;
            }
            if(dict[totemId].Trancas == null)
            {
                dict[totemId].Trancas = new List<TrancaViewModel> { tranca};
            }else
            {
                dict[totemId]?.Trancas?.Add(tranca);
            }
        }

        public List<TrancaViewModel>? GetTrancas(int totemId) 
        {
            if (dict[totemId].Trancas != null)
            {
                List<TrancaViewModel> result = new();
                var objects = dict.ElementAt(totemId).Value.Trancas;
                if(objects?.Count == 0)
                {
                    return result;
                }
                if(objects != null)
                {
                    foreach (var value in objects)
                    {
                        result.Add(value);
                    }
                    return result;
                }
            }
            return null;
        }

        public List<BicicletaViewModel> GetBicicletas(int totemId)
        {
            List<BicicletaViewModel> result = new();
            var objects = dict.ElementAt(totemId).Value.Trancas;
            if( objects?.Count == 0)
            {
                return result;
            }
            result.AddRange(from value in objects
                            let bicicleta = value.Bicicleta
                            where bicicleta != null
                            select value.Bicicleta);
            return result;
        }

        public void RemoveTranca(int totemId, int trancaId)
        {
            var objects = dict.ElementAt(totemId).Value;
            if(objects.Trancas != null && objects.Trancas.Count != 0)
            {
                foreach(var value in objects.Trancas.ToList())
                {
                    if(value.Id == trancaId)
                    {
                        objects.Trancas.Remove(value);
                        value.Bicicleta = null;
                    }
                }
            }
        }

        public bool IsTrancaAssigned(int trancaId)
        {
            Dictionary<int, TotemViewModel>.ValueCollection objects = dict.Values;
            foreach (var value in objects.Select(x => x.Trancas))
            {
                if(value != null)
                {
                    foreach(var tranca in value)
                    {
                        if(tranca.Id == trancaId)
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }
    }
}
