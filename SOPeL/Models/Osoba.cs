using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOPeL.Models
{
    public abstract class Osoba
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Imię nie może być puste")]
        public string Imie { get; set; }

        [MaxLength(60)]
        [Required(ErrorMessage = "Nazwisko nie może być puste")]
        public string Nazwisko { get; set; }

        [RegularExpression(@"([0-9]{11})$", ErrorMessage = "Podany PESEL jest błędny")]
        public string Pesel { get; set; }

        [Phone(ErrorMessage = "Numer telefonu jest niepoprawny")]
        [Required]
        [MaxLength(9)]
        [RegularExpression(@"([0-9]{9})$", ErrorMessage = "Podany numer jest błędny")]
        public string Telefon { get; set; }

        [EmailAddress]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}