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


        string emailBody=""; 

        foreach (Avatar avatar in _emailRespository.GetOrdersAvatars(order_id))
            emailBody = emailBody + avatar.avatar_name+"\n";
        
        
        message.Body = new TextPart("plain")
        {
            Text =" Hello "+ user.full_name + "\n"+"\n"+
                  "Thanks for your order of the following items: "+"\n"+"\n"         
                           + emailBody +"\n"+
                "Your items is attached to this email." +"\n"+"\n"+
                  "" +
                  "Kind regards The Webshop Inc." 
                  
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