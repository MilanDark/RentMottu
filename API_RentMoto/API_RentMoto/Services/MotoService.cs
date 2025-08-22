using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API_RentMoto.Models;
using API_RentMoto.Repositories.Interfaces;
using API_RentMoto.Services;

namespace API_RentMoto.Services
{
    public class MotoService : IMotoService
    {
        private readonly IMotoRepository _repository;

        public MotoService(IMotoRepository motoRepository)
        {
            _repository = motoRepository;
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

        #endregion

        #region Methods

        public IEnumerable<Moto> GetAll()
        {
            return _repository.GetAll();
        }

        public Moto GetMotoByPlaca(string placa)
        {
            return _repository.GetMotoByPlaca(placa);
        }

        public Moto GetMotoById(int id)
        {
            return _repository.GetById(id);
        }

        public void UpdatePlacaMoto(Moto moto, string placa)
        {
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
            _repository.Delete(id);
        }

        public Moto CreateMoto(Moto moto)
        {
            return _repository.Add(moto);
        }

        public Moto CreateMotoExternal(Moto moto)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}