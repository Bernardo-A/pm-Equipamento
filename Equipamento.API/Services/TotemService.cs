using AutoMapper;
using Equipamento.API.Models;
using static Equipamento.API.Services.TotemService;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Equipamento.API.Services
{
    public interface ITotemService
    {
        public TotemModel CreateTotem(TotemDto totem);
        public TotemModel GetTotem(int id);
        public TotemModel UpdateTotem(TotemDto totemNovo, int id);
        public void DeleteTotem(int id);
        public List<TotemModel> GetAll();
        public bool Contains(int id);
        public bool IsEmpty();

        public List<TrancaModel>? GetTrancas(int totemId);

        public List<BicicletaModel> GetBicicletas(int totemId);

        public void AddTranca(TrancaModel tranca, int totemId);
        public void RemoveTranca(int totemId, int trancaId);
        public bool IsTrancaAssigned(int trancaId);
    }

    public class TotemService : ITotemService
    {
        private static readonly Dictionary<int, TotemModel> dict = new();
        public TotemService()
        {
        }

        public TotemModel CreateTotem(TotemDto totem)
        {
            var result = new TotemModel()
            {
                Id = dict.Count,
                Localizacao = totem.Localizacao,
                Descricao = totem.Descricao,    
            };
            dict.Add(dict.Count, result);
            return (dict.ElementAt(result.Id).Value);
        }

        public TotemModel GetTotem(int id)
        {
            return dict.ElementAt(id).Value;
        }

        public TotemModel UpdateTotem(TotemDto totemNovo, int id)
        {
            var totemAntigo = dict.ElementAt(id).Value;
            var result = new TotemModel
            {
                Id = totemAntigo.Id,
                Trancas = totemAntigo.Trancas,
                Localizacao = totemNovo.Localizacao,
                Descricao = totemNovo.Descricao,
            };
            dict[id] = result;
            return (result);
        }

        public virtual void DeleteTotem(int id)
        {
            dict.Remove(id);
        }

        public List<TotemModel> GetAll()
        {
            List<TotemModel> result = new();
            Dictionary<int, TotemModel>.ValueCollection objects = dict.Values;
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

        public void AddTranca(TrancaModel tranca, int totemId)
        {
            if (!dict.ContainsKey(totemId))
            {
                return;
            }
            if(dict[totemId].Trancas == null)
            {
                dict[totemId].Trancas = new List<TrancaModel> { tranca};
            }else
            {
                dict[totemId]?.Trancas?.Add(tranca);
            }
        }

        public List<TrancaModel>? GetTrancas(int totemId) 
        {
            if (dict[totemId].Trancas != null)
            {
                List<TrancaModel> result = new();
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

        public List<BicicletaModel> GetBicicletas(int totemId)
        {
            List<BicicletaModel> result = new();
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
                        value.Status = "EMREPARO";
                        value.Bicicleta = null;
                    }
                }
            }
        }

        public bool IsTrancaAssigned(int trancaId)
        {
            Dictionary<int, TotemModel>.ValueCollection objects = dict.Values;
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
