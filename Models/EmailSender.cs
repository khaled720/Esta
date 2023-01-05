using System.Net.Mail;

namespace ESTA.Models
{
    public static class EmailSender
    {
        public static  bool Send_Mail(string to, string body, string subject, string fromtitle)
        {
            

            try
            {
                SmtpClient smtpClient = new SmtpClient();
                //          < add key = "host" value = "smtp.gmail.com" />

                //< add key = "port" value = "587" />
                smtpClient.Host = "smtp.gmail.com"; //System.Configuration.ConfigurationManager.AppSettings["host"];
                smtpClient.Port = 587;// int.Parse(System.Configuration.ConfigurationManager.AppSettings["port"]);

                //  smtpClient.Port = 587;  //for gmail test
                MailAddress toAddress = new MailAddress(to);


                string frommail = "khaledkiko2222@gmail.com";
                MailAddress fromAddress = new MailAddress(frommail, fromtitle);

                //credentialPassword
                MailMessage message = new MailMessage(fromAddress, toAddress);
                string password = "klbrbxoiozcwntvq";//System.Configuration.ConfigurationManager.AppSettings["credentialPassword"];

                message.IsBodyHtml = true;
                message.Body = body;
                message.Subject = subject;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                //   ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                bool EnableSslValue = true;//Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableSslFlag"]);

                smtpClient.EnableSsl = EnableSslValue;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(frommail, password);
                 smtpClient.Send(message);


                return true;

            }
            catch (Exception ex)
            { return false; }
        }

    }
}
