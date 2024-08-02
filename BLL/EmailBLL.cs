using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public  static class EmailBLL
    {

        public static void SendConfirmationEmail(string email, Guid guid, string name)
        {
            MailMessage mailMessage = new MailMessage("notifydevhub@gmail.com",email);
            mailMessage.Subject = "DEVHUB Email Confirmation";
            StringBuilder sb = new StringBuilder();           
            sb.Append("<h1 style='background:#127EB1; padding:15px; color:white;'>Thanks for joining DEVHUB!</h1>");            
            sb.Append("<div style='padding:20px 25px; text-align:center; background:#cdcdcd;'>");
            sb.Append("<p style='font-size:20px;'> Hi " + name + ", </p>");
            sb.Append("<p style='font-size:25px; '>Please Confirm that your email address is correct to continue.<br> Click the link below to get started.</p>");
            sb.Append(" <a style='background:#127EB1; color:white; padding:10px 15px; border-radius:10px; cursor:pointer; text-decoration:none;' href='");
            sb.Append(HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery,"/Account/ConfirmUser?guid="+guid));
            sb.Append("'>Confirm Email Address</a></div>");
            mailMessage.Body = sb.ToString();
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("notifydevhub@gmail.com", "programmer@dev");
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);

        }

        public static void ResetPasswordEmail(string email, string guid)
        {
            MailMessage mailMessage = new MailMessage("notifydevhub@gmail.com", email);
            mailMessage.Subject = "DevHub Reset Password";
            StringBuilder sb = new StringBuilder();
            sb.Append("<h1 style='background:#127EB1; padding:15px; color:white;'>Reset Your Password</h1>");
            sb.Append("<div style='padding:20px 25px; text-align:center; background:#cdcdcd;'>");           
            sb.Append("<p style='font-size:25px; '><br> Click the link below to change your password.</p>");
            sb.Append(" <a style='background:#127EB1; color:white; padding:10px 15px; border-radius:10px; cursor:pointer; text-decoration:none;' href='");
            sb.Append(HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "/resetpassword?guid="+guid+"&email="+email));
            sb.Append("'>Reset Now</a></div>");
            mailMessage.Body = sb.ToString();
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("notifydevhub@gmail.com", "programmer@dev");
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);

        }

        public static void PasswordChangeEmail(string email)
        {
            MailMessage mailMessage = new MailMessage("notifydevhub@gmail.com", email);
            mailMessage.Subject = "DEVHUB Password changed";
            StringBuilder sb = new StringBuilder();
          
            sb.Append("<div style='padding:20px 25px; text-align:center; background:#cdcdcd;'>");
            sb.Append("<p style='font-size:25px; '><br> Your Password has been changed succesfully.</p>");
           
            mailMessage.Body = sb.ToString();
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("notifydevhub@gmail.com", "programmer@dev");
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);

        }



    }
}
