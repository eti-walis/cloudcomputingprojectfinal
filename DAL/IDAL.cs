using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    class IDAL
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
        IEnumerable<Doctor> getAllDoctors() { return null; }

        ///////////Medicine////////////////
        void UpdateMedicine(Medicine medicine) { }
        void AddMedicine(Medicine medicine) { }
        void deleteMediciner(Medicine medicine) { }
        IEnumerable<Medicine> getAllMedicines() { return null; }

        //////////Patient////////////////
        void UpdatePatient(Patient patient) { }
        void AddPatient(Patient patient) { }
        void deletePatient(Patient patient) { }
        IEnumerable<Patient> getAllPatients() { return null; }

        /////////////////Prescription//////////
        void AddPrescription(Prescription prescription) { }
        IEnumerable<Prescription> getAllPrescriptions() { return null; }
    }
}
