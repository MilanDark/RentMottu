using System.Collections.Generic;
using API_RentMoto.Models;

namespace API_RentMoto.Repositories
{
    public interface IEntregadorRepository
    {
        bool Verify_CNH(string CNH);
        Entregador Add(Entregador  entregador);
        IEnumerable<Entregador> GetAll();
        Entregador GetById(int id);
        void Update(Entregador entregador);
        bool VerifyCNPJ(string CNPJ);
        void Delete(int id);
        Entregador GetByIdentificador(string identificador);
    }
}



