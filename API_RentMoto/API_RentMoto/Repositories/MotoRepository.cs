using System.Collections.Generic;
using System.Linq;
using API_RentMoto.Models;
using API_RentMoto.Repositories.Interfaces;

namespace API_RentMoto.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly AppDbContext _context;

        public MotoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Moto> GetAll() => _context.Moto.ToList();

        public Moto GetById(int id) => _context.Moto.Find(id);

        public Moto GetMotoByPlaca(string placa) => _context.Moto.Where(x=> x.placa== placa).FirstOrDefault();


        public Moto Add(Moto moto)
        {
            _context.Moto.Add(moto);
            _context.SaveChanges();
            return moto;
        }

        public void Update(Moto moto)
        {
            _context.Entry(moto).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var moto = _context.Moto.Find(id);
            if (moto != null)
            {
                _context.Moto.Remove(moto);
                _context.SaveChanges();
            }
        }
    }
}