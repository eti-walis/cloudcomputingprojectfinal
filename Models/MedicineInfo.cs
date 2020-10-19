using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BE;
namespace CloudComputingProject1.Models
{
    public class MedicineInfo
    {
        public int[] medicineArray = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] get()
        {
            return medicineArray;
        }
        public void set(int[] array)
        {
            medicineArray = array;
        }
        public string MedicineName { get; set; }
        public List<Medicine> MedicineList { get; set; }
    }
}