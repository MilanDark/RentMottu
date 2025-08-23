using API_RentMoto.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RabbitMQ.Client;
using System.Text;



namespace API_RentMoto.Controllers
{
    public class HomeController : ApiController
    {

        void Envia_Pacote_Fila_Teste(string texto)
        {

            var rQueue = new API_RentMoto.Services.RabbitMQ();
            var nomeFila = "Motos_Adicionadas";
            rQueue.TopicName = "ANTT";
            rQueue.CreateConnection();
            rQueue.CreateInfrastructure(nomeFila);
            try
            {
                while (true)
                {
                        var pacote = JsonConvert.SerializeObject(texto, Newtonsoft.Json.Formatting.None);
                        rQueue.Publish(pacote, nomeFila);
                        rQueue.closeConnection();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
        [HttpPost]
        public IHttpActionResult TesteAdd(string texto = "")
        {
            try
            {
                Envia_Pacote_Fila_Teste(texto);
                return Ok("Enviado para a fila...");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        
        }

        [HttpGet]
        public IHttpActionResult TesteRead()
        {
            try
            {

                //var rQueue = new API_RentMoto.Services.RabbitMQ();
                //var nomeFila = "Queue";
                //rQueue.TopicName = "ANTT";
                //rQueue.CreateConnection();
                //rQueue.CreateInfrastructure(nomeFila);
                //string receivedMessage = "";

                //while (true)
                //{
                //    try
                //    {
                //        receivedMessage = rQueue.Receive(nomeFila);
                //        if (receivedMessage != null)
                //        {
                //            var Pacote = new ViagemModel();
                //            var log = JsonConvert.DeserializeObject<ViagemModel_Rabbit_v3>(receivedMessage);
                //            Pacote.PartitionKey = log.PartitionKey;
                //            int result = DateTime.Compare(log.dataHoraEvento, DateTime.Now);
                //            break;
                //        }
                //        else
                //        {
                //            rQueue.closeConnection();
                //            break;
                //        }
                //    }
                //    catch (MessagingException e)
                //    { }
                //}


                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }























        //[HttpGet]
        //public string TesteList()
        //{
        //    using (var context = new AppDbContext())
        //    {
        //        return JsonConvert.SerializeObject(context.Teste.ToList());
        //    }
        //}


 
    }



}
