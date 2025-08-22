using System.Collections.Generic;
using API_RentMoto.Models;

namespace API_RentMoto.Services
{
    public interface IMotoService
    {
        Moto CreateMoto(Moto moto);
        Moto CreateMotoExternal(Moto moto); // chama API externa
        IEnumerable<Moto> GetAll();
        Moto GetMotoById(int id);
        Moto GetMotoByPlaca(string placa);
        void UpdateMoto(Moto moto, Moto new_moto);
        void UpdatePlacaMoto(Moto moto, string placa);
        void DeleteMoto(int id);
    }
}