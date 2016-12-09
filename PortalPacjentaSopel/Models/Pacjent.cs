using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalPacjenta.Models
{
    [Table("Pacjenci")]
    public class Pacjent : Osoba
    {

        public Pacjent()
        {
            Aktw = "T";
        }
        [DataType(DataType.Date)]
        [RegularExpression(@"([0-9]{4}-[0-9]{2}-[0-9]{2})$")]
        public DateTime? DataUrodzenia { get; set; }

        [RegularExpression(@"([M,K]{1})$")]
        public string Plec { get; set; }

        //[Required(ErrorMessage = "Dokument tożsamości jest wymagany.")]
        [Display(Name = "Dokument tożsamości", ShortName = "Dokument tożsamości")]
        public string DokumentTozsamosci { get; set; }

    }



}