using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOPeL.Models
{
    public class Rezerwacja
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public Pracownik Pracownik{ get; set; }
        public Pacjent Pacjent { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime DataRezerwacji { get; set; }

        [StringLength(5)]
        public string GodzinaRezerwacji { get; set; }
    }
}