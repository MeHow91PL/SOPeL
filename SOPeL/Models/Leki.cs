using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOPeL.Models
{
    [Table("Leki")]
    public class Leki
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Dawka { get; set; }
        public string Opakowanie { get; set; }

        public string Dawkowanie { get; set; }
        public string Odplatnosc { get; set; }
    }
}