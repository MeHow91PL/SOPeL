using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace SOPeL.Models
{
    public class Uzytkownik
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Login nie może być pusty")]
        public string Login { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Hasło nie może być puste")]
        public string Haslo { get; set; }
    }
}