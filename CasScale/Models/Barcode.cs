using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CL_WorkApp.Models.Entities
{
    public class Barcode
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int Type { get; set; }
        public int Format_No { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
    }
}
