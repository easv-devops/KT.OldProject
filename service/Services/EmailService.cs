﻿using infrastructure.DataModels;
using infrastructure.Repositories;
using MimeKit;

namespace service.Services;

public class EmailService
{
    
    private readonly OrderRepository _orderRepository;

    public EmailService(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    
    
    
    public void SendEmail(int order_id)
    {
        User user = _orderRepository.GetAnUser(order_id);
        
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("The Webshop Inc.", Environment.GetEnvironmentVariable("fromemail")));
        message.To.Add(new MailboxAddress("Customer", user.Email));
        message.Subject = "Your order confirmation";

        message.Body = new TextPart("plain")
        {
            Text = @"Hello"+ user.full_name
                    
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