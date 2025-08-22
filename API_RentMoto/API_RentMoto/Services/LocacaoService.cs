using API_RentMoto.Models;
using API_RentMoto.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace API_RentMoto.Services
{
    public class LocacaoService : ILocacaoService

    {
        private readonly ILocacaoRepository _repository;

        public LocacaoService(ILocacaoRepository LocacaoRepository)
        {
            _repository = LocacaoRepository;
        }

        #region Funcoes
        protected void Update_DataDevolucao_Locacao(DateTime? Data_devolucao, ref Locacao locacao)
        {
            locacao.data_devolucao= Data_devolucao;
        }
        #endregion






        #region Methods

        public IEnumerable<Locacao> GetAll()
        {
            return _repository.GetAll();
        }

        public Locacao GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Locacao locacao, Locacao new_locacao)
        {
            Update_DataDevolucao_Locacao(new_locacao.data_devolucao, ref locacao);
            _repository.Update(locacao);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Locacao Add(Locacao locacao)
        {
            return _repository.Add(locacao);
        }
        #endregion
    }
}










 