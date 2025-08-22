using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_RentMoto.Models
{
    [Table("moto")]
    public class Moto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(100)]
        public string identificador { get; set; }

        [Required]
        [MaxLength(100)]
        public string modelo { get; set; }

        [Required]
        public int ano { get; set; }

        [MaxLength(50)]
        public string placa { get; set; }
    }


}