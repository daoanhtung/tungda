using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Web;

namespace MyWebsite.Utils
{
    public class Mail
    {
        #region Singleton
        private static Mail instance;

        public static Mail Instance
        {
            get { return instance ?? (instance = new Mail()); }
        }

        #endregion

        private SmtpClient smtp;
        private Mail ()
        {
            // your remote SMTP server IP.
            smtp = new SmtpClient { Host = Common.GetAppStr("MailServer") };
            var networkCred = new System.Net.NetworkCredential
            {
                UserName = Common.GetAppStr("email_ID"),
                Password = Common.GetAppStr("email_password")
            };
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = networkCred;
            smtp.Port = int.Parse(Common.GetAppStr("email_port"));
            smtp.EnableSsl = Convert.ToBoolean(Common.GetAppStr("IsSSLEnabled"));

        }

        public bool SendMail(string toAddresses, string subject, string content)
        {
            try
            {
                var msg = new MailMessage { From = new MailAddress(Common.GetAppStr("email_ID")) };
                msg.To.Add(toAddresses);
                msg.Subject = subject;
                msg.Body = content;
                msg.IsBodyHtml = true;
                
                smtp.Send(msg);
                return true;
            }
            catch (Exception)
            {
                //TODO: log or catch specific ex later
                return false;
            }
        }
    }
}
