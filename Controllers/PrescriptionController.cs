using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using BL;
using CloudComputingProject1.Models;

namespace CloudComputingProject1.Controllers
{
    public class PrescriptionController : Controller
    {
        public BL.ImplementBL bl = new BL.ImplementBL();
        public ActionResult PrescriptionsList()
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            List<Prescription> PrescriptionsList = new List<Prescription>();
            try
            {
                PrescriptionsList = bl.getAllPrescriptions().ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("LoginAdministratorBtn", "Home");
            }
            return View("PrescriptionsList", PrescriptionsList);
        }
        public ActionResult PrescriptionsById(int Id)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            List<Prescription> PrescriptionList = new List<Prescription>();
            try
            {
                PrescriptionList = bl.PatientPrescriptions(Id.ToString()).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("Index", "Home");
            }
            PrescriptionAndListOfPrescriptions p = new PrescriptionAndListOfPrescriptions();
            p.prescriptionsList = PrescriptionList;
            return View("AddPrescription", p);
        }
        public ActionResult CreatePrescription(string ReferringDoctorId, DateTime endData, DateTime startData, float Amount, string patientId, string medicineName)
        {
            PrescriptionAndListOfPrescriptions pr = new PrescriptionAndListOfPrescriptions();
            Prescription p = new Prescription { amount = Amount, PatientId = patientId, EndData = endData, MedicineName = medicineName, ReferringDoctorId = ReferringDoctorId, StartData = startData };
            BL.ImplementBL bl = new BL.ImplementBL();
            try
            {
                bl.AddPrescription(p);
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("AddPrescription", pr);

            }
            ViewBag.Message = String.Format("The prescription was created successfully");
            List<Prescription> PrescriptionsList = new List<Prescription>();
            try
            {
                PrescriptionsList = bl.PatientPrescriptions(patientId.ToString()).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("Index", "Home");
            }
            pr.prescriptionsList = PrescriptionsList;
            return View("AddPrescription", pr);
        }
    }
}
