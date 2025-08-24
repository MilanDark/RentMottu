using API_RentMoto.Models;
using API_RentMoto.Repositories;
using API_RentMoto.Services;
using System;
using System.Net;
using System.Web;
using System.Web.Http;


namespace API_RentMoto.Controllers
{
    /// <summary>Controladora de Entregadores</summary>
    [RoutePrefix("api/entregadores")]
    public class entregadoresController : ApiController
    {
        #region Constructor
        private readonly IEntregadorService _service;

        public entregadoresController()
        {
            _service = new EntregadorService(new entregadorRepository(new AppDbContext()));
        }

        public entregadoresController(IEntregadorService service)
        {
            _service = service;
        }
        #endregion


        #region Methods


        /// <summary>Adicionar Entregador no sistema</summary>
        /// <param>Entidade Entregador</param>
        /// <returns>OK</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(Entregador entregador)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, new { mensagem = "Dados inválidos" });

            try
            {
                var ret = _service.Add(entregador);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.BadRequest, new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>Busca Entregador pelo ID de cadastro</summary>
        /// <param>ID indetidade do Banco</param>
        /// <returns>Entidade Entregador</returns>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                return Ok(GetById(id));
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.BadRequest, new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>Remover um entregador</summary>
        /// <param>ID Identidade do Banco</param>
        /// <returns>OK</returns>
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.BadRequest, new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>Enviar arquivo de imagem da CNH - Somente PNG e BMP</summary>
        /// <param>ID Identidade do Entregador no Banco e Arquivo Indexado no Formato FILE</param>
        /// <returns>OK</returns>
        [HttpPost]
        [Route("{id:int}")]
        public IHttpActionResult UploadCNH([FromUri(Name = "id")] int entregadorId)
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                HttpPostedFile file = httpRequest.Files[0];

                _service.Update(entregadorId, file);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Content(HttpStatusCode.BadRequest, new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
        #endregion
    }
}


