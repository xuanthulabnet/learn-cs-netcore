using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailUtils {
    public static class MailUtils {

        /// <summary>
        /// Gửi Email
        /// </summary>
        /// <param name="_from">Địa chỉ email gửi</param>
        /// <param name="_to">Địa chỉ email nhận</param>
        /// <param name="_subject">Chủ đề của email</param>
        /// <param name="_body">Nội dung (hỗ trợ HTML) của email</param>
        /// <param name="client">SmtpClient - kết nối smtp để chuyển thư</param>
        /// <returns>Task</returns>
        public static async Task<bool> SendMail(string _from, string _to, string _subject, string _body, SmtpClient client) {
            
            // Tạo nội dung Email        
            MailMessage message = new MailMessage (
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add (new MailAddress (_from));
            message.Sender = new MailAddress (_from);


            try {
                await client.SendMailAsync (message);
                return true;
            } catch (Exception ex) {
                Console.WriteLine (ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gửi Email sử dụng máy chủ SMTP cài đặt localhost
        /// </summary>
        /// <param name="_from">Địa chỉ email gửi</param>
        /// <param name="_to">Địa chỉ email nhận</param>
        /// <param name="_subject">Chủ đề của email</param>
        /// <param name="_body">Nội dung (hỗ trợ HTML) của email</param>
        /// <returns>Task</returns>
        public static async Task<bool> SendMailLocalSmtp ( string _from, string _to, string _subject, string _body)
        {
            using (SmtpClient client = new SmtpClient ("localhost")) {
                return await SendMail(_from, _to, _subject, _body, client);
            }
        }

        /// <summary>
        /// Gửi email sử dụng máy chủ SMTP Google (smtp.gmail.com)
        /// </summary>
         /// <param name="_from">Địa chỉ email gửi</param>
        /// <param name="_to">Địa chỉ email nhận</param>
        /// <param name="_subject">Chủ đề của email</param>
        /// <param name="_body">Nội dung (hỗ trợ HTML) của email</param>
        /// <param name="_gmailsend">Tài khoản gmail</param>
        /// <param name="_gmailpassword">Password gmail</param>
        /// <returns></returns>
        public static async Task<bool> SendMailGoogleSmtp (
            string _from,
            string _to,
            string _subject,
            string _body,
            string _gmailsend,
            string _gmailpassword) 
            {

            MailMessage message = new MailMessage (
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add (new MailAddress (_from));
            message.Sender = new MailAddress (_from);

            // Tạo SmtpClient kết nối đến smtp.gmail.com
            using (SmtpClient client = new SmtpClient ("smtp.gmail.com")) {
                client.Port = 587;
                client.Credentials = new NetworkCredential (_gmailsend, _gmailpassword);
                client.EnableSsl = true;
                return await SendMail(_from, _to, _subject, _body, client);
            }

        }
    }
}