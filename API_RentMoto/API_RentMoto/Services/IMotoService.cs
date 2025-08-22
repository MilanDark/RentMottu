using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using API_RentMoto.Models;
using API_RentMoto.Repositories.Interfaces;
using Newtonsoft.Json;

namespace API_RentMoto.Services
{
    public interface IMotoService
    {
        Moto CreateMoto(Moto moto);
        Moto CreateMotoExternal(Moto moto); // chama API externa
        IEnumerable<Moto> GetAll();
        Moto GetMotoById(int id);
        void UpdateMoto(Moto moto);
        void DeleteMoto(int id);
    }

    public class MotoService : IMotoService
    {
        private readonly IMotoRepository _repository;

        public MotoService(IMotoRepository motoRepository)
        {
            _repository = motoRepository;
        }

        public IEnumerable<Moto> GetAll()
        {
            return _repository.GetAll();
        }

        public Moto GetMotoById(int id)
        {
            return _repository.GetById(id);
        }

        public void UpdateMoto(Moto moto)
        {
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
    }
}