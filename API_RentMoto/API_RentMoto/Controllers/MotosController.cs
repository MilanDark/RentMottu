using API_RentMoto.Models;
using API_RentMoto.Repositories;
using API_RentMoto.Services;
using System;
using System.Net;
using System.Web.Http;


namespace API_RentMoto.Controllers
{
    /// <summary>Controladora de MOTOS</summary>
    [RoutePrefix("api/motos")]
    public class motosController : ApiController
    {
        #region Constructor
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
        #endregion


        #region Methods

        /// <summary>Insere uma Moto no sistema</summary>
        /// <param>Entidade MOTO</param>
        /// <returns>OK</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateMoto(Moto moto)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, new { mensagem = "Dados inválidos" });

            try
            {
                _service.CreateMoto(moto);
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

        /// <summary>Busca uma Moto pela Placa</summary>
        /// <param>string Placa</param>
        /// <returns>Entidade Moto</returns>
        [HttpGet]
        [Route("{id:int}/placa")]
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

        /// <summary>Retorna uma Lista com Todas as Motos do Sistema</summary>
        /// <param></param>
        /// <returns>Lista de Entidade MOTO</returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllMoto()
        {
            try
            {
                return Ok(_service.GetAll());
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

        /// <summary>Atualizar a placa de uma moto já cadastrada no sistema</summary>
        /// <param>Inteiro ID Identidade da moto no Banco de dados, Entidade MOTO com a placa que deseja persistir</param>
        /// <returns>Placa modificada com sucesso</returns>
        [HttpPut]
        [Route("{id:int}")]
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

        /// <summary>Busca uma Moto no sistema pelo ID Identidade</summary>
        /// <param>Inteiro ID Identidade da Moto no Branco de dados</param>
        /// <returns>Entidade MOTO encontrada</returns>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetMotoById(int id)
        {
            try
            {
                return Ok(_service.GetMotoById(id));
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

        /// <summary>Apagar uma Moto já gravada no sistema</summary>
        /// <param>INTEIRO - ID Identidade da Moto no Banco de dados</param>
        /// <returns>OK</returns>
        [HttpDelete]
        [Route("{id:int}")] 
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
        #endregion
    }
}