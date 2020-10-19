using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudComputingProject1.Models
{
    public class DrugModel
    {
        public string Name { get; set; }
        public string Producer { get; set; }
        public string GenericName { get; set; }
        public string ActiveIngredients { get; set; }
       // public int ID { get; set; }
        public string MedecienId { get; set; }
        public string Ndc { get; set; }
        public string Properties { get; set; }
        public string picturePath { get; set; }

    }
}