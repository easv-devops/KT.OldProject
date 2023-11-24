using MimeKit;

namespace service.Services;

public class EmailService
{
    public void SendEmail()
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("The Webshop Inc.", Environment.GetEnvironmentVariable("fromemail")));
        message.To.Add(new MailboxAddress("Customer", Environment.GetEnvironmentVariable("toemail")));
        message.Subject = "Your order confirmation";

        message.Body = new TextPart("plain")
        {
            Text = @"Total order price: "
        };

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate(Environment.GetEnvironmentVariable("fromemail"), Environment.GetEnvironmentVariable("frompass") );
            client.Send(message);
            client.Disconnect(true);
        }
    }
}