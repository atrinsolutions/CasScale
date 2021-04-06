using System;
using System.Collections.Generic;
using System.Linq;

namespace CL_WorkApp.Models.Entities
{
    public class Tax_Rate
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int Type { get; set; }
        public float Tax_Rate_Percent { get; set; }
    }
}
