using Serilog;
using Serilog.Core;
using System.Net.Mail;

namespace ESTA.Models
{
    public static class EmailSender
    {
        private static IConfiguration _configuration;
        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static bool Send_Mail(string to, string body, string subject, string fromtitle)
        {


            try
            {
                SmtpClient smtpClient = new SmtpClient();
                //          < add key = "host" value = "smtp.gmail.com" />
                smtpClient.Host = _configuration.GetValue<string>("Mail:Host");
                smtpClient.Port = _configuration.GetValue<int>("Mail:port");
                //configuration.GetValue<int>("Formatting:Number:Precision");
                //  smtpClient.Port = 587;  //for gmail test

                string frommail = _configuration.GetValue<string>("Mail:fromMail");

                string password = _configuration.GetValue<string>("Mail:credentialPassword");

                //< add key = "port" value = "587" />
                //smtpClient.Host = "smtp.gmail.com"; //System.Configuration.ConfigurationManager.AppSettings["host"];
                //smtpClient.Port = 587;// int.Parse(System.Configuration.ConfigurationManager.AppSettings["port"]);

                //  smtpClient.Port = 587;  //for gmail test
                MailAddress toAddress = new MailAddress(to);


                //string frommail = "mistnews558@gmail.com";
                MailAddress fromAddress = new MailAddress(frommail, fromtitle);

                //credentialPassword
                MailMessage message = new MailMessage(fromAddress, toAddress);
                //string password = "ttuvfcgxsxsxrofs";//System.Configuration.ConfigurationManager.AppSettings["credentialPassword"];

                message.IsBodyHtml = true;
                message.Body = body;
                message.Subject = subject;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                //   ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                bool EnableSslValue = _configuration.GetValue<bool>("Mail:EnableSslFlag"); ;//Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableSslFlag"]);

                smtpClient.EnableSsl = EnableSslValue;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(frommail, password);
                smtpClient.Send(message);


                return true;

            }
            catch (Exception ex)
            {
                Log.Error("Email Sender: " + ex.Message);
                return false;
            }
        }

    }
}
