using API_RentMoto.Models;
using API_RentMoto.Repositories;

namespace API_RentMoto.Services
{
    public class NotificacaoQueueService
    {
        private readonly INotificacaoQueueRepository _repository;

        public NotificacaoQueueService(INotificacaoQueueRepository notificacaoQueueRepository)
        {
            _repository = notificacaoQueueRepository;
        }

        #region Methods
        public Notificacoes_Cadastro_Motos Add(Notificacoes_Cadastro_Motos msg)
        {
            return _repository.Add(msg);
        }
        #endregion
    }
}











