using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winform_Grab
{
    class DistanceResponse
    {
        public class Distance
        {
            public string Text { get; set; }
            public int Value { get; set; } // meters
        }
        public class Duration
        {
            public string Text { get; set; }
            public int Value { get; set; } // seconds
        }

        public class Element
        {
            public Distance Distance { get; set; }
            public Duration Duration { get; set; }
            public string Status { get; set; }
        }

        public class Row
        {
            public Element[] Elements { get; set; }
        }

        public string[] OriginAddresses { get; set; }
        public string[] DestinationAddresses { get; set; }
        public Row[] Rows { get; set; }
        public string Status { get; set; }

        public bool IsValid()
        {
            return Status == "OK" && Rows?.Length > 0 && Rows[0].Elements?.Length > 0 && Rows[0].Elements[0].Status == "OK";
        }
    }
}
