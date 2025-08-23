using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API_RentMoto.Models;
using API_RentMoto.Repositories.Interfaces;
using API_RentMoto.Services;
using Newtonsoft.Json;

namespace API_RentMoto.Services
{
    public class MotoService : IMotoService
    {
        private readonly IMotoRepository _repository;
        private readonly ILocacaoRepository _locacaoRepository;

        const string QueueName = "Motos_Adicionadas";

        public MotoService(IMotoRepository motoRepository, ILocacaoRepository LocacaoRepository)
        {
            _repository = motoRepository;
            _locacaoRepository = LocacaoRepository;
        }



        #region Functions
        protected void Convert_to_Update_moto(Moto moto, ref Moto new_moto)
        {
            new_moto.id = moto.id;
            new_moto.identificador = moto.identificador;
            new_moto.ano = moto.ano;
            new_moto.modelo = moto.modelo;
            new_moto.placa = moto.placa;
        }

        bool Verify_Motorcycles_Some_Placa(int id, string placa)
        {
            return (_repository.GetMotoByPlacaAndId(id, placa) != null);
        }

        bool Verify_Motorcycles_By_Placa(string placa)
        {
            return (_repository.GetMotoByPlaca(placa.ToUpper().Trim())!=null);
        }

        protected void Ajust_Fields_To_DB(ref Moto moto)
        {
            moto.placa = moto.placa.Trim().ToUpper();
        }

        bool Verify_Motorcycles_By_Placa(int id)
        {
            return (_repository.GetById(id)!=null);
        }

        bool Verify_Rent_By_Moto(string identificadorMoto)
        {
            return _locacaoRepository.Verify_Rent_By_Moto(identificadorMoto);
        }

        void Envia_Pacote_Fila_Teste(Moto moto)
        {
            var Queue = new RabbitMQ();
            Queue.Envia_Pacote_Fila_Teste(JsonConvert.SerializeObject(moto), QueueName);
        }
        #endregion




        #region Methods

        public Moto GetMotoById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Moto> GetAll()
        {
            return _repository.GetAll();
        }

        public Moto GetMotoByPlaca(string placa)
        {

            if (string.IsNullOrEmpty(placa))
                throw new InvalidOperationException("Parâmetro Placa não enviado");

            var ret = _repository.GetMotoByPlaca(placa.ToUpper().Trim());

            if (ret == null)
                throw new InvalidOperationException("Não existem motos cadastradas com esta placa");

            return ret;
        }

        public Moto GetMotoByIdentificador(string identificador)
        {
            return _repository.GetByIdentificador(identificador);
        }

        public void UpdatePlacaMoto(int id, string placa)
        {
            if (id <= 0)
                throw new ArgumentException("Dados inválidos");

            var moto = GetMotoById(id);
            if (moto == null)
                throw new InvalidOperationException("Moto não encontrada");

            if (Verify_Motorcycles_Some_Placa(id,placa))
                throw new InvalidOperationException("Já existe uma Moto diferente cadastrada com a placa que você tenta alterar");

            moto.placa = placa;
            _repository.Update(moto);
        }

        public void UpdateMoto(Moto moto, Moto new_moto)
        {
            Convert_to_Update_moto(new_moto, ref moto);
            _repository.Update(moto);
        }

        public void DeleteMoto(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Dados inválidos");

            var moto = GetMotoById(id);
            if (moto==null)
                throw new InvalidOperationException("Moto não encontrada");

            if(Verify_Rent_By_Moto(moto.identificador))
                throw new InvalidOperationException("A moto não pode ser removida por estar com locação ativa");

            _repository.Delete(id);
        }

        public Moto CreateMoto(Moto moto)
        {
            if (Verify_Motorcycles_By_Placa(moto.placa))
                throw new InvalidOperationException("Já existe uma moto cadastrada com esta placa");

            Ajust_Fields_To_DB(ref moto);

            Envia_Pacote_Fila_Teste(moto);
            return _repository.Add(moto);
        }
        #endregion
    }
}