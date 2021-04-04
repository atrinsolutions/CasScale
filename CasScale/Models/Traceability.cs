using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CL_WorkApp.Models.Entities
{
    public class Traceability
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Descripion { get; set; }
        public int Born_In_Country { get; set; }
        public int Bred_Country { get; set; }
        public int Slaughter_House { get; set; }
        public int Cutting_Hal { get; set; }
    }
}
