using System.Net;
using infrastructure.DataModels;
using infrastructure.Repositories;
using MimeKit;

namespace service.Services;

public class EmailService
{
    
    private readonly EmailRepository _emailRespository;
 
      
    public EmailService(EmailRepository emailRespository)
    {
        _emailRespository = emailRespository;
    }
    
    /*
     * Sends an email for the specified order ID.
     */
    public void SendEmail(int order_id)
    {
     
     MimeMessage message = new MimeMessage();
     BodyBuilder builder = new BodyBuilder ();
        
      UserModel user = _emailRespository.GetOrdersUser(order_id);
         
        makeEmailHeader(order_id, user, message);
        makeEmailBody(order_id, user, builder);
        attachment(order_id,builder,message);
        sendEmail(message);
    }
    
    /*
     * Creates the email header with sender, recipient, and subject information.
     */
    void makeEmailHeader(int order_id,UserModel user,MimeMessage message)
    {
        message.From.Add(new MailboxAddress("The Webshop Inc. order no: " + order_id, Environment.GetEnvironmentVariable("fromemail")));
        message.To.Add(new MailboxAddress("Customer", user.email));
        message.Subject = "Your order confirmation";
    }
    
    /*
     * Creates the email body with personalized greeting, order details, and a closing message.
     */
    public void makeEmailBody(int order_id, UserModel user,BodyBuilder builder)
    {
        string emailBody=""; 
        foreach (AvatarModel avatar in _emailRespository.GetOrdersAvatars(order_id))
            emailBody = emailBody + avatar.avatar_name+"\n";
        
            builder.TextBody = " Hello " + user.full_name + "\n" + "\n" +
                               "Thanks for your order no: "+order_id +" including the following items: " + "\n" + "\n"
                               + emailBody + "\n" +
                               "Your items is attached to this email." + "\n" + "\n" +
                               "" +
                               "Kind regards The Webshop Inc."; 
      }
    
    /*
     * Downloads avatar images and attaches them to the email.
     */
    public void attachment(int order_id,BodyBuilder builder, MimeMessage message)
    {
        WebClient webClient = new WebClient();

        foreach (AvatarModel avatar in _emailRespository.GetOrdersAvatars(order_id))
        {
            string fileName = avatar.avatar_name + ".png";
            webClient.DownloadFile("https://robohash.org/"+fileName, Path.Combine(fileName));   
            builder.Attachments.Add (Path.Combine(fileName));
        }   
            
        builder.Attachments.Add (Path.Combine("invoice.pdf"));
        message.Body = builder.ToMessageBody();
        
       foreach (AvatarModel avatar in _emailRespository.GetOrdersAvatars(order_id))
       {
           string fileName = avatar.avatar_name + ".png";
           File.Delete(Path.Combine(fileName));
       }   
    }
    
    /*
     * Sends the email using SMTP with Gmail credentials.
     */
    public void sendEmail(MimeMessage message)
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