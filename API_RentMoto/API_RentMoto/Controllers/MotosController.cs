using API_RentMoto.Models;
using API_RentMoto.Repositories;
using API_RentMoto.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace API_RentMoto.Controllers
{
    [RoutePrefix("api/motos")]
    public class motosController : ApiController
    {
        private readonly IMotoService _service;

        public motosController()
        {
            var db = new AppDbContext();
            var motoRepo = new MotoRepository(db);
            var entregadorRepo = new entregadorRepository(db);
            var LocacaoRepo = new locacaoRepository(db);

            _service = new MotoService(new MotoRepository(new AppDbContext()) , LocacaoRepo);
        }

        public motosController(IMotoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")] //ok
        public IHttpActionResult Create(Moto moto)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, new { mensagem = "Dados inválidos" });

            try
            {
                var ret = _service.CreateMoto(moto);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.NotFound, new { mensagem = ex.Message });
            }
            catch (Exception ex) 
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("")]//ok
        public IHttpActionResult GetMotoByPlaca(string placa = null)
        {
            try
            {
                return Ok(_service.GetMotoByPlaca(placa));
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.NotFound, new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}")]//ok
        public IHttpActionResult UpdatePlacaMoto(int id, [FromBody] Moto moto)
        {
            try
            {
                _service.UpdatePlacaMoto(id, moto.placa);
                return Content(HttpStatusCode.OK, new { mensagem = "Placa modificada com sucesso" });
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.NotFound, new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}")]//ok
        public IHttpActionResult GetMotoById(int id)
        {
            try
            {
                if(id <=0)
                    return Content(HttpStatusCode.BadRequest, new { mensagem = "Request mal formada" });

                var moto = _service.GetMotoById(id);
                if (moto == null)
                    return Content(HttpStatusCode.NotFound, new { mensagem = "Moto não encontrada" });

                return Ok(moto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("")]//ok
        public IHttpActionResult UpdateMoto([FromBody] Moto moto)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, new { mensagem = "Dados inválidos" });

            try
            {
                var existingMoto = _service.GetMotoById(moto.id);
                if (existingMoto == null)
                    return Content(HttpStatusCode.NotFound, new { mensagem = "Moto não encontrada" });

                _service.UpdateMoto(existingMoto, moto);
                return Ok(moto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id:int}")] //ok
        public IHttpActionResult DeleteMoto(int id)
        {
            try
            { 
                _service.DeleteMoto(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.NotFound, new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}