using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Model
{
    class EmailSendServiceClass
    {
        #region vars
        private string strLogin;         // email, c которого будет рассылаться почта
        private string strPassword;  // пароль к email, с которого будет рассылаться почта
        private string strSmtp = "smtp.yandex.ru"; // smtp-server
        private int iSmtpPort = 25;                // порт для smtp-server
        #endregion
        public EmailSendServiceClass(string sLogin, string sPassword)
        {
            strLogin = sLogin;
            strPassword = sPassword;
        }

        private void SendMail(string mail, string name) // Отправка email конкретному адресату
        {
            using (MailMessage mm = new MailMessage(strLogin, mail))
            {
                mm.Subject = "none";//strSubject;
                mm.Body = "Hello world!";
                mm.IsBodyHtml = false;
                SmtpClient sc = new SmtpClient(strSmtp, iSmtpPort);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(strLogin, strPassword);
                try
                {
                    sc.Send(mm);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Невозможно отправить письмо " + ex.ToString());
                }
            }
        }//private void SendMail(string mail, string name)

    }  //private void SendMail(string mail, string name)

    //Альтернативный класс для отправки
    static class ServiceData
    {
        //static public List<string> SmptClients = new List<string>()
        //{ "smtp.yandex.ru", "smtp.gmail.com","smtp.mail.ru" };
        //    static public List<int> Ports = new List<int>() { 25, 58, 25 };
        static public List<SmtpClient> SmtpClients { get; } = new List<SmtpClient>() { new SmtpClient("smtp.yandex.ru", 25), new SmtpClient("smtp.gmail.com", 58), new SmtpClient("smtp.mail.ru", 25) };

    }


    class EMailSendServiceClass
    {





        static public event Action<string> Action;//Любое событие может вызываться только внутри класса в котором оно определено
        public void Send(MailMessage message, string password, int port)
        {
            try
            {
                //var fromAddress = new MailAddress("geekbrains2021@gmail.com", "Tester");
                //var toAddress = new MailAddress("geekbrains2021@gmail.com", "Tester");
                //Преобазование из Windows-1251 в UTF-8
                string subject = message.Subject;// tbSubject.Text;
                //string fio = "from me";
                //rtbBody.SelectAll();
                password = System.IO.File.ReadAllText("C:\\temp\\1.txt");//  tbPassword.Password;
                string body = message.Body;// tbBody.Text; //rtbBody.Selection.Text; // dataTasks.Value + "\r\n" + dataResult.Value;// win1251.GetString(win1251Bytes3)+"\r\n"+ win1251.GetString(win1251Bytes2);                
                var smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("geekbrains2021@gmail.com", password)
                };
                Action?.Invoke("Message is sending");
                smtp.Send(message);
                //MessageBox.Show("Message has sent");
                //Debug.WriteLine("Message has sent");
                Action?.Invoke("Message has sent");
            }
            catch (Exception ex)
            {
                //using System.Diagnostic
                Debug.WriteLine(ex);
                //Console.WriteLine(ex);
                Action?.Invoke(ex.Message);
            }


        }
    }
}
