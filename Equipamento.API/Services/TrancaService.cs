using AutoMapper;
using Equipamento.API.ViewModels;
using System.Diagnostics;
using static Equipamento.API.Services.TrancaService;

namespace Equipamento.API.Services
{
    public interface ITrancaService
    {
        public TrancaViewModel CreateTranca(TrancaInsertViewModel tranca);
        public TrancaViewModel GetTranca(int id);
        public TrancaViewModel UpdateTranca(TrancaInsertViewModel tranca, int id);
        public TrancaViewModel DeleteTranca(int id);
        public List<TrancaViewModel> GetAll();
        public bool Contains(int id);
        public TrancaViewModel ChangeStatus(int id, string status);
        public bool IsEmpty();
        public TrancaViewModel Unlock(int? bicicletaId, int trancaId);
        public TrancaViewModel Lock(int? bicicletaId, int trancaId);
        public BicicletaViewModel? GetBicicleta(int trancaId);
        public bool AddTrancaToTotem(TrancaViewModel tranca, int totemId);
        public bool RemoveTrancaFromTotem(TrancaViewModel tranca, int totemId);
        public TrancaViewModel? AddBicicletaToTranca(BicicletaRedeAddViewModel viewModel);
        public TrancaViewModel? RemoveBicicletaFromTranca(BicicletaRemoveViewModel viewModel);
    }

    public class TrancaService : ITrancaService
    {
        private static readonly Dictionary<int, TrancaViewModel> dict = new();

        private readonly IBicicletaService _bicicletaService;

        private readonly ITotemService _totemService;

        public TrancaService(IBicicletaService bicicletaService, ITotemService totemService)
        {
            _bicicletaService = bicicletaService;
            _totemService = totemService;
        }


        public TrancaViewModel CreateTranca(TrancaInsertViewModel tranca)
        {
            var result = new TrancaViewModel
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

        public TrancaViewModel GetTranca(int id)
        {
            return dict.ElementAt(id).Value;
        }

        public TrancaViewModel UpdateTranca(TrancaInsertViewModel tranca, int id)
        {
            var trancaAntigo = dict.ElementAt(id).Value;
            var result = new TrancaViewModel
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

        public TrancaViewModel Unlock(int? bicicletaId, int trancaId)
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

        public TrancaViewModel Lock(int? bicicletaId, int trancaId)
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

        public BicicletaViewModel? GetBicicleta(int trancaId)
        {
            var tranca = dict[trancaId];
            if (tranca.Bicicleta != null)
            {
                var result = tranca.Bicicleta;
                return result;
            }
            return null;
        }

        public bool AddTrancaToTotem(TrancaViewModel tranca, int totemId)
        {
            if (!_totemService.IsTrancaAssigned(tranca.Id))
            {
                _totemService.AddTranca(tranca, totemId);
                return true;
            }
            return false;
        }

        public bool RemoveTrancaFromTotem(TrancaViewModel tranca, int totemId)
        {
            if (_totemService.IsTrancaAssigned(tranca.Id))
            {
                _totemService.RemoveTranca(totemId, tranca.Id);
                return true;
            }
            return false;
        }

        public TotemViewModel GetTotem(int totemId)
        {
            return _totemService.GetTotem(totemId);
        }
        public TrancaViewModel? AddBicicletaToTranca(BicicletaRedeAddViewModel viewModel)
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

        public TrancaViewModel? RemoveBicicletaFromTranca(BicicletaRemoveViewModel viewModel)
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
