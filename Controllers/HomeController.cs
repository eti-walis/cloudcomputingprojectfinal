using System;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using BE;

namespace CloudComputingProject1.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            BL.ImplementBL bL = new BL.ImplementBL();

            
            bL.ImportDataFromExcel();
            //Prescription p = new Prescription { amount = 0, StartData = new DateTime(2010, 01, 01), EndData = new DateTime(2010, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            // p = new Prescription { amount = 0, StartData = new DateTime(2010, 01, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            // p = new Prescription { amount = 0, StartData = new DateTime(2010, 01, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            // p = new Prescription { amount = 0, StartData = new DateTime(2010, 01, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            // p = new Prescription { amount = 0, StartData = new DateTime(2010, 01, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);

            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 04, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 05, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 05, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 05, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 01, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 05, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 05, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 05, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Sterile Diluent", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);


            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 04, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Amyvid‏", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 05, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Amyvid‏", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 05, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Amyvid‏", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 05, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Amyvid‏", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 01, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Amyvid‏", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 05, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Amyvid‏", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 12, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Amyvid‏", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);
            //p = new Prescription { amount = 0, StartData = new DateTime(2010, 12, 01), EndData = new DateTime(2011, 02, 01), MedicineName = "Amyvid‏", ReferringDoctorId = "1", PatientId = "1" };
            //bL.AddPrescription(p);

            return View();
        }

        public ActionResult SignInDoctor()
        {
            return View("SignInDoctor", "Doctor");
        }
        public ActionResult LoginAdministrator()
        {
            return View();
        }

        public ActionResult LoginAdministratorBtn(int Password)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            try
            {
                bl.isAdministrator(Password.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("LoginAdministrator");

            }
            return View();
        }

    }
}






