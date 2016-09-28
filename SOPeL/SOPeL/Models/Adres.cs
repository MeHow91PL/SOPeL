using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOPeL.Models
{
    public class Adres
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [RegularExpression(@"([0-9]{2}-[0-9]{3})$")]
        public string KodPocztowy { get; set; }

        [MaxLength(60)]
        public string Miasto { get; set; }

        [MaxLength(60)]
        public string Ulica { get; set; }

        [MaxLength(10)]
        public string NrDomu { get; set; }

        [MaxLength(10)]
        public string NrLokalu { get; set; }
    }
}