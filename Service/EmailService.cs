using InventoryAPI.Helper;
using InventoryAPI.Model.DTOs.Mail;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace InventoryAPI.Service
{
    public class EmailService(IOptions<BaseSetup> setup) : IEmailService
    {
        private readonly BaseSetup _setup = setup.Value;
        public async Task<ResponseModel<string>> SendEmail(MailRequest mail)
        {
            try
            {
              var response = new ResponseModel<string>();
                //Mimekit
                var message = new MimeMessage
                {
                    To = { MailboxAddress.Parse(mail.Reciever) },
                    Sender = MailboxAddress.Parse(_setup.Sender),
                    Subject = mail.Subject,
                    Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    {
                        Text = mail.Body,
                    }
                };
                //inject smpt
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("username", "password"),
                    EnableSsl = true,
                };

                smtpClient.Send("from@gmail.com", "to@gmail.com", "Subject", "Body");

                //using (var client = new SmtpClient())
                //{
                //    client.Connect(_setup.Server, _setup.Port);
                //    client.Authenticate(_setup.Sender,_setup.Password);
                //    await client.SendAsync(message);
                //    client.Disconnect(true);
                //}
                return response.Successful("Email was sent successfully");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
