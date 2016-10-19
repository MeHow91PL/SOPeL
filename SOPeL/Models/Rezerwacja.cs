using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOPeL.Models
{
    public class Rezerwacja
    {
        public Pracownik Pracownik{ get; set; }
        public Pacjent Pacjent { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime DataRezerwacji { get; set; }

        [Display(Name = "Godzina od")]
        public string godzOd { get; set; }

        [Display(Name = "Godzina do")]
        public string godzDo { get; set; }
    }
}