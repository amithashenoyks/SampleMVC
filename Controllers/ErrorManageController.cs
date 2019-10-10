using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial.Controllers
{
    public class ErrorManageController : Controller
    {
        // GET: ErrorManage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HttpError404()
        {
            return RedirectToAction("Index", "Home");
        }

        public JsonResult HttpError500()
        {
            return Json("not found", JsonRequestBehavior.AllowGet);
        }
    }
}