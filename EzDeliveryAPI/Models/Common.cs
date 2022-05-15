using Plivo.API;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;

namespace EzDeliveryAPI.Models
{
    public class Common
    {
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
        public string SaveImage(string image, string Type, string IdentificationNo, string Attachment)
        {
            string Extention = null, filePath = null, imagePath = null;
            try
            {
                string RootPath = HostingEnvironment.MapPath("~");

                string Mainpath = RootPath + "Files";

                if (!Directory.Exists(Mainpath))
                {
                    Directory.CreateDirectory(Mainpath);
                    imagePath = "/Files";
                }
                else
                {
                    imagePath = "/Files";
                }
                if (Type == "Customer")
                {
                    if (!Directory.Exists(RootPath + imagePath + "/Customer"))
                    {
                        Directory.CreateDirectory(RootPath + "Files" + "/Customer");
                        imagePath = imagePath + "/Customer";
                    }
                    else
                    {
                        imagePath = imagePath + "/Customer";
                    }

                    if (!Directory.Exists(RootPath + imagePath + "/" + IdentificationNo))
                    {
                        Directory.CreateDirectory(RootPath + imagePath + "/" + IdentificationNo);
                        imagePath = imagePath + "/" + IdentificationNo;
                    }
                    else
                    {
                        imagePath = imagePath + "/" + IdentificationNo;
                    }

                }
                //if (Type == "CustomerVehicle")
                //{

                //    if (!Directory.Exists(RootPath + imagePath + "/CustomerVehicle"))
                //    {
                //        Directory.CreateDirectory(RootPath + "Files" + "/CustomerVehicle");
                //        imagePath = imagePath + "/CustomerVehicle";
                //    }
                //    else
                //    {
                //        imagePath = imagePath + "/CustomerVehicle";
                //    }

                //    if (!Directory.Exists(RootPath + imagePath + "/" + IdentificationNo))
                //    {
                //        Directory.CreateDirectory(RootPath + imagePath + "/" + IdentificationNo);
                //        imagePath = imagePath + "/" + IdentificationNo;
                //    }
                //    else
                //    {
                //        imagePath = imagePath + "/" + IdentificationNo;
                //    }
                //}
                if (Type == "Employee")
                {
                    if (!Directory.Exists(RootPath + imagePath + "/Employee"))
                    {
                        Directory.CreateDirectory(RootPath + "Files" + "/Employee");
                        imagePath = imagePath + "/Employee";
                    }
                    else
                    {
                        imagePath = imagePath + "/Employee";
                    }
                    if (!Directory.Exists(RootPath + imagePath + "/" + IdentificationNo))
                    {
                        Directory.CreateDirectory(RootPath + imagePath + "/" + IdentificationNo);
                        imagePath = imagePath + "/" + IdentificationNo;
                    }
                    else
                    {
                        imagePath = imagePath + "/" + IdentificationNo;
                    }
                    if (Attachment == "Documents")
                    {
                        if (!Directory.Exists(RootPath + imagePath + "/Documents"))
                        {
                            Directory.CreateDirectory(RootPath + imagePath + "/Documents");
                            imagePath = imagePath + "/Documents";
                        }
                        else
                        {
                            imagePath = imagePath + "/Documents";
                        }
                    }
                    else if (Attachment == "Image")
                    {
                        if (!Directory.Exists(RootPath + imagePath + "/Photo"))
                        {
                            Directory.CreateDirectory(RootPath + imagePath + "/Photo");
                            imagePath = imagePath + "/Photo";
                        }
                        else
                        {
                            imagePath = imagePath + "/Photo";
                        }
                    }
                }

               
            }
            catch (Exception)
            {
                throw;
            }

            string Path = HostingEnvironment.ApplicationPhysicalPath;

            string[] ImageFile;
            try
            {
                ImageFile = image.Split(',');
                image = ImageFile[1];
                if (ImageFile[0].StartsWith("data:audio/mpeg"))
                {
                    Extention = ".mp3";
                }

                byte[] newBytes = new byte[8000];
                if (image != null)
                {
                    newBytes = Convert.FromBase64String(image);
                }
                filePath = imagePath + "/" + DateTime.Now.ToString("HHmmss") + Extention; //.png or jpg
                MemoryStream ms = new MemoryStream(newBytes, 0, newBytes.Length);
                ms.Write(newBytes, 0, newBytes.Length);                  //  string fileName = InstructorID + Extention; //.png or jpg

                if (Attachment == "Documents")
                {
                    File.WriteAllBytes(Path + filePath, newBytes);
                    return filePath;
                }
                else
                {
                    Image img = Image.FromStream(ms, true);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    img.Save(HostingEnvironment.MapPath("~" + filePath));
                }
                return filePath;
            }
            catch (Exception ex)
            {
                ExeceptionHandle(ex);
            }
            return "";
        }
        public void ExeceptionHandleWithMethodName(Exception ex, string MethodName)
        {
            try
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);

                string diagnoseResults = "";

                for (int i = 0; i < trace.FrameCount; i++)

                {

                    diagnoseResults += i.ToString() + ":" + trace.GetFrame(i).GetMethod().Name + ":" + trace.GetFrame(i).GetFileLineNumber() + ":" + trace.GetFrame(0).GetFileColumnNumber() + "-";

                }

                string[] diagnoseBlocks = diagnoseResults.Split(new char[] { '-' });

                string processesException = "";

                processesException += "Exception Message =  " + MethodName + "\r\n";

                processesException += "Exception Class = " + ex.TargetSite.ReflectedType.Name + "\r\n\r\n";

                processesException += "Exception Path:\r\n";

                processesException += "===============\r\n";

                for (int i = 0; i < diagnoseBlocks.Length - 1; i++)

                {

                    string[] diagnoseCell = diagnoseBlocks[i].Split(new char[] { ':' });

                    processesException += "Exception Level = " + i.ToString() + "\r\n";

                    processesException += "Exception Method = " + diagnoseCell[1].ToString() + "\r\n";

                    processesException += "Exception Line number = " + diagnoseCell[2].ToString() + "\r\n";

                    processesException += "Exception Column number = " + diagnoseCell[3].ToString() + "\r\n\r\n";

                }

                // StackFrame CallStack = new StackFrame(1, true);
                ////string a=" File: " + CallStack.GetFileName() + ", Line: " + CallStack.GetFileLineNumber();

                // var st = new StackTrace(ex, true);
                // // Get the top stack frame
                // var frame = st.GetFrame(0);
                // // Get the line number from the stack frame
                // //var line = frame.GetFileLineNumber();

                // int line = (new StackTrace(ex, true)).GetFrame(st.FrameCount - 1).GetFileLineNumber();

                //MethodName = MethodName + "  Line No: " + line;

                string startupPath = System.IO.Path.GetFullPath(@"..\..\");

                string h = Environment.CurrentDirectory;

                //image.Save(HostingEnvironment.MapPath("~" + "\\ErrorLog.txt"));

                string FileFath = HostingEnvironment.MapPath("~" + "/ErrorLog.txt");

                if (!File.Exists(FileFath))
                {
                    File.Create(FileFath);
                }
                string errorLogPath = @FileFath;
                File.AppendAllText(errorLogPath, Environment.NewLine + Environment.NewLine + Environment.NewLine + DateTime.Now + Environment.NewLine + processesException + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "StackTrace: " + ex.StackTrace + Environment.NewLine + ex.InnerException);
            }
            catch (Exception obj)
            {
                getExceptionGeneratedMethod(obj, "ExeceptionHandleWithMethodName");
            }
        }
        private void getExceptionGeneratedMethod(Exception obj, string v)
        {
            throw new NotImplementedException();
        }
        public void SendSms(string mobile, string Sms)
        {
            WebClient Client = new WebClient();
            string baseurl = " " + mobile + "&msg=" + Sms;
            Stream data = Client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public string GenerateRandonPassword(string RandonPassword, int noofcharacters)
        {
            char[] charArr = RandonPassword.ToCharArray();
            string RandomPassword = string.Empty;
            Random objran = new Random();
            // int noofcharacters = 15;
            for (int i = 0; i < noofcharacters; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!RandomPassword.Contains(charArr.GetValue(pos).ToString()))
                    RandomPassword += charArr.GetValue(pos);
                else
                    i--;
            }
            return RandomPassword;
        }

        #region Send SMS
        // call plivo api for sending mail
        public static IRestResponse<MessageResponse> SendSMSviaAPI(string destMobileNumber, string textTobeSent)
        {
            //RestAPI plivo = new RestAPI("Your AUTH_ID", "Your AUTH_TOKEN");
            //AUTH_ID = MAMJE3NDE1OGZHYMEXM2
            //AUTH_TOKEN= NjUyMWRkY2FmOTQ5YjlmZjcyMjVlOTAwYWJkYTI3

            RestAPI plivo = new RestAPI("MAMJE3NDE1OGZHYMEXM2", "NjUyMWRkY2FmOTQ5YjlmZjcyMjVlOTAwYWJkYTI3");

            IRestResponse<MessageResponse> resp = plivo.send_message(new Dictionary<string, string>()
            {
                { "src", "966563226748" }, // Sender's phone number with country code
                { "dst", destMobileNumber }, // Receiver's phone number with country code
                { "text", textTobeSent }, // Your SMS text message
                { "method", "POST"} // Method to invoke the url
                
                //{ "url", "http://example.com/report/"}, // The URL to which with the status of the message is sent
            });

            return resp;
        }
        #endregion
    }
}