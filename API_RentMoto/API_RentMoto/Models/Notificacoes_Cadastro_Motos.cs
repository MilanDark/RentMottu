using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API_RentMoto.Models
{
    [Table("notificacoes_cadastro_motos")]
    public class Notificacoes_Cadastro_Motos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public DateTime timestamp { get; set; } = DateTime.Now;

        [MaxLength(200)]
        public string Mensagem { get; set; }
    }

}



