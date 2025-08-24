using API_RentMoto.Models;
using API_RentMoto.Repositories;
using NLog;

namespace API_RentMoto.Services
{
    public class NotificacaoQueueService
    {
        private readonly INotificacaoQueueRepository _repository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public NotificacaoQueueService(INotificacaoQueueRepository notificacaoQueueRepository)
        {
            _repository = notificacaoQueueRepository;
        }

        #region Methods
        public Notificacoes_Cadastro_Motos Add(Notificacoes_Cadastro_Motos msg)
        {
            Logger.Info(string.Format("Notificacao_Cadastro_Motos->{0}", msg));

            return _repository.Add(msg);
        }
        #endregion
    }
}











