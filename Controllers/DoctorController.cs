using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudComputingProject1.Models;
using BE;

namespace CloudComputingProject1.Controllers
{
    public class DoctorController : Controller
    {

        public ActionResult LogInDoctor()
        {
            return View();
        }

        public ActionResult LogInDoctorBtn(int Id)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            try
            {
                bl.IsExistDoctor(Id.ToString());

            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("LogInDoctor");

            }
            return View("DoctorEnterPatientsId");
        }


        public ActionResult DoctorsList()
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            List<Doctor> doctorsLis = new List<Doctor>();
            List<DoctorModelClass> lists = new List<DoctorModelClass>();
            try
            {
                doctorsLis = bl.getAllDoctors().ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("LoginAdministratorBtn", "Home");
            }

            foreach (var item in doctorsLis)
            {
                DoctorModelClass d = new DoctorModelClass();
                d.ID = item.ID;
                d.MailAddress = item.MailAddress;
                d.Name = item.Name;
                d.Specialization = item.Specialization;
                d.ExpirationDate = item.ExpirationDate;
                //try
                //{
                //    d.PicturePath = bl.DownloadFileByName(item.ID.ToString());

                //}
                //catch (Exception)
                //{
                    d.PicturePath = "/img/DefaultImageForDoctor.png";
                //}

                lists.Add(d);
            }
            return View("DoctorsList", lists);
        }

        [HttpPost]
        public ActionResult SaveNewDoctor(Doctor doctorBE, HttpPostedFileBase filename)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            //Default Image For Doctor
            if (filename != null)
            {
                try
                {
                    bl.UplaodFileOnDriveInFolder(filename, doctorBE.ID.ToString());

                }
                catch (Exception)
                {
                    string ex = "There was a problem with Uplaoding the picture";
                    ViewBag.Message = String.Format(ex);
                    return View("AddDoctor");
                }
            }
            try
            {
                bl.AddDoctor(doctorBE);
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("AddDoctor");
            }

            List<DoctorModelClass> lists = new List<DoctorModelClass>();
            foreach (var item in bl.getAllDoctors())
            {
                DoctorModelClass d = new DoctorModelClass();
                d.ID = item.ID;
                d.MailAddress = item.MailAddress;
                d.Name = item.Name;
                d.Specialization = item.Specialization;
                d.ExpirationDate = item.ExpirationDate;
                try
                {
                    d.PicturePath = bl.DownloadFileByName(item.ID.ToString());

                }
                catch (Exception)
                {
                    d.PicturePath = "/img/DefaultImageForDoctor.png";
                }

                lists.Add(d);
            }
            return View("DoctorsList", lists);
        }


        public ActionResult AddDoctor()
        {
            return View();
        }
        public ActionResult EditDoctor(int id)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            Doctor docBE = new Doctor();
            docBE = bl.getDoctor(id.ToString());
            DoctorModelClass editdoc = new DoctorModelClass();
            editdoc.ID = docBE.ID;
            editdoc.MailAddress = docBE.MailAddress;
            editdoc.Name = docBE.Name;
            editdoc.Specialization = docBE.Specialization;
            editdoc.ExpirationDate = docBE.ExpirationDate;
            try
            {
                editdoc.PicturePath = bl.DownloadFileByName(docBE.ID.ToString());

            }
            catch (Exception)
            {
                editdoc.PicturePath = "/img/DefaultImageForDoctor.png";
            }

            return View(editdoc);
        }
        public ActionResult DeleteDoctor(int id)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            try
            {
                bl.deleteDoctor(id.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);

            }
            List<Doctor> doctorsLis = new List<Doctor>();
            List<DoctorModelClass> lists = new List<DoctorModelClass>();
            try
            {
                doctorsLis = bl.getAllDoctors().ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("LoginAdministratorBtn", "Home");
            }

            foreach (var item in doctorsLis)
            {
                DoctorModelClass d = new DoctorModelClass();
                d.ID = item.ID;
                d.MailAddress = item.MailAddress;
                d.Name = item.Name;
                d.Specialization = item.Specialization;
                d.ExpirationDate = item.ExpirationDate;
                try
                {
                    d.PicturePath = bl.DownloadFileByName(item.ID.ToString());

                }
                catch (Exception)
                {
                    d.PicturePath = "/img/DefaultImageForDoctor.png";
                }

                lists.Add(d);
            }
            return View("DoctorsList", lists);
        }
        public ActionResult DetailsDoctor(int id)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            Doctor docBE = bl.getDoctor(id.ToString());
            DoctorModelClass d = new DoctorModelClass();
            d.ID = docBE.ID;
            d.MailAddress = docBE.MailAddress;
            d.Name = docBE.Name;
            d.Specialization = docBE.Specialization;
            d.ExpirationDate = docBE.ExpirationDate;

            try
            {
                d.PicturePath = bl.DownloadFileByName(docBE.ID.ToString());

            }
            catch (Exception)
            {
                d.PicturePath = "/img/DefaultImageForDoctor.png";
            }
            return View(d);
        }
        [HttpPost]
        public ActionResult SaveDoctor(Doctor d, HttpPostedFileBase filename)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            if (filename != null)
            {
                try
                {
                    bl.UplaodFileOnDriveInFolder(filename, d.ID.ToString(), "רופאים");

                }
                catch (Exception)
                {
                    string ex = "There was a problem with Uplaoding the picture";
                    ViewBag.Message = String.Format(ex);
                    return View("EditDoctor", d);
                }
            }
            try
            {
                bl.UpdateDoctor(d);
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("EditDoctor", d);
            }
            List<Doctor> doctorsLis = new List<Doctor>();
            List<DoctorModelClass> lists = new List<DoctorModelClass>();
            try
            {
                doctorsLis = bl.getAllDoctors().ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("Index", "Home");
            }

            foreach (var item in doctorsLis)
            {
                DoctorModelClass doc = new DoctorModelClass();
                doc.ID = item.ID;
                doc.MailAddress = item.MailAddress;
                doc.Name = item.Name;
                doc.Specialization = item.Specialization;
                doc.ExpirationDate = item.ExpirationDate;
                //try
                //{
                //    doc.PicturePath = bl.DownloadFileByName(item.ID.ToString());

                //}
                //catch (Exception)
                //{
                    doc.PicturePath = "/img/DefaultImageForDoctor.png";
                //}

                lists.Add(doc);
            }
            return View("DoctorsList", lists);
        }

    }
}
