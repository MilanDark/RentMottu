using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_RentMoto.Models
{
    [Table("locacao")]
    public class Locacao
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        [Required]
        public string entregador_id { get; set; }

        [Required]
        public string moto_id { get; set; }

        [Required]
        public DateTime data_inicio { get; set; }

        [Required]
        public DateTime data_termino { get; set; }

        [Required]
        public DateTime data_previsao_termino { get; set; }

        [Required]
        public int plano { get; set; }

        public DateTime? data_devolucao { get; set; }


        #region Not_Mapped_Fields

        [NotMapped]
        [Column(TypeName = "numeric(10,2)")]
        public decimal valor_diaria
        {
            get
            {
                switch (plano)
                {
                    case 7:
                        return 30;
                    case 15:
                        return 28;
                    case 30:
                        return 22;
                    case 45:
                        return 20;
                    case 50:
                        return 18;
                    default:
                        return 0;
                }
            }
        }


        #endregion




    }

}



