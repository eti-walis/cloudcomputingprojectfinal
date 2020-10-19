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
using BLj;
using System.IO;

namespace BL
{
    public class ImplementBL
    {
        
        ///////////////Administrator//////////////
        public void UpdateAdministrator(Administrator administrator)
        {
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
            dal.deleteAdministrator(id);
        }
        public IEnumerable<Administrator> getAllAdministrators()
        {
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
            return dal.getAllAdministrators();
        }
        public void isAdministrator(string code)
        {
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
            if (code != "12345")
            {
                throw new Exception("incorrect code");
            }

        }

        //////////Doctor//////////////////////
        public void UpdateDoctor(Doctor doctor)
        {
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
            return dal.getAllDoctors();
        }
        public void IsExistDoctor(string id)
        {
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
            try
            {
                dal.UpdateMedicine(medicine);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string IdByName(string name)
        {
            ImplementDAL dal = new ImplementDAL();
            string id = (from item in dal.getAllMedicines()
                         where item.Name == name
                         select item.MedecienId).FirstOrDefault();
            if (id != null)
                return id;
            else throw new Exception("medicine doesn't exist");
        }
            public void AddMedicine(Medicine medicine)
        {
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
            return dal.getAllPrescriptions().Where(item => item.PatientId == id);


        }
        public IEnumerable<Patient> getAllPatients()
        {
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
            return dal.getAllPatients();
        }

        /////////////////Prescription//////////
        public void AddPrescription(Prescription prescription)
        {
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
            Doctor d = getDoctor(prescription.ReferringDoctorId);
            if (d.ExpirationDate.Date < DateTime.Today.Date)
            {
                throw new Exception("Invalid doctor license");
            }
            if (prescription.EndData <= prescription.StartData)
                throw new Exception("Dates are not correct, Enter again");
            //check if the medicines don't conflict
            //CheckDrugs check = new CheckDrugs();
            //try
            //{

            //    string s =check.checkDrugs(prescription.PatientId, prescription.Id, prescription.StartData, prescription.EndData);
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}

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
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
            return dal.getAllPrescriptions();
        }
        public int MedicinePerPeriod(string medicine, DateTime startDate, DateTime endDate)
        {
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
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
        public void UpdateMedicinePicture(string medicineId , HttpPostedFileBase file)
        {
            DAL.ImplementDAL dal = new DAL.ImplementDAL();
            dal.UpdateMedicinePicture(medicineId, file);
        }

       

        public string DownloadFileByName(string fileName)

        {
            GoogleDriveApi googleDriveDAL = new GoogleDriveApi();
            try
            {
                return Path.Combine("~/GoogleDriveFiles", Path.GetFileName(googleDriveDAL.DownloadGoogleFileByName(fileName))) ;

               //googleDriveDAL.DownloadGoogleFileByName(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UplaodFileOnDrive(HttpPostedFileBase file, string drugId)
        {
            if (validMedicinePicture(file) == true)
            {
                GoogleDriveApi googleDriveDAL = new GoogleDriveApi();
                googleDriveDAL.UplaodFileOnDrive(file, drugId);
            }
            else
                throw new Exception("the picture is not a medicine");

        }

        public void UplaodFileOnDriveInFolder(HttpPostedFileBase file, string folderName)
        {
            if (validMedicinePicture(file) == true)
            {
                GoogleDriveApi googleDriveDAL = new GoogleDriveApi();
                googleDriveDAL.UplaodFileOnDriveInFolder(file, folderName);
            }
            else
                throw new Exception("the picture is not a medicine");
        }
        public void UplaodFileOnDriveInFolder(HttpPostedFileBase file, string newFileName, string folderName)
        {
            if (validMedicinePicture(file) == true)
            {
                GoogleDriveApi googleDriveDAL = new GoogleDriveApi();
                googleDriveDAL.UplaodFileOnDriveInFolder(file, newFileName, folderName);
            }
            else
                throw new Exception("the picture is not a medicine");

        }

        public void CreateFolder(string FolderName)
        {
            GoogleDriveApi googleDriveDAL = new GoogleDriveApi();
            googleDriveDAL.CreateFolder(FolderName);
        }

        public void DeleteFile(string fileId)
        {
            GoogleDriveApi googleDriveDAL = new GoogleDriveApi();
            googleDriveDAL.DeleteFile(fileId);
        }
        private bool validMedicinePicture(HttpPostedFileBase file)
        {
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),Path.GetFileName(file.FileName));
            file.SaveAs(path);
            ImageTagsLogic r = new ImageTagsLogic();
            List<string> tagsPictures = r.GetTags(path);
            foreach (var item in tagsPictures)
            {
                if (item.Contains("medicine") || item.Contains("drug") || item.Contains("pill") || item.Contains("medical"))
               
                    return true;
            }
            return false;
        }

    }
}