using System.Collections.Generic;
using System.Linq;
using API_RentMoto.Models;
using API_RentMoto.Repositories.Interfaces;


namespace API_RentMoto.Repositories
{
    public class NotificacaoQueueRepository : INotificacaoQueueRepository
    {

        private readonly AppDbContext _context;

        public NotificacaoQueueRepository(AppDbContext context)
        {
            _context = context;
        }

        public Notificacoes_Cadastro_Motos Add(Notificacoes_Cadastro_Motos msg)
        {
            _context.Notificao_Cadastro_Moto.Add(msg);
            _context.SaveChanges();
            return msg;
        }

    }
}