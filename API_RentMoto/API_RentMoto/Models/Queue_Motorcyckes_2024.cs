using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API_RentMoto.Models
{
    [Table("queue_motorcyckes_2024")]
    public class Queue_Motorcyckes_2024
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public DateTime timestamp { get; set; } = DateTime.Now;

    }

}



