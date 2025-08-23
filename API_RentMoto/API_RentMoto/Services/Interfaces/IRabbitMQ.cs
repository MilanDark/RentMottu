using API_RentMoto.Models;

namespace API_RentMoto.Services
{
    public interface IRabbitMQ
    {
        bool Queue_Moto_Add(string texto, string nomeFila);
        Moto Queue_Moto_Read();
    }
}