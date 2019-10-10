using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_Tutorial.Models;
using WebMatrix.WebData;
using System.Web.Caching;
using System.Runtime.Caching;
using MVC_Tutorial.AzureAccess;
using MVC_Tutorial.App_Start;
using System.Data.Objects;
using System.Data;

namespace MVC_Tutorial.Controllers
{
    public class HomeController : Controller
    {


        string msg = "";
        MVC_Tutorial.Models.MVCTutorialContext db = new MVCTutorialContext();
        public ActionResult Index()
        {

            

            db.Configuration.LazyLoadingEnabled = true;

            var ac = (from artcile in db.Articles where artcile.ID > 0 select artcile).FirstOrDefault();

            foreach (var item in ac.Description)
            {
                Trace.WriteLine(item.ToString());
                Trace.Flush();
            }

            
            //if (HttpContext.Request.Cookies.Count > 1)
            //{
            //    Trace.WriteLine(DateTime.Now);
            //    // Trace.WriteLine("UserName:{0}",FormsAuthentication.Decrypt(HttpContext.Request.Cookies[1].Value).UserData);
            //    //  Trace.WriteLine("Name:{0}", FormsAuthentication.Decrypt(HttpContext.Request.Cookies[1].Value).Name);
            //    Trace.Flush();
            //}

            return View();

        }

        [HttpPost]
        public ActionResult Index(Article article)
        {

            if (ModelState.IsValid)
            {
                IDbTransaction dbTransaction = (IDbTransaction)db.Database.BeginTransaction();
               
                try
                {
                    db.Articles.Add(article);
                    db.SaveChanges();
                    dbTransaction.Commit();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    dbTransaction.Rollback();
                }
               
               
            }
            return View(article);
        }

        public ActionResult CreateUser()
        {
            if (HttpContext.Request.Cookies.Count < 1)
            {

                FormsAuthenticationTicket authticket = new FormsAuthenticationTicket(1, "user", DateTime.Now, DateTime.Now.AddDays(90), true, "amitha");
                string encryptedTicket = FormsAuthentication.Encrypt(authticket);
                HttpCookie usercookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(usercookie);
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(string UserName)
        {
            // string UserId = "1";
            // UserName = "Amitha";
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("TutorialDB", "Users", "Id", "UserName", autoCreateTables: false);
            }

            if (!WebSecurity.UserExists(UserName))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            string suggestedUID = String.Format(CultureInfo.InvariantCulture, "{0} is not available.", UserName);
            for (int i = 1; i < 100; i++)
            {
                string altCandidate = UserName + i.ToString();
                if (!WebSecurity.UserExists(altCandidate))
                {
                    suggestedUID = String.Format(CultureInfo.InvariantCulture,
                    "{0} is not available. Try {1}.", UserName, altCandidate);
                    break;
                }
            }
            return Json(suggestedUID, JsonRequestBehavior.AllowGet);
        }


        public float Checkbalance(float balance)
        {
            Contract.Ensures(balance >= 0.0f);
            // if (balance < 0.0f)
            //    balance = 0.0f;
            return balance;
        }


        public ActionResult Contact()
        {



            object data = null;
            try
            {
                data = Load();
            }
            catch (Exception ex)
            {

                Trace.TraceError("Exception caught" + ex.ToString());
            }
            finally
            {
                Trace.TraceInformation("view rendering");
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult Contact(FormCollection form)
        {
            var name = form["txtName"];
            var age = form["txtAge"];
            return Json(name + age, JsonRequestBehavior.AllowGet);
        }

        public object Load()
        {
            object data = "amitha";
            throw new Exception("custom error");
            // return data;
        }


        //using runtime object cache
        //CanCache("one", 1);
        //CanCache("two", 2);
        //CanCache("three", 3);
        //var bal = Checkbalance(-1);
        public void CanCache(string key, int value)
        {
            // ARRANGE
            ObjectCache cache = MemoryCache.Default;
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(1))
            };
            // ACT
            //  cache.Remove(key);
            cache.Add(key, value, policy);
            int fetchedValue = (int)cache.Get(key);

            if (fetchedValue.Equals(value))
            {
                string msg = "equal";
            }
            // ASSERT
            // Assert.That(fetchedValue, Is.EqualTo(value), "Uh oh!");
            //System.Web.Caching
            Cache cache1 = new Cache();
            cache1["one"] = "ONE";

        }


    }
}