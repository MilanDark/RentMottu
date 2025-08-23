using System.Collections.Generic;
using System.Web;
using API_RentMoto.Models;

namespace API_RentMoto.Services
{
    public interface IEntregadorService
    {
        Entregador Add(Entregador entregador);
        IEnumerable<Entregador> GetAll();
        Entregador GetById(int id);
        void Update(int entregadorId, HttpPostedFile file);
        void Delete(int id);
        Entregador GetByIdentificador(string identificador);
    }
}




