using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Tutorial.App_Start
{
    public class FilterConfig
    {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute() {
                ExceptionType = typeof(NullReferenceException),
                View = "Index"
            }); 
           // filters.Add(new RequireHttpsAttribute());
        }

    }
}