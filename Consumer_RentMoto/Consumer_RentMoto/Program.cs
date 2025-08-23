using System;
using System.Threading;
using API_RentMoto.Models;
using Newtonsoft.Json;
using NLog;


namespace MotoQueueConsumer
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
            Logger.Info("Console consumer iniciado...");
            Thread.Sleep(2000);

            Logger.Info("Buscando 1 registro da fila...");
            Thread.Sleep(5000);

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
                        Logger.Info("A moto " + log.modelo + " de ano 2024 foi cadastrada no sistema.");
                        Publish_Log(log);
                        Console.ReadKey();
                    }
                }
                rQueue.closeConnection();

                Logger.Info("Aguardando mensagens. Pressione [enter] para sair.");
                Console.ReadKey();

            }
            catch (Exception e)
            {
                Logger.Info("ERRO:" + e.Message);
                Console.ReadLine();
            }
        }
        #endregion
    }
}








