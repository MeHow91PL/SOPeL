using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOPeL.ViewModels
{
    public class WizytaViewModel
    {
        public ICollection<Pracownik> pracownicy { get; set; }
        public ICollection<Rezerwacja> rezerwacje { get; set; }
        public ICollection<Wizyta> wizyty { get; set; }
        public ICollection<Rezerwacja> rezerwacjeDzisiejsze { get; set; }
    }

    public class WydrukWywiaduViewModel
    {
        public Pacjent Pacjent { get; set; }
        public string Wywiad { get; set; }
    }
}