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

        [HttpGet]
        public IHttpActionResult TesteRead()
        {
            Moto log = new Moto();
            try
            {
                var rQueue = new API_RentMoto.Services.RabbitMQ();
                var nomeFila = "Motos_Adicionadas";
                rQueue.TopicName = "RentMoto";
                rQueue.CreateConnection();
                rQueue.CreateInfrastructure(nomeFila);
                string receivedMessage = "";

                receivedMessage = rQueue.Receive(nomeFila);
                if (receivedMessage != null)
                {
                    var Pacote = new Moto();
                    //Pacote.PartitionKey = log.PartitionKey;
                    log = JsonConvert.DeserializeObject<Moto>(receivedMessage);
                }
                else
                    rQueue.closeConnection();

                return Ok(log);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }



}
