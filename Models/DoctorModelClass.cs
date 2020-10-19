using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE;
namespace CloudComputingProject1.Models
{
    public class DoctorModelClass
    {
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string ID { get; set; }
        public string MailAddress { get; set; }       
        public DateTime ExpirationDate { get; set; }
        public string PicturePath { get; set; }


    }

    
}