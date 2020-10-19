using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public class IBL
    {
        ///////////////Administrator//////////////
        void UpdateAdministrator(Administrator administrator) { }
        void addAdministrator(Administrator administrator) { }
        void deleteAdministrator(Administrator administrator) { }
        IEnumerable<Administrator> getAllAdministrators() { return null; }

        //////////Doctor//////////////////////
        void UpdateDoctor(Doctor doctor) { }
        void AddDoctor(Doctor doctor) { }
        void deleteDoctor(Doctor doctor) { }
       public  IEnumerable<Doctor> getAllDoctors() { return null; }

        ///////////Medicine////////////////
        void UpdateMedicine(Medicine medicine) { }
        void AddMedicine(Medicine medicine) { }
        void deleteMediciner(Medicine medicine) { }
        public IEnumerable<Medicine> getAllMedicines() { return null; }

        //////////Patient////////////////
        void UpdatePatient(Patient patient) { }
        void AddPatient(Patient patient) { }
        void deletePatient(Patient patient) { }
       public  IEnumerable<Patient> getAllPatients() { return null; }

        /////////////////Prescription//////////
        void AddPrescription(Prescription prescription) {  }
        IEnumerable<Prescription> getAllPrescriptions() { return null; }
    }
}
