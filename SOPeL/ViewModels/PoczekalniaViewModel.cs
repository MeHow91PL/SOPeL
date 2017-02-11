using SOPeL.Infrastructure;
using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOPeL.ViewModels
{
    public class PoczekalniaViewModel
    {
        public ICollection<Pracownik> pracownicy { get; set; }
        public ICollection<Rezerwacja> rezerwacje { get; set; }
        public FiltryPoczekalni FiltryPoczekalni { get; set; }
    }

    public class FiltryPoczekalni
    {
        public string WybranaData { get; set; }
        public int WybranyLekarz { get; set; }
        public Status StatusRezerwacji { get; set; }
    }
}