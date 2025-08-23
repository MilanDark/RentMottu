using System.Collections.Generic;
using API_RentMoto.Models;

namespace API_RentMoto.Services
{
    public interface ILocacaoService
    {
        Locacao Add(Locacao entregador);
        IEnumerable<Locacao> GetAll();
        Locacao GetById(int id);
        void Update(Locacao locacao, Locacao new_locacao);
        void Delete(int id);
        double CalculateValue(Locacao locacao);
    }
}