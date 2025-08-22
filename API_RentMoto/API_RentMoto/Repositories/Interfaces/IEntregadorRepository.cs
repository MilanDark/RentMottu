using System.Collections.Generic;
using API_RentMoto.Models;

namespace API_RentMoto.Repositories
{
    public interface IEntregadorRepository
    {
        Entregador Add(Entregador  entregador);
        IEnumerable<Entregador> GetAll();
        Entregador GetById(int id);
        void Update(Entregador entregador);
        void Delete(int id);
    }
}



