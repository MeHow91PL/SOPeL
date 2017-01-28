using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOPeL.Infrastructure
{
    public class Enums
    {
        public enum Aktywny
        {
            Tak,
            Nie
        }

        public enum Status
        {
            Wykonany,
            Rezerwacja
        }
    }

    public static class Ścieżki
    {
        /// <summary>
        /// Ścieżka do widoku podpowiadającego jaki status ewuś oznacza konkretna ikona
        /// </summary>
        public static string InfoStatusEWus { get; } = "~/Views/Podpowiedzi/InfoStatusEWus.cshtml";

        /// <summary>
        /// Ścieżka do widoku podpowiadającego jaki status ewuś oznacza konkretna ikona
        /// </summary>
        public static string InfoKartaRezerwacji { get; } = "~/Views/Podpowiedzi/InfoKartaRezerwacji.cshtml";
    }
}