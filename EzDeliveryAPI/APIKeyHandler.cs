using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace EzDeliveryAPI
{
    public class APIKeyHandler: DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Headers.Contains("Access-Control-Request-Headers"))
                {
                    return base.SendAsync(request, cancellationToken);
                }
                else if (request.Headers.Contains("APICODE"))
                {
                    var apiKey = request.Headers.GetValues("APICODE").FirstOrDefault();
                    if (apiKey == "123456789")
                    {
                        return base.SendAsync(request, cancellationToken);
                    }
                }

                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var taskObj = new TaskCompletionSource<HttpResponseMessage>();
                taskObj.SetResult(response);
                return taskObj.Task;
            }
            catch(Exception ex)
            {
                ExeceptionHandle(ex);
            }
            return null;
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