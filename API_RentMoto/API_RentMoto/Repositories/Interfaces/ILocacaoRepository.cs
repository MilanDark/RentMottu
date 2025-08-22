

using System.Collections.Generic;
using API_RentMoto.Models;

namespace API_RentMoto.Repositories.Interfaces
{
    public interface ILocacaoRepository
    {
        Locacao Add(Locacao entregador);
        IEnumerable<Locacao> GetAll();
        Locacao GetById(int id);
        void Update(Locacao entregador);
        void Delete(int id);
    }
}


