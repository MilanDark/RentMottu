using System.Collections.Generic;
using System.Linq;
using API_RentMoto.Models;
using API_RentMoto.Repositories.Interfaces;

namespace API_RentMoto.Repositories
{
    public class locacaoRepository : ILocacaoRepository
    {
        private readonly AppDbContext _context;

        public locacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Locacao> GetAll() => _context.Locacao.ToList();

        public Locacao GetById(int id) => _context.Locacao.Find(id);

        public Locacao Add(Locacao locacao)
        {
            _context.Locacao.Add(locacao);
            _context.SaveChanges();
            return locacao;
        }

        public void Update(Locacao locacao)
        {
            _context.Entry(locacao).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var reg = _context.Locacao.Find(id);
            if (reg != null)
            {
                _context.Locacao.Remove(reg);
                _context.SaveChanges();
            }
        }

    }
}

