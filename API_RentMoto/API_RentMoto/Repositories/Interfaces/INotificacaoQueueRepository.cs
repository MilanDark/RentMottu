using API_RentMoto.Models;

namespace API_RentMoto.Repositories
{
    public interface INotificacaoQueueRepository
    {
        Notificacoes_Cadastro_Motos Add(Notificacoes_Cadastro_Motos msg);
    }
}