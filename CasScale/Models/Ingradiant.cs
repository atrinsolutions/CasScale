using System;
using System.Collections.Generic;
using System.Linq;

namespace CL_WorkApp.Models.Entities
{
    public class Ingradiant
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Descripion { get; set; }
        public string Serving_Size { get; set; }
        public string Serving_Per_Container { get; set; }
        public int Base_Unit { get; set; }
        public int Base_Size { get; set; }
        public int Calories { get; set; }
        public int Calories_From_Fat { get; set; }
        public int Total_Fat { get; set; }
        public int Saturated_Fat { get; set; }
        public int TransFat { get; set; }
        public int Cholesterol { get; set; }
        public int sodium { get; set; }
        public int Total_Carbohydrate { get; set; }
        public int Dietray_Fiber { get; set; }
        public int Sugers { get; set; }
        public int Protein { get; set; }
        public int Vitamin_A_Percent { get; set; }
        public int Calcium_Percent { get; set; }
        public int Vitamin_C_Percent { get; set; }
        public int Iron_Percent { get; set; }
        public int Salt_Percent { get; set; }
        public int Energy { get; set; }

    }
}
