using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Models
{

    [Table("Wizyty")]
    public class Wizyta 
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public DateTime DataWizyty { get; set; }

        [Display(Name = "Godzina od")]
        public string godzOd { get; set; }

        [Display(Name = "Godzina do")]
        public string godzDo { get; set; }

        [Display(Name = "Data ost. mod.")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataModyfikacji { get; set; }

        public int PracownikID { get; set; }
        public int PacjentID { get; set; }
        public int? RezerwacjaId { get; set; }

        public virtual Pracownik Pracownik { get; set; }
        public virtual Pacjent Pacjent { get; set; }
        public virtual Rezerwacja Rezerwacja { get; set; }



        [Display(Name = "Rozpoznanie")]
        public string Rozpoznanie { get; set; }

        [Display(Name = "Badadnie")]
        public string Badanie { get; set; }

        [Display(Name = "Wywiad")]
        public string Wywiad { get; set; }

        [Display(Name = "Zalecenia")]
        public string Zalecenia { get; set; }

        [Display(Name = "Skierowanie")]
        public string Skierowanie { get; set; }

    }
}