using System.Collections.Generic;
using System.Linq;
using API_RentMoto.Models;

namespace API_RentMoto.Repositories
{
    public class entregadorRepository : IEntregadorRepository
    {
        private readonly AppDbContext _context;

        public entregadorRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Entregador> GetAll() => _context.Entregador.ToList();

        public Entregador GetById(int id) => _context.Entregador.Find(id);

        public Entregador GetByIdentificador(string identificador) => _context.Entregador.Where(x => x.identificador == identificador).FirstOrDefault();

        public bool VerifyCNPJ(string CNPJ) => _context.Entregador.Where(x => x.cnpj == CNPJ).Count() > 0;

        public bool Verify_CNH(string CNH) => _context.Entregador.Where(x => x.numero_cnh == CNH).Count() > 0;

        public Entregador Add(Entregador entregador)
        {
            _context.Entregador.Add(entregador);
            _context.SaveChanges();
            return entregador;
        }

        public void Update(Entregador entregador)
        {
            _context.Entry(entregador).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var moto = _context.Entregador.Find(id);
            if (moto != null)
            {
                _context.Entregador.Remove(moto);
                _context.SaveChanges();
            }
        }
    }
}


