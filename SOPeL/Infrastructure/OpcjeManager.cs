using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOPeL.Infrastructure
{
    public class OpcjeManager 
    {
        public static class Terminarz
        {
            public static string godzOd { get; set; }
            public static string godzDo { get; set; }
            public static string czasTrwaniaWizyty { get; set; }
            public static GrafikDlaPrzychodni grafikDlaPrzychodni { get; set; }
            public static string znakPodzialuImieniaINazwiska { get; set; }



            public enum GrafikDlaPrzychodni
            {
                Tak,
                Nie
            }
        }

        public static class Ogólne
        {
            public class ZnakPodziałuImieniaINazwiska
            {
                public static readonly string nazwa = "ogol_podz_imie_nazw";
            }

        }
    }
}