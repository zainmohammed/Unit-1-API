using Plivo.API;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzDeliveryAPI
{
    public static class SendSMSViaPlivo
    {
        #region Variable

        public const string SENDSMSFORREGISTRATION = "Your registration has been done successfully.Your OTP is ##OTP##";
        public const string SENDSMSPHONEUPDATE = "Your phone number has been updated successfully. Your OTP is ##OTP##";
        public const string SENDSMSFORCANCLESTATUS = "Your appointment has been canceled.";
        public const string SENDSMSFORESCHEDULESTATUS = "Your appointment has been Reschedule.";
        public const string SENDSMSFOREJECTSTATUS = "Your appointment has been Rejected.";
        public const string SENDSMSFOCONFIRMSTATUS = "Your appointment has been Confirm.";
        public const string SENDFORGOTPASSWORD = "Hello ##FULLNAME##, your password is ##PASSWORD##";
        public const string NOTIFICATIONREGISTER = "Your are registered successfully.";
        //public const string NOTIFICATIONDOCTORREPLYTOAPPOINTMENTTOPATIENT = "Hello, YOUR APPOINTMENT WITH DR ##DOCTORNAME## IS ##STATUS##";
        public const string NOTIFICATIONDOCTORREPLYTOAPPOINTMENTTOPATIENT = "Dear ##PATIENTNAME##, your appointment with Dr. ##DOCTORNAME## on ##APPOINTMENTDATE## by ##APPTIME## is ##STATUS##";
        public const string NOTIFICATIONPATIENTBOOKAPPOINTMENTTODOCTOR = "Dear Dr. ##DOCTORNAME##, a patient ##PATIENTNAME## wants to book the appointment with you on ##APPOINTMENTDATE## by ##APPTIME##.";
        public const string SENDSMSBOOKAPPOINTMENT = "Use ##OTP## to register your appointment with Dr. ##DRNAME##. You will receive confirmation shortly.";
        public const string SENDSMSCONFIRMAPPOINTMENT = "Your appointment with Dr. ##DorctorName## by ##APPTIME## ##APPDAY## on ##APPDATE## is ##STATUS##";

        public const string NOTIFICATIONPATIENTCHATREQUEST = "Dear Dr. ##DOCTORNAME##, a patient ##PATIENTNAME## would like to have chat with you.";
        public const string NOTIFICATIONPATIENTCHATREQUESTEND = "Dear Dr. ##DOCTORNAME##, a patient ##PATIENTNAME## ended chat.";
        public const string NOTIFICATIONDOCTORCHATREQUESTACCEPTREJECT = "Dear ##PATIENTNAME##, your chat request ##STATUS## by doctor Dr. ##DOCTORNAME##.";
        public const string NOTIFICATIONDOCTORCHATREQUESTEND = "Dear ##PATIENTNAME##, Your chat with doctor Dr. ##DOCTORNAME## has been ended.";

        public const string NOTIFICATIONPATIENTCHAT = "Dear Dr. ##DOCTORNAME##, you received chat message from patient ##PATIENTNAME##.";
        public const string NOTIFICATIONDOCTORCHAT = "Dear ##PATIENTNAME##, you received chat message from doctor Dr. ##DOCTORNAME##.";

        public const string NOTIFICATIONPATIENTRATING = "Dear ##PATIENTNAME##, kindly rate doctor Dr. ##DOCTORNAME## against your recent appointment.";

        public const string NOTIFICATIONREMINDAPPLICATIONTOPATIENT = "Reminder! Dear ##PATIENTNAME##, Today is your appointment with Dr. ##DOCTORNAME## at ##APPTIME##";

        public const string NOTIFICATIONATTEMPTAPPLICATIONTOPATIENT = "Dear Dr. ##DOCTORNAME##, has ##PATIENTNAME## attended the appointment with you at ##APPTIME## ?";
        public const string ALLPATIENTNOTIFICATIONAFTERDRREGISTER = "We are proud to welcome Dr. ##DOCTORNAME##, ##Qualification## ##Speciality## to Mawaidi.";


        public const string ArabicALLPATIENTNOTIFICATIONAFTERDRREGISTER = "إلى المويدي ##Speciality## ##Qualification## ,##DOCTORNAME## ونحن فخورون للترحيب الدكتور";
        public const string ArabicSENDSMSFORREGISTRATION = "##OTP## تم تسجيلك بنجاح. مكتب المدعي العام الخاص بك هو";
        public const string ArabicSENDSMSPHONEUPDATE = "##OTP## تم تحديث رقم هاتفك بنجاح";
        public const string ArabicSENDSMSFORCANCLESTATUS = "تم إلغاء موعدك";
        public const string ArabicSENDSMSFORESCHEDULESTATUS = "تم إعادة جدولة موعدك";
        public const string ArabicSENDSMSFOREJECTSTATUS = "تم رفض موعدك";
        public const string ArabicSENDSMSFOCONFIRMSTATUS = "تم تأكيد موعدك";
        public const string ArabicSENDFORGOTPASSWORD = "##PASSWORD## كلمة المرور هي ##FULLNAME##, مرحبا";
        public const string ArabicNOTIFICATIONREGISTER = "تم تسجيلك بنجاح";
        //public const string NOTIFICATIONDOCTORREPLYTOAPPOINTMENTTOPATIENT = "Hello, YOUR APPOINTMENT WITH DR ##DOCTORNAME## IS ##STATUS##";
        public const string ArabicNOTIFICATIONDOCTORREPLYTOAPPOINTMENTTOPATIENT = "##STATUS## إس ##APPTIME## بي ##APPOINTMENTDATE## إس ##DOCTORNAME## DR باتينتنام ,##PATIENTNAME## عزيزي";
        public const string ArabicNOTIFICATIONPATIENTBOOKAPPOINTMENTTODOCTOR = "##APPTIME## بواسطة ##APPOINTMENTDATE## يريد حجز موعد معك على ##PATIENTNAME## مريض ##DOCTORNAME## عزيزي الطبيب";
        public const string ArabicSENDSMSBOOKAPPOINTMENT = "سوف تتلقى تأكيد قريبا ##DRNAME## لتسجيل موعدك مع الدكتور ##OTP## استعمال";
        public const string ArabicSENDSMSCONFIRMAPPOINTMENT = "هو ##APPDATE## على ##APPTIME## ##APPDAY## بواسطة ##DorctorName## موعدك مع الدكتور";

        public const string ArabicNOTIFICATIONPATIENTCHATREQUEST = "أود أن الدردشة معك ##PATIENTNAME## مريض ##DOCTORNAME##  عزيزي الطبيب";
        public const string ArabicNOTIFICATIONPATIENTCHATREQUESTEND = "انتهت الدردشة ##PATIENTNAME## مريض ##DOCTORNAME##  عزيزي الطبيب";
        public const string ArabicNOTIFICATIONDOCTORCHATREQUESTACCEPTREJECT = "##DOCTORNAME## من قبل الطبيب الدكتور ##STATUS## طلب الدردشة ##PATIENTNAME## عزيزي";
        public const string ArabicNOTIFICATIONDOCTORCHATREQUESTEND = "قد انتهى ##DOCTORNAME## الدردشة الخاصة بك مع الطبيب الدكتور ##PATIENTNAME## عزيزي";

        public const string ArabicNOTIFICATIONPATIENTCHAT = "##PATIENTNAME## تلقيت رسالة دردشة من المريض ##DOCTORNAME## عزيزي الطبيب";
        public const string ArabicNOTIFICATIONDOCTORCHAT = "##DOCTORNAME## تلقيت رسالة دردشة من الطبيب ##PATIENTNAME## عزيزي";

        public const string ArabicNOTIFICATIONPATIENTRATING = " ضد موعدك الأخير  ##DOCTORNAME## يرجى تقييم الطبيب الدكتور ##PATIENTNAME## عزيزي";

        public const string ArabicNOTIFICATIONREMINDAPPLICATIONTOPATIENT = "##APPTIME## في ##DOCTORNAME## اليوم هو موعدك مع الدكتور ##PATIENTNAME## تذكير! العزيز";

        public const string ArabicNOTIFICATIONATTEMPTAPPLICATIONTOPATIENT = "##APPTIME## حضر التعيين معك في ##PATIENTNAME## لديها ##DOCTORNAME##  عزيزي الطبيب";
        #endregion

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