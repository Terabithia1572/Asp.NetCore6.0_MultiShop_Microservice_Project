using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MultiShop.WebUI.Models;

namespace MultiShop.WebUI.Controllers
{
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult SendMail()
        {
            return View();
        }
        [HttpPost]

        public IActionResult SendMail(MailRequest mailRequest)
        {
            MimeMessage mimeMessage = new MimeMessage(); // Yeni bir MimeMessage nesnesi oluşturuyoruz
            MailboxAddress mailboxAddressFrom = new MailboxAddress("MultiShop Admin", "terabithia188@gmail.com"); // Gönderen bilgilerini ekliyoruz
            mimeMessage.From.Add(mailboxAddressFrom); // Gönderen adresini MimeMessage nesnesine ekliyoruz
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail); // Alıcı bilgilerini ekliyoruz

            mimeMessage.To.Add(mailboxAddressTo); // Alıcı adresini MimeMessage nesnesine ekliyoruz

            var bodyBuilder = new BodyBuilder(); // Mail içeriğini oluşturmak için BodyBuilder kullanıyoruz

            bodyBuilder.TextBody = mailRequest.MessageContent; // Mail içeriğini BodyBuilder'a ekliyoruz
            mimeMessage.Body = bodyBuilder.ToMessageBody(); // BodyBuilder'dan oluşturulan içeriği MimeMessage'a ekliyoruz
            mimeMessage.Subject = mailRequest.Subject; // Mail konusunu MimeMessage'a ekliyoruz
            SmtpClient client = new SmtpClient(); // SMTP istemcisi oluşturuyoruz
            client.Connect("smtp.gmail.com", 587, false); // SMTP sunucusuna bağlanıyoruz
            client.Authenticate("terabithia188@gmail.com", "abphrryupnkcvoyo"); // SMTP sunucusunda kimlik doğrulaması yapıyoruz
            client.Send(mimeMessage); // Maili gönderiyoruz
            client.Disconnect(true); // Bağlantıyı kapatıyoruz
            return View(); // Gönderim işlemi tamamlandıktan sonra aynı sayfaya yönlendiriyoruz

        }
    }
}
