using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Prescription
    {
        public int Id { get; set; }
        //public string ID { get; set; }
        public string MedicineName { get; set; }
        public string PatientId { get; set; }
        public float amount { get; set; }
        //public int period { get; set; }
        public DateTime StartData { get; set; }
        public DateTime EndData { get; set; }
        public string ReferringDoctorId { get; set; }
    }
}

