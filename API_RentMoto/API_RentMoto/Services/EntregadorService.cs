using API_RentMoto.Models;
using API_RentMoto.Repositories;
using API_RentMoto.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_RentMoto.Services
{

    public class EntregadorService : IEntregadorService
    {
        private readonly IEntregadorRepository _repository;

        public EntregadorService(IEntregadorRepository EntregadorRepository)
        {
            _repository = EntregadorRepository;
        }






        #region Funcoes
        protected void Add_Photo_Entregador(string imagem_cnh, ref Entregador new_entregador)
        {
            new_entregador.imagem_cnh= imagem_cnh;
        }
        #endregion


        #region Methods

        public IEnumerable<Entregador> GetAll()
        {
            return _repository.GetAll();
        }

        public Entregador GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Entregador entregador, string imagem_cnh)
        {
            Add_Photo_Entregador(imagem_cnh, ref entregador);
            _repository.Update(entregador);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Entregador Add(Entregador entregador)
        {
            return _repository.Add(entregador);
        }
        #endregion
    }
}