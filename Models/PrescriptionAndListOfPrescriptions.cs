using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE;


namespace CloudComputingProject1.Models
{
    public class PrescriptionAndListOfPrescriptions
    {
        public Prescription prescription { set; get; }
        public List<Prescription> prescriptionsList { set; get; }

        public PrescriptionAndListOfPrescriptions()
        {
            prescriptionsList = new List<Prescription>();
        }
      
        public List<Prescription> GetAllPrescriptions()
        {
            return this.prescriptionsList;
        }
    }

}
