using infrastructure.DataModels;
using infrastructure.Repositories;
using MimeKit;

namespace service.Services;

public class EmailService
{
    
    private readonly EmailRespository _emailRespository;

    public EmailService(EmailRespository emailRespository)
    {
        _emailRespository = emailRespository;
    }
    
    
    
    
    public void SendEmail(int order_id)
    {
        User user = _emailRespository.GetOrdersUser(order_id);
        
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("The Webshop Inc.", Environment.GetEnvironmentVariable("fromemail")));
        message.To.Add(new MailboxAddress("Customer", user.Email));
        message.Subject = "Your order confirmation";

        message.Body = new TextPart("plain")
        {
            Text = @"Hello "+ user.full_name+"\n"
            +       user.Street+"\n"
            +       user.Zip+"\n"+"\n"           
            +       "You have ordered:"
            
            
        };

        foreach (Avatar avatar in _emailRespository.GetOrdersAvatars(order_id))
        {
            Console.WriteLine(avatar.avatar_name);
           
        }
        
        
        
        
        
        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate(Environment.GetEnvironmentVariable("fromemail"), Environment.GetEnvironmentVariable("frompass") );
            client.Send(message);
            client.Disconnect(true);
        }
    }
}