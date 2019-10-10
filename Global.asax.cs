using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using MVC_Tutorial.App_Start;
using MVC_Tutorial.Controllers;
using System.Web.WebPages;
using System.Diagnostics;

namespace MVC_Tutorial
{
    public class Global : HttpApplication
    {

        private int Exceptioncount = 0;
        void Application_Start(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.FirstChanceException += (sender1, args) =>
            {
                Trace.TraceWarning("Exceptions:{0}", ++Exceptioncount);
            };


            // Code that runs on application startup
            //  var x = 0;
            //var i=  (1 % x); //Exception settings are set in CLR ,so it with throw error
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

         
            //DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("ieModbile")
            //{
            //    ContextCondition = (context => context.GetOverriddenUserAgent().IndexOf("iemobile", StringComparison.OrdinalIgnoreCase) >= 0)
            //}) ;
        }


        void Application_Error(object sender, EventArgs e)
        {
            //if (Server != null)
            //{
            //    //Get the context
            //    HttpContext appContext = ((MVC_Tutorial.Global)sender).Context;
            //    Exception ex = Server.GetLastError().GetBaseException();

            //    Response.Clear();
            //    HttpException exception = new HttpException();
            //    //Log the error using the logging framework

            //    //  Logger.Error(ex);
            //    string action = "";
            //    if (exception != null)
            //    {


            //        switch (exception.GetHttpCode())
            //        {
            //            case 404:
            //                // page not found
            //                action = "HttpError404";
            //                break;
            //            //   case 500:
            //            // server error
            //            //    action = "HttpError500";
            //            //   break;
            //            default:
            //                action = "General";
            //                break;
            //        }
            //    }
            //    //Clear the last error on the server so that custom errors are not fired
            //    Server.ClearError();
            //    //forward the user to the error manager controller.
            //    IController errorController = new ErrorManageController();
            //    RouteData routeData = new RouteData();
            //    routeData.Values["controller"] = "ErrorManagerController";
            //    routeData.Values["action"] = action;
            //    errorController.Execute(
            //    new RequestContext(new HttpContextWrapper(appContext), routeData));
            //}
        }
    }
}