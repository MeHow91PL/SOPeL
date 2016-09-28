using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SOPeL.Models
{
    public class Pracownik : Osoba
    {
        [MaxLength(100)]
        public string Specjalizacja { get; set; }

        [MaxLength(8)]
        public string PWZ { get; set; }

        [MaxLength(15)]
        public string TytulNaukowy { get; set; }
    }
}