using API_RentMoto.Models;
using API_RentMoto.Repositories;
using API_RentMoto.Services;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace API_RentMoto.Controllers
{
    [RoutePrefix("api/motos")]
    public class motosController : ApiController
    {
        private readonly API_RentMoto.Services.IMotoService _service;

        public motosController()
        {
            _service = new MotoService(new MotoRepository(new AppDbContext()));
        }

        public motosController(IMotoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var motos = _service.GetAll();
                if (motos == null || !motos.Any())
                    return NotFound();

                return Ok(motos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("")] //ok
        public IHttpActionResult Create(Moto moto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var ret = _service.CreateMoto(moto);
                return Created($"api/motos/{ret.Id}", ret); ;
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetMotoById(int id)
        {
            try
            {
                var moto = _service.GetMotoById(id);
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
        [Route("{id:int}")]
        public IHttpActionResult UpdateMoto(int id, [FromBody] Moto moto)
        {
            if (moto == null || id != moto.Id)
                return BadRequest("Invalid data.");

            try
            {
                var existingMoto = _service.GetMotoById(id);
                if (existingMoto == null)
                    return NotFound();

                _service.UpdateMoto(moto);
                return StatusCode(HttpStatusCode.NoContent); // 204 No Content
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteMoto(int id)
        {
            try
            {
                var moto = _service.GetMotoById(id);
                if (moto == null)
                    return NotFound();

                _service.DeleteMoto(id);
                return StatusCode(HttpStatusCode.NoContent); // 204 No Content
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}