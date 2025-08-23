using System;
using API_RentMoto.Models;
using Newtonsoft.Json;

namespace MotoQueueConsumer
{
    class Program
    {

        #region Functions
        private static void Publish_Log(Moto log)
        {
            var dbContext = new AppDbContext();
            var repo = new API_RentMoto.Repositories.NotificacaoQueueRepository(dbContext);
            var notificacaoService = new API_RentMoto.Services.NotificacaoQueueService(repo);

            var msgTeste = new Notificacoes_Cadastro_Motos { Mensagem = "Realizado cadastro da moto " + log.modelo + " com placa " + log.placa + " de ano 2024" };

            var resultado = notificacaoService.Add(msgTeste);
        }
        #endregion



        #region Methods
        static void Main(string[] args)
        {
            Console.WriteLine("Buscando 1 registro da fila...");

            Moto log = new Moto();
            try
            {
                var rQueue = new API_RentMoto.Services.RabbitMQ();
                var nomeFila = "Motos_Adicionadas";
                rQueue.TopicName = "RentMoto";
                rQueue.CreateConnection();
                rQueue.CreateInfrastructure(nomeFila);
                string receivedMessage = rQueue.Receive(nomeFila);

                if (receivedMessage != null)
                {
                    log = JsonConvert.DeserializeObject<Moto>(receivedMessage);
                    if (log.ano == 2024)
                    {
                        Console.WriteLine("A moto " + log.modelo + " de ano 2024 foi cadastrada no sistema.");
                        Publish_Log(log);
                        Console.ReadKey();
                    }
                }
                rQueue.closeConnection();

                Console.WriteLine("Aguardando mensagens. Pressione [enter] para sair.");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRO: " + e.Message);
                Console.ReadLine();
            }
        }
        #endregion
    }
}