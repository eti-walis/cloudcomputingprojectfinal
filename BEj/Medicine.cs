using System;

namespace BE
{
    public class Medicine
    {
        public string Name { get; set; }
        public string Producer { get; set; }
        public string GenericName { get; set; }
        public string ActiveIngredients { get; set; }
        public int ID { get; set; }
        public string MedecienId { get; set; }
        //public string Ndc { get; set; }
        public string Properties { get; set; }
        public string PicturePath { get; set; }

    }
}

