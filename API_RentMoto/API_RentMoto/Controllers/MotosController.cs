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
            _service = new MotoService(new MotoRepository(new AppDbContext()));
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
                if (string.IsNullOrEmpty(placa )) return NotFound();

                var moto = _service.GetMotoByPlaca(placa);
                if (moto == null)
                    return NotFound();

                return Ok(moto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{id:int}/{placa}")]//ok
        public IHttpActionResult UpdatePlacaMoto(int id, string placa)
        {

            try
            {
                if(id<=0)
                    return Content(HttpStatusCode.BadRequest, new { mensagem = "Dados inválidos" });

                var existingMoto = _service.GetMotoById(id);
                if (existingMoto == null)
                    return Content(HttpStatusCode.NotFound, new { mensagem = "Moto não encontrada" });

                _service.UpdatePlacaMoto(existingMoto, placa);
                return Content(HttpStatusCode.OK, new { mensagem = "Placa modificada com sucesso" });
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
                if(id<=0)
                    return Content(HttpStatusCode.BadRequest, new { mensagem = "Dados inválidos" });

                var moto = _service.GetMotoById(id);
                if (moto == null)
                    return Content(HttpStatusCode.NotFound, new { mensagem = "Moto não encontrada" });

                _service.DeleteMoto(id);
                return Ok();// n StatusCode(HttpStatusCode.NoContent); // 204 No Content
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}