using API_RentMoto.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace API_RentMoto.Controllers
{
    public class HomeController : ApiController
    {
        
        [HttpPost]
        public IHttpActionResult TesteAdd(string Test_Text = "")
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Teste.Add(new Teste { Retorno = Test_Text });
                    context.SaveChanges();
                }

                return Ok("Registro Inserido");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        
        }


        [HttpGet]
        public string TesteList()
        {
            using (var context = new AppDbContext())
            {
                return JsonConvert.SerializeObject(context.Teste.ToList());
            }
        }


        //[HttpGet]
        //public string TesteList2()
        //{
        //    using (var context = new AppDbContext())
        //    {
        //        var products = context.Teste.Where(p => p.ID == 1).ToList();
        //    }

        //    return "OK!";
        //}
    }



}
