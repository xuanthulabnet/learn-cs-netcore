using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using MailUtils;

namespace CS030_SendMail {
    class Program {

        static void Main (string[] args) {

            String mailnhan = "youremail.abc@gmail.com";
            String mailgui = "yoremail.xyz@gmail.com";
            String chude = "Kiểm tra email gửi đi";
            String noidung = @"<h1>Xin chào XuanThuLab</h1><p>Đây là mail gửi sử dụng gmail</p>";

            string smtpacount = "yourmailacount@gmail.com";
            string smtppassword = "PASSWORDCUABAN";

            
            MailUtils.MailUtils.SendMailGoogleSmtp(
                mailgui,
                mailnhan,
                chude,
                noidung,
                smtpacount,
                smtppassword

            ).Wait();


            // Nếu hàm main là asyn chì gọi
            // await MailUtils.MailUtils.SendMailGoogleSmtp(...);
        }
    }
}