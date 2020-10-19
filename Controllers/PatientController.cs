using CloudComputingProject1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using BL;
namespace CloudComputingProject1.Controllers
{
    public class PatientController : Controller
    {
        public ActionResult PatientsList()
        {
            ImplementBL bl = new ImplementBL();
            List<Patient> patiensList = new List<Patient>();
            try
            {
                patiensList = bl.getAllPatients().ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("LoginAdministratorBtn");
            }
            return View(patiensList);
        }
        public ActionResult CreatePatient()
        {
            return View();
        }

        public ActionResult AddNewPatient(Patient p)
        {
            ImplementBL bl = new ImplementBL();
            try
            {
                bl.AddPatient(p);
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
            }
            List<Patient> patiensList = bl.getAllPatients().ToList();
            return View("PatientsList", patiensList);
        }

        public ActionResult SavePatient(Patient p)
        {
            ImplementBL bl = new ImplementBL();
            try
            {
                bl.UpdatePatient(p);
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("EditPatient", p);
            }
            List<Patient> patiensList = bl.getAllPatients().ToList();
            return View("PatientsList", patiensList);
        }
        public ActionResult DeletePatient(int id)
        {
            ImplementBL bl = new ImplementBL();
            try
            {
                bl.deletePatient(id.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
            }
            List<Patient> patiensList = bl.getAllPatients().ToList();
            return View("PatientsList", patiensList);
        }

        public ActionResult EditPatient(int id)
        {
            ImplementBL bl = new ImplementBL();
            Patient p = bl.GetPatient(id.ToString());
            return View(p);
        }
        public ActionResult DetailsPatient(int id)
        {
            ImplementBL bl = new ImplementBL();
            Patient p = bl.GetPatient(id.ToString());
            return View(p);
        }
    }

}
