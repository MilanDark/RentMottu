using System;
using System.Collections.Generic;
using API_RentMoto.Models;
using API_RentMoto.Repositories.Interfaces;
using Newtonsoft.Json;
using NLog;

namespace API_RentMoto.Services
{
    public class MotoService : IMotoService
    {
        private readonly IMotoRepository _repository;
        private readonly ILocacaoRepository _locacaoRepository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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

        void Queue_Moto_Add(Moto moto)
        {
            var Queue = new RabbitMQ();
            Queue.Queue_Moto_Add(JsonConvert.SerializeObject(moto), QueueName);
        }
        #endregion




        #region Methods

        public Moto GetMotoById(int id)
        {
            Logger.Info(string.Format("GetMOtoById->{0}", id));

            if (id <= 0)
                throw new ArgumentException("Dados inválidos");

            var moto = _repository.GetById(id);
            if (moto == null)
                throw new ArgumentException("Moto não encontrada");


            Logger.Info($"UpdateMoto->Finalizando");

            return moto;
        }

        public IEnumerable<Moto> GetAll()
        {
            Logger.Info($"GetAll->Iniciando");

            return _repository.GetAll();
        }

        public Moto GetMotoByPlaca(string placa)
        {
            Logger.Info(string.Format("GetMotoByPlaca->{0}", placa));

            if (string.IsNullOrEmpty(placa))
                throw new InvalidOperationException("Parâmetro Placa não enviado");

            var ret = _repository.GetMotoByPlaca(placa.ToUpper().Trim());

            if (ret == null)
                throw new InvalidOperationException("Não existem motos cadastradas com esta placa");

            Logger.Info($"GetMotoByPlaca->Finalizando.");
            return ret;
        }

        public Moto GetMotoByIdentificador(string identificador)
        {
            Logger.Info(string.Format("GetMotoByIdentificador->{0}", identificador));

            return _repository.GetByIdentificador(identificador);
        }

        public void UpdatePlacaMoto(int id, string placa)
        {
            Logger.Info(string.Format("UpdatePlacaMOto->{0}-{1}", id, placa));

            if (id <= 0)
                throw new ArgumentException("Dados inválidos");

            var moto = GetMotoById(id);
            if (moto == null)
                throw new InvalidOperationException("Moto não encontrada");

            if (Verify_Motorcycles_Some_Placa(id,placa))
                throw new InvalidOperationException("Já existe uma Moto diferente cadastrada com a placa que você tenta alterar");

            moto.placa = placa;
            _repository.Update(moto);

            Logger.Info($"UpdateMoto->Finalizando.");
        }

        public void UpdateMoto(Moto moto, Moto new_moto)
        {
            Logger.Info(string.Format("UpdateMoto->{0}", moto.id));

            Convert_to_Update_moto(new_moto, ref moto);
            _repository.Update(moto);

            Logger.Info($"UpdateMoto->Finalizando");
        }

        public void DeleteMoto(int id)
        {
            Logger.Info(string.Format("DeleteMoto->{0}", id));

            if (id <= 0)
                throw new ArgumentException("Dados inválidos");

            var moto = GetMotoById(id);
            if (moto==null)
                throw new InvalidOperationException("Moto não encontrada");

            if(Verify_Rent_By_Moto(moto.identificador))
                throw new InvalidOperationException("A moto não pode ser removida por estar com locação ativa");


            _repository.Delete(id);

            Logger.Info($"DeleteMoto->Finalizado.");
        }

        public Moto CreateMoto(Moto moto)
        {
            Logger.Info(string.Format("CreateMoto->{0}", moto.identificador));

            if (Verify_Motorcycles_By_Placa(moto.placa))
            {
                throw new InvalidOperationException("Já existe uma moto cadastrada com esta placa");
            }

            Ajust_Fields_To_DB(ref moto);

            Queue_Moto_Add(moto);

            Logger.Info($"CreateMoto->Gravando e finalizando método.");
            return _repository.Add(moto);
        }
        #endregion
    }
}