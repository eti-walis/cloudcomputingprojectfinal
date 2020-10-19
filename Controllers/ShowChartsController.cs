using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using BE;
using CloudComputingProject1.Models;
namespace CloudComputingProject1.Controllers
{
    public class ShowChartsController : Controller
    {

        // GET: ShowCharts
        public ActionResult Index(int id, int year)
        {
            ImplementBL bl = new ImplementBL();
            string MedicineName = bl.GetMedicine(id.ToString()).Name.ToString();
            List<int> prescription = bl.info(MedicineName,year);
            MedicineInfo md = new MedicineInfo();
            md.MedicineName = MedicineName;
            md.medicineArray = prescription.ToArray();
            return View(md);

        }
        public ActionResult View()
        {
            ImplementBL bl = new ImplementBL();
            List < Medicine >md= bl.getAllMedicines().ToList();
            return View(md);

        }

        // GET: ShowCharts/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShowCharts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShowCharts/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShowCharts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShowCharts/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShowCharts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShowCharts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }



}
