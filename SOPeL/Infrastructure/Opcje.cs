using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOPeL.Infrastructure
{
    public class Opcje
    {
        public class Terminarz
        {
            public string godzOd { get; set; }
            public string godzDo { get; set; }
            public string czasTrwaniaWizyty { get; set; }

            enum OgolnyDlaPrzychodni
            {
                Tak,
                Nie
            }
        }
    }
}