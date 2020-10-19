using System;
using System.Web.Mvc;


namespace CloudComputingProject1.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            BL.ImplementBL bL = new BL.ImplementBL();
            //bL.ImportDataFromExcel();
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






