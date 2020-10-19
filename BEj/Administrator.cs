using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Administrator
    {

        public string UserName { get; set; }
        public string ID { get; set; }
        public int Idkey { get; set; }
        public int Password { get; set; }

        public override string ToString()
        {
            return $"id:{ID} name:{UserName} password:{Password}";
        }

    }
}
