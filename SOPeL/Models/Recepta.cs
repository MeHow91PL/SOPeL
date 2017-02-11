using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOPeL.Models
{
    [Table("Recepty")]
    public class Recepty
    {
        public int Id { get; set; }
        public List<Leki> Leki { get; set; }
        public Wizyta Wizyta { get; set; }
    }
}