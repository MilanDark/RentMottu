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

        [Index("IX_placa", IsUnique = true)] // define índice único
        [MaxLength(8)]
        public string placa { get; set; }
    }


}