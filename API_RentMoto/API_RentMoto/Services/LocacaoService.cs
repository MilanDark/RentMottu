using API_RentMoto.Models;
using API_RentMoto.Repositories;
using API_RentMoto.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;

namespace API_RentMoto.Services
{
    public class LocacaoService : ILocacaoService

    {

        private readonly ILocacaoRepository _repository;
        private readonly IMotoService _motoService;
        private readonly IEntregadorService _entregadorService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public LocacaoService(ILocacaoRepository LocacaoRepository, IMotoService motoService, IEntregadorService entregadorService)
        {
            _repository = LocacaoRepository;
            _motoService = motoService;
            _entregadorService = entregadorService;
        }



        #region Funcoes
        private void Update_DataDevolucao_Locacao(DateTime? Data_devolucao, ref Locacao locacao)
        {
            locacao.data_devolucao = Data_devolucao;
        }

        private string Verify_Locacao_Rules(ref Locacao locacao)
        {
            if (_motoService.GetMotoByIdentificador(locacao.moto_id) == null)
                return "Não existem MOTOS com essa identificação. Locação cancelada";

            if (_entregadorService.GetByIdentificador(locacao.entregador_id) == null)
                return "Não existem ENTREGADORES com essa identificacao. Locação cancelada";

            if (locacao.plano != 7 && locacao.plano != 15 && locacao.plano != 30 && locacao.plano != 45 && locacao.plano != 50)
                return "O PLANO não existe. Locação cancelada";

            if (locacao.data_inicio.Date != DateTime.Today.AddDays(1).Date)
                return "A Data Início Locação deve ser D+1";

            var entregador = _entregadorService.GetByIdentificador(locacao.entregador_id);
            if (entregador == null)
                return "Entregador não cadastrado";

            if (entregador.tipo_cnh != "A")
                return "Entregador não possui o Tipo de CNH 'A'.";

            return null;
        }


        private double Calculate_Value(Locacao locacao)
        {
            double valor_multa = 0;
            double valor_plano = 0;

            if (locacao.plano == 7) valor_plano = 30;
            if (locacao.plano == 15) valor_plano = 28;
            if (locacao.plano == 30) valor_plano = 22;
            if (locacao.plano == 45) valor_plano = 20;
            if (locacao.plano == 50) valor_plano = 18;

            TimeSpan difF_days = locacao.data_previsao_termino- locacao.data_termino;
            if (difF_days.Days > 0)
            {
                if (locacao.plano == 7)
                    valor_multa = (valor_plano * 0.20) * difF_days.Days;
                else if (locacao.plano != 7)
                    valor_multa = (valor_plano * 0.40) * difF_days.Days;
            }
            else if (difF_days.Days < 0)
            {
                valor_multa = Math.Abs((difF_days.Days) * 50);
            }
            return (valor_plano* locacao.plano )+ valor_multa;
        }
        #endregion






        #region Methods

        public IEnumerable<Locacao> GetAll()
        {
            Logger.Info($"GetAllLocacao->Iniciando...");

            return _repository.GetAll();
        }

        public Locacao GetById(int id)
        {
            Logger.Info($"GetByIdLocacao->{id}");

            if (id <= 0)
                throw new InvalidOperationException("Dados inválidos");

            var ret = GetById(id);
            if (ret == null)
                throw new InvalidOperationException("Locação não encontrada");


            Logger.Info($"GetByIdLocacao->Finalizando.");
            return ret;
        }

        public void Update(int id, Locacao new_locacao)
        {
            Logger.Info($"UpdateLocacao->{id}");

            if (id <= 0 || new_locacao.data_devolucao == null)
                throw new InvalidOperationException("Dados inválidos");

            var ret = GetById(id);
            if (ret == null)
                throw new InvalidOperationException("Locação não encontrada");

            Update_DataDevolucao_Locacao(new_locacao.data_devolucao, ref ret);
            _repository.Update(ret);

            Logger.Info($"UpdateLocacao->Finalizando.");
        }

        public void Delete(int id)
        {
            Logger.Info($"Delete->{id}");

            _repository.Delete(id);

            Logger.Info($"Delete->Finalizando.");
        }

        public double CalculateValue(Locacao locacao)
        {
            return (Calculate_Value(locacao));
        }

        public Locacao Add(Locacao locacao)
        {
            Logger.Info($"AddLocacao->{locacao.moto_id}");

            var msgError = Verify_Locacao_Rules(ref locacao);
            if (!string.IsNullOrEmpty(msgError))
                throw new InvalidOperationException(msgError);

            return _repository.Add(locacao);
        }
        #endregion
    }
}











