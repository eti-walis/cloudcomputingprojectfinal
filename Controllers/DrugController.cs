using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE;
using BL;
using CloudComputingProject1.Models;
using System.IO;
namespace CloudComputingProject1.Controllers
{

    public class DrugController : Controller
    {
        [HttpPost]
        public ActionResult SaveNewDrug(Medicine drug, HttpPostedFileBase file)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            try
            {
                bl.UplaodFileOnDriveInFolder(file, drug.ID.ToString());
            }
            catch (Exception)
            {
                string ex = "There was a problem with Uplaoding the picture";
                ViewBag.Message = String.Format(ex);
                return View("AddDrug");
            }
            try
            {
                bl.AddMedicine(drug);
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("AddDrug");
            }
            List<DrugModel> lists = new List<DrugModel>();
            List<Medicine> medicinesList = new List<Medicine>();
            try
            {
                medicinesList = bl.getAllMedicines().ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("AddDrug");
            }
            try
            {
                foreach (var item in medicinesList)
                {
                    DrugModel drugModel = new DrugModel();
                    drugModel.ActiveIngredients = item.ActiveIngredients;
                    drugModel.GenericName = item.GenericName;
                    drugModel.MedecienId = item.MedecienId;
                    drugModel.Name = item.Name;
                    drugModel.Producer = item.Producer;
                    drugModel.Properties = item.Properties;
                    //try
                    //{
                    //    drugModel.picturePath = bl.DownloadFileByName(item.ID.ToString());

                    //}
                    //catch (Exception)
                    //{
                    //    string ex = "There is a problem with the picture";
                    //    ViewBag.Message = String.Format(ex);
                    //    return View("AddDrug");
                    //}

                    lists.Add(drugModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("AddDrug", "Drug");
            }
            return View("DrugsList", lists);
        }

        public ActionResult AddDrug()
        {
            return View();
        }
        public ActionResult DrugsList()
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            List<DrugModel> lists = new List<DrugModel>();
            List<Medicine> medicinesList = new List<Medicine>();
            try
            {
                medicinesList = bl.getAllMedicines().ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("LoginAdministratorBtn");
            }
            foreach (var item in medicinesList)
            {
                DrugModel drugModel = new DrugModel();
                drugModel.ActiveIngredients = item.ActiveIngredients;
                drugModel.GenericName = item.GenericName;
                drugModel.MedecienId = item.MedecienId;
                drugModel.Name = item.Name;
                drugModel.Producer = item.Producer;
                drugModel.Properties = item.Properties;
                //try
                //{
                //    drugModel.picturePath = bl.DownloadFileByName(item.ID.ToString());

                //}
                //catch (Exception)
                //{
                //    string ex = "There is a problem with the picture";
                //    ViewBag.Message = String.Format(ex);
                //    return View("LoginAdministratorBtn");
                //}
                lists.Add(drugModel);
            }
            return View("DrugsList", lists);
        }
        public ActionResult EditDrug(string id)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            Medicine medBE = new Medicine();
            medBE = bl.GetMedicine(id);
            DrugModel editmed = new DrugModel();
            editmed.Name = medBE.Name;
            editmed.Producer = medBE.Producer;
            editmed.GenericName = medBE.GenericName;
            editmed.ActiveIngredients = medBE.ActiveIngredients;
            editmed.MedecienId = medBE.MedecienId;
            editmed.Properties = medBE.Properties;
            //try
            //{
            //    editmed.picturePath = bl.DownloadFileByName(medBE.MedecienId.ToString());
            //}
            //catch (Exception)
            //{
            //    string ex = "There was a problem with the picture";
            //    ViewBag.Message = String.Format(ex);
            //}
            return View(editmed);
        }
        public ActionResult DeleteDrug(string id)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            try
            {
                bl.deleteMediciner(id);
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
            }
            List<DrugModel> lists = new List<DrugModel>();
            List<Medicine> medicinesList = bl.getAllMedicines().ToList();
            try
            {
                medicinesList = bl.getAllMedicines().ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("LoginAdministratorBtn");
            }
            foreach (var item in medicinesList)
            {
                DrugModel drugModel = new DrugModel();
                drugModel.ActiveIngredients = item.ActiveIngredients;
                drugModel.GenericName = item.GenericName;
                drugModel.MedecienId = item.MedecienId;
                drugModel.Name = item.Name;
                drugModel.Producer = item.Producer;
                drugModel.Properties = item.Properties;
                //try
                //{
                //    drugModel.picturePath = bl.DownloadFileByName(item.ID.ToString());

                //}
                //catch (Exception)
                //{
                //    string ex = "There is a problem with the picture";
                //    ViewBag.Message = String.Format(ex);
                //    return View("LoginAdministratorBtn");
                //}
                lists.Add(drugModel);
            }
            return View("DrugsList", lists);
        }
        public ActionResult DetailsDrug(string id)
        {

            BL.ImplementBL bl = new BL.ImplementBL();
            Medicine item = bl.GetMedicine(id);
            DrugModel drugModel = new DrugModel();
            drugModel.ActiveIngredients = item.ActiveIngredients;
            drugModel.GenericName = item.GenericName;
            drugModel.MedecienId = item.MedecienId;
            drugModel.Name = item.Name;
            drugModel.Producer = item.Producer;
            drugModel.Properties = item.Properties;
            //try
            //{
            //    drugModel.picturePath = bl.DownloadFileByName(item.ID.ToString());

            //}
            //catch (Exception)
            //{
            //    string ex = "There is a problem with the picture";
            //    ViewBag.Message = String.Format(ex);
            //    return View("LoginAdministratorBtn");
            //}
            return View(drugModel);
        }
        public ActionResult SaveDrug(Medicine m, HttpPostedFileBase file)
        {
            BL.ImplementBL bl = new BL.ImplementBL();
            if (file != null)
            {
                //try
                //{
                //    bl.UplaodFileOnDriveInFolder(file, m.ID.ToString());
                //}
                //catch (Exception)
                //{
                //    string ex = "There was a problem with Uplaoding the picture";
                //    ViewBag.Message = String.Format(ex);
                //    return View("EditDrug", m);
                //}
            }
            try
            {
                bl.UpdateMedicine(m);
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("EditDrug", m);
            }
            List<DrugModel> lists = new List<DrugModel>();
            List<Medicine> medicinesList = new List<Medicine>();
            try
            {
                medicinesList = bl.getAllMedicines().ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = String.Format(ex.Message);
                return View("LoginAdministratorBtn");
            }
            foreach (var item in medicinesList)
            {
                DrugModel drugModel = new DrugModel();
                drugModel.ActiveIngredients = item.ActiveIngredients;
                drugModel.GenericName = item.GenericName;
                drugModel.MedecienId = item.MedecienId;
                drugModel.Name = item.Name;
                drugModel.Producer = item.Producer;
                drugModel.Properties = item.Properties;
                //try
                //{
                //    drugModel.picturePath = bl.DownloadFileByName(item.ID.ToString());

                //}
                //catch (Exception)
                //{
                //    string ex = "There is a problem with the picture";
                //    ViewBag.Message = String.Format(ex);
                //    return View("LoginAdministratorBtn");
                //}
                lists.Add(drugModel);
            }
            return View("DrugsList", lists);
        }
    }
}
