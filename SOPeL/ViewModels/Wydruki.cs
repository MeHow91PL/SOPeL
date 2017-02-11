using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOPeL.ViewModels
{
    public class WydrukRecepty
    {
        public Pacjent Pacjent { get; set; }
        public Pracownik Pracownik { get; set; }
        public string[] Leki { get; set; }
    }
}