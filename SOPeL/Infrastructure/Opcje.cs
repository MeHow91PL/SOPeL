using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOPeL.Infrastructure
{
    public class Opcje
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
    }
}