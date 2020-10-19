
using System;
using System.Collections.Generic;
using System.Text;



namespace BE
{
    public class Doctor
    {
        public string Name { get; set; }
        public string Specialization { get; set; }
        public int Idkey { get; set; }
        public string ID { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string MailAddress { get; set; }

        public string PicturePath { get; set; }

    }
}

