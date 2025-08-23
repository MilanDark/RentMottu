using API_RentMoto.Models;
using API_RentMoto.Repositories;
using API_RentMoto.Services;
using System;
using System.Net;
using System.Web.Http;

namespace API_RentMoto.Controllers
{
    [RoutePrefix("api/locacao")]
    public class locacaoController : ApiController
    {

        #region Constructor
        private readonly ILocacaoService _service;

        public locacaoController()
        {
            var db = new AppDbContext();
            var motoRepo = new MotoRepository(db);
            var entregadorRepo = new entregadorRepository(db);
            var LocacaoRepo = new locacaoRepository(db);

            var motoService = new MotoService(motoRepo, LocacaoRepo);
            var entregadorService = new EntregadorService(entregadorRepo);

            _service = new LocacaoService(new locacaoRepository(new AppDbContext())    , motoService, entregadorService);
        }

        public locacaoController(ILocacaoService service)
        {
            _service = service;
        }
        #endregion


        #region Methods
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(Locacao locacao)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, new { mensagem = "Dados inválidos" });

            try
            {
                _service.Add(locacao);
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

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Devolucao(int id, [FromBody] Locacao locacao)
        {
            try
            {
                _service.Update(id, locacao);
                return Content(HttpStatusCode.OK, new { mensagem = "Data de devolução informada com sucesso" });
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
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult GetContractValue(Locacao locacao)
        {
            try
            {
                return Ok(_service.CalculateValue(locacao));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion
    }
}


