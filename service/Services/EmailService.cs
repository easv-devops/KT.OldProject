using System.Net;
using infrastructure.DataModels;
using infrastructure.Repositories;
using MimeKit;

namespace service.Services;

public class EmailService
{
    
    private readonly EmailRespository _emailRespository;
    private  User user;
    private  int _order_id;
    private MimeMessage message = new MimeMessage();
    private BodyBuilder builder = new BodyBuilder ();
    

    public EmailService(EmailRespository emailRespository)
    {
        _emailRespository = emailRespository;
    }
    
    
    
    public void SendEmail(int order_id)
    {
        _order_id = order_id;
        user = _emailRespository.GetOrdersUser(_order_id);
         
        makeEmailHeader();
        makeEmailBody();
        attachment();
        sendEmail();
      
    }


    void makeEmailHeader()
    {
        message.From.Add(new MailboxAddress("The Webshop Inc.", Environment.GetEnvironmentVariable("fromemail")));
        message.To.Add(new MailboxAddress("Customer", user.Email));
        message.Subject = "Your order confirmation";

    }
    
    
    public void makeEmailBody()
    {
        string emailBody=""; 

        foreach (Avatar avatar in _emailRespository.GetOrdersAvatars(_order_id))
            emailBody = emailBody + avatar.avatar_name+"\n";


        
            builder.TextBody = " Hello " + user.full_name + "\n" + "\n" +
                               "Thanks for your order no: "+_order_id +" including the following items: " + "\n" + "\n"
                               + emailBody + "\n" +
                               "Your items is attached to this email." + "\n" + "\n" +
                               "" +
                               "Kind regards The Webshop Inc."; 
      }


    public void attachment()
    {
        WebClient webClient = new WebClient();

        foreach (Avatar avatar in _emailRespository.GetOrdersAvatars(_order_id))
        {
            string fileName = avatar.avatar_name + ".png";
            webClient.DownloadFile("https://robohash.org/"+fileName, Path.Combine(fileName));   
            builder.Attachments.Add (Path.Combine(fileName));
                
        }   
            
        message.Body = builder.ToMessageBody();


        foreach (Avatar avatar in _emailRespository.GetOrdersAvatars(_order_id))
        {
            string fileName = avatar.avatar_name + ".png";
            File.Delete(Path.Combine(fileName));
        }   
    }
    
    
    
    
    public void sendEmail()
    {
        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate(Environment.GetEnvironmentVariable("fromemail"), Environment.GetEnvironmentVariable("frompass") );
            client.Send(message);
            client.Disconnect(true);
        }
    }

   
    
    
}