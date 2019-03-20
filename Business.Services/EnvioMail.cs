using Business.Services;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Business.Services
{
    public class EnvioMail
    {
        public static string ServidorCorreo
        {
            get
            {
                return (!string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("ServidorCorreo"))) ?
                    ConfigurationManager.AppSettings.Get("ServidorCorreo") : "Servidor de Correo no encontrado en Web.Config";
            }
        }

        /// <summary>
        /// Método que envía correo electrónico
        /// </summary>
        /// <param name="to">Dirección de correo donde va correo destino, si es mas de 1 va separado por ";"</param>
        /// <param name="from">Dirección de correo de envío</param>
        /// <param name="asunto">Asunto del Correo electrónico</param>
        /// <param name="msgBody">Mensaje a enviar</param>
        /// <returns></returns>
        public static bool enviarMail(string to, string from, string asunto, string msgBody)
        {
            MailMessage msg = new MailMessage();
            bool resp = false;
            string[] too = to.Split(';');

            foreach (string mail in too)
                msg.To.Add(mail);

            msg.From = new MailAddress(from);
            msg.Subject = asunto;
            msg.Body = msgBody;
            msg.Priority = MailPriority.Normal;
            msg.IsBodyHtml = true;

            SmtpClient clientSmtp = new SmtpClient(ServidorCorreo);

            try
            {
                clientSmtp.Send(msg);
                resp = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resp;
        }

        /// <summary>
        /// Método que envía correo electrónico
        /// </summary>
        /// <param name="to">Dirección de correo destinatario (Mas de un correo separado por ";")</param>
        /// <param name="from">Dirección de correo emisor</param>
        /// <param name="asunto">Asunto del Correo electrónico</param>
        /// <param name="msgBody">Mensaje a enviar</param>
        /// <param name="smtp">IP servidor de correo</param>
        /// <returns></returns>
        public static bool enviarMail(string to, string from, string asunto, string msgBody, string smtp)
        {
            MailMessage msg = new MailMessage();
            bool resp = false;
            string[] too = to.Split(';');

            foreach (string mail in too)
                msg.To.Add(mail);

            msg.From = new MailAddress(from);
            msg.Subject = asunto;
            msg.Body = msgBody;
            msg.Priority = MailPriority.Normal;
            msg.IsBodyHtml = true;

            SmtpClient clientSmtp = new SmtpClient(smtp);

            try
            {
                clientSmtp.Send(msg);
                resp = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resp;
        }


       
    }
}
