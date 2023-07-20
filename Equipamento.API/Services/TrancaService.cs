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
        public Task<bool> AddTrancaToTotem(TrancaRedeDto rede);
        public Task<bool> RemoveTrancaFromTotem(TrancaRedeDto rede);
        public Task<TrancaModel?> AddBicicletaToTranca(BicicletaRedeDto viewModel);
        public Task<TrancaModel?> RemoveBicicletaFromTranca(BicicletaRemoveDto viewModel);
    }

    public class TrancaService : ITrancaService
    {
        private static readonly Dictionary<int, TrancaModel> dict = new();

        private readonly IBicicletaService _bicicletaService;

        private readonly ITotemService _totemService;

        private readonly HttpClient HttpClient = new();

        private const string aluguelAddress = "https://pmaluguel.herokuapp.com";

        private const string externoAPI = "https://pmexterno.herokuapp.com";

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

        public async Task<bool> AddTrancaToTotem(TrancaRedeDto rede)
        {
            if (!_totemService.IsTrancaAssigned(rede.TrancaId))
            {
                var tranca = GetTranca(rede.TrancaId);
                _totemService.AddTranca(tranca, rede.TotemId);

                var responseTranca = await HttpClient.GetAsync(aluguelAddress + "/funcionario/" + rede.FuncionarioId);
                var funcionario = await responseTranca.Content.ReadFromJsonAsync<FuncionarioModel>();
                var body = JsonContent.Create(new EmailDto
                {
                    Email = funcionario?.Email,
                    Assunto = "Operação no bicicletário concluída",
                    Mensagem = "A tranca " + tranca.Id + " foi adicionada"
                });

                await HttpClient.PostAsync(externoAPI + "/enviarEmail", body);
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveTrancaFromTotem(TrancaRedeDto rede)
        {
            if (_totemService.IsTrancaAssigned(rede.TrancaId))
            {
                _totemService.RemoveTranca(rede.TotemId, rede.TrancaId);

                var responseTranca = await HttpClient.GetAsync(aluguelAddress + "/funcionario/" + rede.FuncionarioId);
                var funcionario = await responseTranca.Content.ReadFromJsonAsync<FuncionarioModel>();
                var body = JsonContent.Create(new EmailDto
                {
                    Email = funcionario?.Email,
                    Assunto = "Operação no bicicletário concluída",
                    Mensagem = "A tranca " + rede.TrancaId + " foi adicionada"
                });

                await HttpClient.PostAsync(externoAPI + "/enviarEmail", body);

                return true;
            }
            return false;
        }

        public TotemModel GetTotem(int totemId)
        {
            return _totemService.GetTotem(totemId);
        }
        public async Task<TrancaModel?> AddBicicletaToTranca(BicicletaRedeDto viewModel)
        {
            var tranca = dict.ElementAt(viewModel.TrancaId).Value;
            if(tranca.Bicicleta != null)
            {
                return null;
            }
            var bicicleta = _bicicletaService.GetBicicleta(viewModel.BicicletaId);
            bicicleta.Status = "DISPONIVEL";
            tranca.Bicicleta = bicicleta;
            tranca.Status= "OCUPADA";

            var responseTranca = await HttpClient.GetAsync(aluguelAddress + "/funcionario/" + viewModel.FuncionarioId);
            var funcionario = await responseTranca.Content.ReadFromJsonAsync<FuncionarioModel>();
            var body = JsonContent.Create(new EmailDto {
                Email = funcionario?.Email,
                Assunto = "Operação no bicicletário concluída",
                Mensagem = "A Bicicleta " + bicicleta.Id +  " foi adicionada à tranca " + tranca.Id
            });

            await HttpClient.PostAsync(externoAPI + "/enviarEmail", body);

            return tranca;
        }

        public async Task<TrancaModel?> RemoveBicicletaFromTranca(BicicletaRemoveDto viewModel)
        {
            var tranca = dict.ElementAt(viewModel.TrancaId).Value;
            if (tranca.Bicicleta == null)
            {
                return null;
            }
            var bicicleta = _bicicletaService.GetBicicleta(viewModel.BicicletaId);
            bicicleta.Status = viewModel.StatusAcaoReparador;
            tranca.Bicicleta = null;

            var responseTranca = await HttpClient.GetAsync(aluguelAddress + "/funcionario/" + viewModel.FuncionarioId);
            var funcionario = await responseTranca.Content.ReadFromJsonAsync<FuncionarioModel>();
            var body = JsonContent.Create(new EmailDto
            {
                Email = funcionario?.Email,
                Assunto = "Operação no bicicletário concluída",
                Mensagem = "A Bicicleta " + bicicleta.Id + " foi removida, status:" + viewModel.StatusAcaoReparador
            });

            await HttpClient.PostAsync(externoAPI + "/enviarEmail", body);
            return tranca;
        }
    }
}
