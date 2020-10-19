using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using BE;
using DAL;
using Microsoft.Office.Interop.Excel;

using _Excel = Microsoft.Office.Interop.Excel;
using OpenQA.Selenium.Remote;
using DALj;
using System.Web;
using BEj;

namespace BL
{
    public class ImplementBL
    {
        DAL.ImplementDAL dal = new DAL.ImplementDAL();
        ///////////////Administrator//////////////
        public void UpdateAdministrator(Administrator administrator)
        {
            try
            {
                dal.UpdateAdministrator(administrator);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void addAdministrator(Administrator administrator)
        {
            try
            {
                dal.addAdministrator(administrator);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void deleteAdministrator(string id)
        {
            dal.deleteAdministrator(id);
        }
        public IEnumerable<Administrator> getAllAdministrators()
        {
            return dal.getAllAdministrators();
        }
        public void isAdministrator(string code)
        {
            if (code != "12345")
            {
                throw new Exception("incorrect code");
            }

        }

        //////////Doctor//////////////////////
        public void UpdateDoctor(Doctor doctor)
        {
            try
            {
                dal.UpdateDoctor(doctor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddDoctor(Doctor doctor)
        {
            try
            {
                dal.AddDoctor(doctor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void deleteDoctor(string id)
        {
            try
            {
                dal.deleteDoctor(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Doctor getDoctor(string id)
        {
            Doctor d = (from item in dal.getAllDoctors().ToList()
                        where item.ID == id
                        select item).FirstOrDefault();
            if (d != null)
                return d;
            else
                throw new Exception("doctor doesn't exist");
        }
        public IEnumerable<Doctor> getAllDoctors()
        {
            return dal.getAllDoctors();
        }
        public void IsExistDoctor(string id)
        {
            try
            {
                dal.IsExistDoctor(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        ///////////Medicine////////////////
        public void UpdateMedicine(Medicine medicine)
        {
            try
            {
                dal.UpdateMedicine(medicine);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void AddMedicine(Medicine medicine)
        {
            try
            {
                dal.AddMedicine(medicine);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void deleteMediciner(string id)
        {
            try
            {
                dal.deleteMediciner(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Medicine GetMedicine(string id)
        {
            Medicine m = (from item in dal.getAllMedicines().ToList()
                          where item.MedecienId == id
                          select item).FirstOrDefault();
            if (m != null)
                return m;
            else
                throw new Exception("medicine doesn't exist");
        }
        public IEnumerable<Medicine> getAllMedicines()
        {
            return dal.getAllMedicines();
        }
        public List<int> info(string MedicineName, int year)
        {
            ImplementDAL dal = new ImplementDAL();
            return dal.info(MedicineName, year);
        }
        //////////Patient////////////////
        public void UpdatePatient(Patient patient)
        {
            try
            {
                dal.UpdatePatient(patient);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddPatient(Patient patient)
        {
            try
            {
                dal.AddPatient(patient);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void deletePatient(string id)
        {
            try
            {
                dal.deletePatient(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public Patient GetPatient(string id)
        {
            Patient p = (from item in dal.getAllPatients().ToList()
                         where item.ID == id
                         select item).FirstOrDefault();
            if (p != null)
                return p;
            else
                throw new Exception("patient doesn't exist");
        }
        public IEnumerable<Prescription> PatientPrescriptions(string id)
        {
            return dal.getAllPrescriptions().Where(item => item.PatientId == id);


        }
        public IEnumerable<Patient> getAllPatients()
        {
            return dal.getAllPatients();
        }

        /////////////////Prescription//////////
        public void AddPrescription(Prescription prescription)
        {
            Doctor d = getDoctor(prescription.ReferringDoctorId);
            if (d.ExpirationDate.Date < DateTime.Today.Date)
            {
                throw new Exception("Invalid doctor license");
            }
            //check if the medicines don't conflict

            //more checking
            try
            {
                dal.AddPrescription(prescription);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Prescription> getAllPrescriptions()
        {
            return dal.getAllPrescriptions();
        }
        public int MedicinePerPeriod(string medicine, DateTime startDate, DateTime endDate)
        {
            int sum = 0;
            foreach (var item in dal.getAllPrescriptions())
            {
                if (item.StartData >= startDate && item.StartData <= endDate && item.MedicineName == medicine)
                    sum += 1;
            }
            return sum;
        }
        //export from excel file
        public void ImportDataFromExcel()//put the  medicines-data from the excel file in the database
        {
            string FilePath = "C:\\Users\\User\\Desktop\\CloudComputingProject1-DB-Shira\\CloudComputingProject1-DB-Shira\\medicine.xlsx";
            _Application excel = new _Excel.Application();
            Workbook wb = excel.Workbooks.Open(FilePath);
            Worksheet ws = wb.Worksheets[1];
            string name = string.Empty, genericName = string.Empty, producer = string.Empty, active = string.Empty, proparties = string.Empty, medicineId = string.Empty;
            for (int i = 2; i < 1001; i++)
            {
                name = Convert.ToString(ws.Cells[1][i].Value2);
                genericName = Convert.ToString(ws.Cells[2][i].Value2);
                producer = Convert.ToString(ws.Cells[3][i].Value2);
                active = Convert.ToString(ws.Cells[4][i].Value2);
                proparties = Convert.ToString(ws.Cells[5][i].Value2);
                medicineId = Convert.ToString(ws.Cells[7][i].Value2);

                using (var ctx = new PharmacyContext())
                {
                    var drug = new Medicine { Properties = proparties, ActiveIngredients = active, GenericName = genericName, Name = name, Producer = producer, MedecienId = medicineId };
                    ctx.Medicines.Add(drug);
                    try
                    {
                        ctx.SaveChanges();
                        // your code for insert here
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting  
                                // the current instance as InnerException  
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }


                }

            }

        }
        GoogleDriveApi googleDriveDAL = new GoogleDriveApi();

        public string DownloadFileByName(string fileName)

        {
            try
            {
                return googleDriveDAL.DownloadGoogleFileByName(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UplaodFileOnDrive(HttpPostedFileBase file, string drugId)
        {
            googleDriveDAL.UplaodFileOnDrive(file, drugId);
        }

        public void UplaodFileOnDriveInFolder(HttpPostedFileBase file, string folderName)
        {
            googleDriveDAL.UplaodFileOnDriveInFolder(file, folderName);
        }
        public void UplaodFileOnDriveInFolder(HttpPostedFileBase file, string newFileName, string folderName)
        {
            googleDriveDAL.UplaodFileOnDriveInFolder(file, newFileName, folderName);
        }

        public void CreateFolder(string FolderName)
        {
            googleDriveDAL.CreateFolder(FolderName);
        }

        public void DeleteFile(string fileId)
        {
            googleDriveDAL.DeleteFile(fileId);
        }
    }
}