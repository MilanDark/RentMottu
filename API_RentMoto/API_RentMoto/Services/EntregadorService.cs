using API_RentMoto.Models;
using API_RentMoto.Repositories;
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
            return _repository.GetAll();
        }

        public Entregador GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Entregador GetByIdentificador(string identificador)
        {
            return _repository.GetByIdentificador(identificador);
        }

        //public void Update(Entregador entregador, string imagem_cnh)
        //{
        //    Add_Photo_Entregador(imagem_cnh, ref entregador);
        //    _repository.Update(entregador);
        //}

        public void Update(int entregadorId, HttpPostedFile file)
        {
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
        }


        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Entregador Add(Entregador entregador)
        {
            if (Verify_CNPJ(entregador.cnpj))
                throw new InvalidOperationException("CNPJ já cadastrado");

            if (Verify_CNH(entregador.numero_cnh))
                throw new InvalidOperationException("CNH já cadastrado");

            return _repository.Add(entregador);
        }
        #endregion
    }
}