using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Patient
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Idkey { get; set; }
        public string ID { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }
        
        public override string ToString()
        {
            return $"id:{ID} firstname:{FirstName} LastName{LastName}";
        }
    }
}
