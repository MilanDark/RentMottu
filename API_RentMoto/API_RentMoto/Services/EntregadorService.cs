using API_RentMoto.Models;
using API_RentMoto.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace API_RentMoto.Services
{

    public class EntregadorService : IEntregadorService
    {
        private readonly IEntregadorRepository _repository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public EntregadorService(IEntregadorRepository EntregadorRepository)
        {
            _repository = EntregadorRepository;
        }

  


        #region Funcoes
        private void Add_Photo_Entregador(string imagem_cnh, ref Entregador new_entregador)
        {
            new_entregador.imagem_cnh= imagem_cnh;
        }

        private bool Verify_CNPJ(string CNPJ)
        {
            return _repository.VerifyCNPJ(CNPJ);
        }

        private bool Verify_CNH(string CNH)
        {
            return _repository.Verify_CNH(CNH);
        }

        private bool Verify_File(string extension)
        {
            var allowedExtensions = new[] { ".png", ".bmp" };
            return allowedExtensions.Contains(extension);
        }

        #endregion


        #region Methods

        public IEnumerable<Entregador> GetAll()
        {
            Logger.Info($"GetAllEntregador->Iniciando.");

            return _repository.GetAll();
        }

        public Entregador GetById(int id)
        {
            Logger.Info(string.Format("GetByIdEntregador->{0}", id));

            if (id <= 0)
                throw new InvalidOperationException("Request mal formada");

            var moto = GetById(id);
            if (moto == null)
                throw new InvalidOperationException("Moto não encontrada");


            Logger.Info($"GetByIdEntregador->Finalizando.");
            return moto;
        }

        public Entregador GetByIdentificador(string identificador)
        {
            Logger.Info(string.Format("GetByIdentificadorEntregador->{0}", identificador));

            return _repository.GetByIdentificador(identificador);
        }

        public void Update(int entregadorId, HttpPostedFile file)
        {
            Logger.Info(string.Format("UpdateEntregador->{0}", entregadorId));

            var entregador = GetById(entregadorId);
            if (entregador == null)
                throw new InvalidOperationException("Dados inválidos");

            if (file.ContentLength==0)
                throw new InvalidOperationException("Nenhum arquivo foi enviado");

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!Verify_File(extension))
                throw new InvalidOperationException("Formato inválido. Apenas PNG ou BMP são aceitos.");

            var fileName = $"cnh_{entregadorId}_{DateTime.Now.Ticks}{extension}";
            var filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/CNHs/"), fileName);

            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            file.SaveAs(filePath);
            entregador.imagem_cnh = filePath;

            _repository.Update(entregador);

            Logger.Info($"UpdateEntregador->Finalizando.");
        }


        public void Delete(int id)
        {
            Logger.Info(string.Format("DeleteEntregador->{0}", id));

            if (id <= 0)
                throw new InvalidOperationException("Dados inválidos");

            var moto = GetById(id);
            if (moto == null)
                throw new InvalidOperationException("Moto não encontrada");

            _repository.Delete(id);

            Logger.Info($"DeleteEntregador->Finalizando.");
        }

        public Entregador Add(Entregador entregador)
        {
            Logger.Info(string.Format("AddEntregador->{0}", entregador.identificador));

            if (Verify_CNPJ(entregador.cnpj))
                throw new InvalidOperationException("CNPJ já cadastrado");

            if (Verify_CNH(entregador.numero_cnh))
                throw new InvalidOperationException("CNH já cadastrado");

            return _repository.Add(entregador);
        }
        #endregion
    }
}