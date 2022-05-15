using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Hosting;
using System.IO;

namespace EzDeliveryAPI
{
    public class EzDeliveryExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            //var filePath = HttpContext.Current.Server.MapPath("~/Files/log.txt");
            //var txt = DateTime.Now.ToString() + " : " + actionExecutedContext.Exception.Message + "\n";
            //System.IO.File.AppendAllText(filePath, txt);

            try
            {
                if (actionExecutedContext.Exception.GetType() == typeof(SqlException))
                {
                    actionExecutedContext.ActionContext.ModelState.AddModelError("", "Sql Server service is not available");
                    actionExecutedContext.Response = actionExecutedContext.Request
                            .CreateErrorResponse(HttpStatusCode.BadGateway, actionExecutedContext.ActionContext.ModelState);

                    ExeceptionHandle(actionExecutedContext.Exception);

                }
                else if (actionExecutedContext.Exception.GetType() == typeof(System.Net.Mail.SmtpException))
                {
                    actionExecutedContext.ActionContext.ModelState.AddModelError("", "Unable to send Email.");
                    actionExecutedContext.Response = actionExecutedContext.Request
                            .CreateErrorResponse(HttpStatusCode.GatewayTimeout, actionExecutedContext.ActionContext.ModelState);
                    ExeceptionHandle(actionExecutedContext.Exception);
                }
                else
                {
                    actionExecutedContext.Response =
                        actionExecutedContext.Request
                        .CreateErrorResponse(HttpStatusCode.InternalServerError, actionExecutedContext.Exception);
                    ExeceptionHandle(actionExecutedContext.Exception);
                }
            }
            catch(Exception ex)
            {
                ExeceptionHandle(ex);
            }
        }

        public void ExeceptionHandle(Exception ex)
        {
            //MemoryStream ms = new MemoryStream(photobyte, 0, photobyte.Length);
            //Image image = Image.FromStream(ms, true);

            string startupPath = System.IO.Path.GetFullPath(@"..\..\");

            string h = Environment.CurrentDirectory;

            //image.Save(HostingEnvironment.MapPath("~" + "\\ErrorLog.txt"));

            string FileFath = HostingEnvironment.MapPath("~" + "/ErrorLog.txt");

            if (!File.Exists(FileFath))
            {
                File.Create(FileFath);
            }
            string errorLogPath = @FileFath;
            File.AppendAllText(errorLogPath, Environment.NewLine + Environment.NewLine + Environment.NewLine + DateTime.Now + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "StackTrace: " + ex.StackTrace + Environment.NewLine + ex.InnerException);
        }
    }


}