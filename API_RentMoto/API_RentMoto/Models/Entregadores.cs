using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_RentMoto.Models
{
    [Table("entregador")]
    public class Entregador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string identificador { get; set; }

        [Required]
        [MaxLength(100)]
        public string nome { get; set; }

        [Required]
        [MaxLength(14)]
        public string cnpj  { get; set; }

        public DateTime data_nascimento { get; set; }

        [Required]
        [MaxLength(20)]
        public string numero_cnh{ get; set; }

        [Required]
        [MaxLength(2)]
        public string tipo_cnh{ get; set; }

        public string imagem_cnh { get; set; }
    }

}





