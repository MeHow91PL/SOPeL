using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOPeL.Infrastructure
{
    public class BrakPesel : Exception
    {
        public BrakPesel(string Konumikat): base (Konumikat)
        {

        }
    }
    public class BrakImienia : Exception
    {
        public BrakImienia(string Konumikat) : base(Konumikat)
        {

        }
    }
    public class BrakNazwiska : Exception
    {
        public BrakNazwiska(string Konumikat) : base(Konumikat)
        {

        }
    }
}