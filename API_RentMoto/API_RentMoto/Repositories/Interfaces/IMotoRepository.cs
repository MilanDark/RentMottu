
using System.Collections.Generic;
using API_RentMoto.Models;


namespace API_RentMoto.Repositories.Interfaces
{
    public interface IMotoRepository
    {
        IEnumerable<Moto> GetAll();
        Moto GetById(int id);
        Moto Add(Moto moto);
        void Update(Moto moto);
        void Delete(int id);
    }
}



