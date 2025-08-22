using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using API_RentMoto.Models;
using API_RentMoto.Repositories.Interfaces;
using Newtonsoft.Json;

namespace API_RentMoto.Services
{
    public interface IEntregadorService
    {
        Entregador Add(Entregador entregador);
        IEnumerable<Entregador> GetAll();
        Entregador GetById(int id);
        void Update(Entregador entregador, string imagem_cnh);
        void Delete(int id);
    }
}




