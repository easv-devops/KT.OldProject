using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

public class EmailController
{
    private readonly EmailService _emailService;

    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }
 
    [HttpPost]
    [Route("/email")]
    public ResponseDto SendEmail([FromBody] Order order)
    {
    _emailService.SendEmail();
        
        return new ResponseDto()
        {
            MessageToClient = "Successfully created an order",
    };
    }
    
    
    
    
}