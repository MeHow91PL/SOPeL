using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOPeL.Models
{
    [Table("Opcje")]
    public class Opcja
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public TypOpcji Typ { get; set; }
        public string Wartosc { get; set; }
        public string  Ostatnia_modyfikacja{ get; set; }
    }

    public enum TypOpcji
    {
        Przychodni,
        Pracownika
    }
}