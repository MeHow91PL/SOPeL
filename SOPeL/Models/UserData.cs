using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOPeL.Models
{
    public class UserData
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [MaxLength(50)]

        [Display(Name = "Login")]
        [Required(ErrorMessage = "Login nie może być pusty!!")]
        public string Login { get; set; }

        [Display(Name = "Hasło")]
        [MaxLength(100)]
        [Required(ErrorMessage = "Hasło nie może być puste")]
        public string Haslo { get; set; }

        public DateTime? DataUtworzenia { get; set; }

        public int? PracownikID { get; set; }

        public virtual Pracownik Pracownik { get; set; }
    }
}