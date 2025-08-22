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
    [RoutePrefix("api/entregadores")]
    public class entregadoresController : ApiController
    {
        private readonly IEntregadorService _service;

        public entregadoresController()
        {
            _service = new EntregadorService(new entregadorRepository(new AppDbContext()));
        }

        public entregadoresController(IEntregadorService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")] //ok
        public IHttpActionResult Add(Entregador entregador)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, new { mensagem = "Dados inválidos" });

            try
            {
                var ret = _service.Add(entregador);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("{id:int}")]//ok
        public IHttpActionResult Upload_CNH(int id, [FromBody] Entregador entregador)
        {
            if (string.IsNullOrEmpty(entregador.imagem_cnh) || (entregador.imagem_cnh.Length % 4 != 0))
                return Content(HttpStatusCode.BadRequest, new { mensagem = "Dados inválidos" });

            try
            {
                var existingEntregador = _service.GetById(id);
                if (existingEntregador == null)
                    return Content(HttpStatusCode.NotFound, new { mensagem = "Entregador não encontrado" });

                _service.Update(existingEntregador, entregador.imagem_cnh);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return Content(HttpStatusCode.BadRequest, new { mensagem = "Request mal formada" });

                var moto = _service.GetById(id);
                if (moto == null)
                    return Content(HttpStatusCode.NotFound, new { mensagem = "Moto não encontrada" });

                return Ok(moto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[HttpPut]
        //[Route("")]
        //public IHttpActionResult Update([FromBody] Entregador entregador)
        //{
        //    if (!ModelState.IsValid)
        //        return Content(HttpStatusCode.BadRequest, new { mensagem = "Dados inválidos" });

        //    try
        //    {
        //        var existingMoto = _service.GetById(entregador.id);
        //        if (existingMoto == null)
        //            return Content(HttpStatusCode.NotFound, new { mensagem = "Moto não encontrada" });

        //        _service.Update(entregador);
        //        return Ok(entregador);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return Content(HttpStatusCode.BadRequest, new { mensagem = "Dados inválidos" });

                var moto = _service.GetById(id);
                if (moto == null)
                    return Content(HttpStatusCode.NotFound, new { mensagem = "Moto não encontrada" });

                _service.Delete(id);
                return Ok();// n StatusCode(HttpStatusCode.NoContent); // 204 No Content
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}













//namespace API_RentMoto.Controllers
//{
//    [RoutePrefix("api/motos")]
//    public class motosController : ApiController
//    {
//        private readonly API_RentMoto.Services.IMotoService _service;


//    }
//}
