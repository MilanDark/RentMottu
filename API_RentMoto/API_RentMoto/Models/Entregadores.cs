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
        [Index("IX_cnpj", IsUnique = true)]
        public string cnpj  { get; set; }

        public DateTime data_nascimento { get; set; }

        [Required]
        [MaxLength(20)]
        [Index("IX_cnh", IsUnique = true)] 
        public string numero_cnh{ get; set; }

        [Required]
        [MaxLength(2)]
        [RegularExpression("^(A|B|AB)$", ErrorMessage = "Tipo de CNH inválido. Apenas A, B ou AB são permitidos.")]
        public string tipo_cnh{ get; set; }

        public string imagem_cnh { get; set; }
    }

}





