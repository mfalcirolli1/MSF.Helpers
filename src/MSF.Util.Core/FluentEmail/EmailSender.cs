using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using MSF.Util.Extensions;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MSF.Util.Core.FluentEmail
{
    internal class EmailSender
    {
        public async Task SendEmail()
        {
            try
            {
                var sender = new SmtpSender(() => new SmtpClient(host: "smtp.gmail.com")
                {
                    EnableSsl = false,
                    //DeliveryMethod = SmtpDeliveryMethod.Network,
                    //Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                    PickupDirectoryLocation = "C:\\Users\\Falt_\\Documentos\\github\\Log"
                });

                var template = new StringBuilder();
                template.AppendLine("Text here, @Model.FirstName");
                template.AppendLine("<p> Lorem ipsum Lorem ipsum Lorem ipsum @Model.ProductName </p>");
                template.AppendLine("Thanks!");

                Email.DefaultSender = sender;
                Email.DefaultRenderer = new RazorRenderer();

                var email = await Email
                    .From("fmatheus8@gmail.com")
                    .To("falt_the@hotmail.com ")
                    .Subject("Teste FluentEmail")
                    .UsingTemplate(template.ToString(), new { FirstName = "Name Test", ProductName = "Product Test" })
                    //.Body("Qualquer texto aqui")
                    .SendAsync();

                if (email.Successful)
                {
                    StringExtensions.PrintGreen("Email enviado com sucesso!");
                }
                else
                {
                    StringExtensions.PrintRed("Erro ao enviar o email");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
