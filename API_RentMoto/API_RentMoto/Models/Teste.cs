using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_RentMoto.Models
{
    [Table("Teste")]
    public class Teste
    {
        public int ID { get; set; }

        public string Retorno { get; set; }
    }
}