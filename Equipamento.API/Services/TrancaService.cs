using AutoMapper;
using Equipamento.API.Models;
using System.Diagnostics;
using static Equipamento.API.Services.TrancaService;

namespace Equipamento.API.Services
{
    public interface ITrancaService
    {
        public TrancaModel CreateTranca(TrancaDto tranca);
        public TrancaModel GetTranca(int id);
        public TrancaModel UpdateTranca(TrancaDto tranca, int id);
        public TrancaModel DeleteTranca(int id);
        public List<TrancaModel> GetAll();
        public bool Contains(int id);
        public TrancaModel ChangeStatus(int id, string status);
        public bool IsEmpty();
        public TrancaModel Unlock(int? bicicletaId, int trancaId);
        public TrancaModel Lock(int? bicicletaId, int trancaId);
        public BicicletaModel? GetBicicleta(int trancaId);
        public bool AddTrancaToTotem(TrancaModel tranca, int totemId);
        public bool RemoveTrancaFromTotem(TrancaModel tranca, int totemId);
        public TrancaModel? AddBicicletaToTranca(BicicletaRedeDto viewModel);
        public TrancaModel? RemoveBicicletaFromTranca(BicicletaRemoveDto viewModel);
    }

    public class TrancaService : ITrancaService
    {
        private static readonly Dictionary<int, TrancaModel> dict = new();

        private readonly IBicicletaService _bicicletaService;

        private readonly ITotemService _totemService;

        public TrancaService(IBicicletaService bicicletaService, ITotemService totemService)
        {
            _bicicletaService = bicicletaService;
            _totemService = totemService;
        }


        public TrancaModel CreateTranca(TrancaDto tranca)
        {
            var result = new TrancaModel
            {
                Id = dict.Count,
                Bicicleta = null,
                Numero = tranca.Numero,
                Localizacao = tranca.Localizacao,
                AnoDeFabricacao = tranca.AnoDeFabricacao,
                Modelo = tranca.Modelo,
                Status = tranca.Status
            };
            dict.Add(dict.Count, result);
            return (result);
        }

        public TrancaModel GetTranca(int id)
        {
            return dict.ElementAt(id).Value;
        }

        public TrancaModel UpdateTranca(TrancaDto tranca, int id)
        {
            var trancaAntigo = dict.ElementAt(id).Value;
            var result = new TrancaModel
            {
                Id = trancaAntigo.Id,
                Bicicleta = trancaAntigo.Bicicleta,
                Numero = tranca.Numero,
                Localizacao = tranca.Localizacao,
                AnoDeFabricacao = tranca.AnoDeFabricacao,
                Modelo = tranca.Modelo,
                Status = tranca.Status
            };
            dict[id] = result;
            return (result);
        }

        public TrancaModel DeleteTranca(int id)
        {
            dict[id].Status = "Excluida";
            return dict.ElementAt(id).Value;
        }

        public List<TrancaModel> GetAll()
        {
            List<TrancaModel> result = new();
            Dictionary<int, TrancaModel>.ValueCollection objects = dict.Values;
            foreach (var value in objects)
            {
                result.Add(value);
            }
            return result;
        }

        public TrancaModel ChangeStatus(int id, string status)
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
        public bool IsEmpty()
        {
            if (dict.Count == 0)
            {
                return true;
            }
            return false;
        }

        public TrancaModel Unlock(int? bicicletaId, int trancaId)
        {
            var tranca = dict.ElementAt(trancaId).Value;
            tranca.Status = "LIVRE";
            if (bicicletaId == null)
            {
                return dict.ElementAt(trancaId).Value;
            }
            if (tranca.Bicicleta != null)
            {
                tranca.Bicicleta.Status = "EM_USO";
                dict[trancaId].Bicicleta = null;
            }
            return dict.ElementAt(trancaId).Value;
        }

        public TrancaModel Lock(int? bicicletaId, int trancaId)
        {
            dict[trancaId].Status = "OCUPADA";
            if (bicicletaId == null)
            {
                return dict.ElementAt(trancaId).Value;
            }
            dict[trancaId].Bicicleta = _bicicletaService.GetBicicleta((int)bicicletaId);
            _bicicletaService.ChangeStatus((int)bicicletaId, "DISPONIVEL");
            return dict.ElementAt(trancaId).Value;
        }

        public BicicletaModel? GetBicicleta(int trancaId)
        {
            var tranca = dict[trancaId];
            if (tranca.Bicicleta != null)
            {
                var result = tranca.Bicicleta;
                return result;
            }
            return null;
        }

        public bool AddTrancaToTotem(TrancaModel tranca, int totemId)
        {
            if (!_totemService.IsTrancaAssigned(tranca.Id))
            {
                _totemService.AddTranca(tranca, totemId);
                return true;
            }
            return false;
        }

        public bool RemoveTrancaFromTotem(TrancaModel tranca, int totemId)
        {
            if (_totemService.IsTrancaAssigned(tranca.Id))
            {
                _totemService.RemoveTranca(totemId, tranca.Id);
                return true;
            }
            return false;
        }

        public TotemModel GetTotem(int totemId)
        {
            return _totemService.GetTotem(totemId);
        }
        public TrancaModel? AddBicicletaToTranca(BicicletaRedeDto viewModel)
        {
            var tranca = dict.ElementAt(viewModel.TrancaId).Value;
            if(tranca.Bicicleta != null)
            {
                return null;
            }
            var bicicleta = _bicicletaService.GetBicicleta(viewModel.BicicletaId);
            bicicleta.Status = "DISPONIVEL";
            tranca.Bicicleta = bicicleta;
            return tranca;
        }

        public TrancaModel? RemoveBicicletaFromTranca(BicicletaRemoveDto viewModel)
        {
            var tranca = dict.ElementAt(viewModel.TrancaId).Value;
            if (tranca.Bicicleta == null)
            {
                return null;
            }
            var bicicleta = _bicicletaService.GetBicicleta(viewModel.BicicletaId);
            bicicleta.Status = viewModel.StatusAcaoReparador;
            tranca.Bicicleta = null;
            return tranca;
        }
    }
}
