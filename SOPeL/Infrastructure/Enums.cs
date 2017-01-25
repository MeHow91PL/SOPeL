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
}