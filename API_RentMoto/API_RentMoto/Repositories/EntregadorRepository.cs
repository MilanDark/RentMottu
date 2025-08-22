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


