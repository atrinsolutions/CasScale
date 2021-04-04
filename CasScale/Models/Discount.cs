using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CL_WorkApp.Models.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public int Department_No { get; set; }
        public int PLU_No { get; set; }
        public string Name { get; set; }
        public int PLU_Type { get; set; }
        public int Discount_Type { get; set; }
        public int Onest_Target { get; set; }
        public int Onest_Value { get; set; }
        public int Thesecond_Target { get; set; }
        public int Thesecond_Value { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }

    }
}
