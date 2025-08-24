using System.Collections.Generic;
using API_RentMoto.Models;

namespace API_RentMoto.Services
{
    public interface IMotoService
    {
        Moto CreateMoto(Moto moto);
        IEnumerable<Moto> GetMoto(string placa);
        Moto GetMotoById(int id);
        void UpdateMoto(Moto moto, Moto new_moto);
        void UpdatePlacaMoto(int id, string placa);
        void DeleteMoto(int id);
        Moto GetMotoByIdentificador(string identificador);
    }
}