using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

public class EmailController
{
    private readonly EmailService _emailService;
    private readonly PdfService _pdfService;

    public EmailController(EmailService emailService, PdfService pdfService)
    {
        _emailService = emailService;
        _pdfService = pdfService;
    }
 
    /*
     * Sends an email to the customer with the order details.
     */
    [HttpPost]
    [Route("api/email/{order_id}")]
    public ResponseDto SendEmail([FromRoute] int order_id)
    { 
        _emailService.SendEmail(order_id);
        return new ResponseDto()
        {
            MessageToClient = "Successfully created an order",
        }; 
    }
    
    
    [HttpPost]
    [Route("api/PDF/{order_id}")]
    public ResponseDto makePDF ([FromRoute] int order_id)
    { 
        _pdfService.CreatePdf(order_id);
        
        return new ResponseDto()
        {
            MessageToClient = "Successfully created an order",
        }; 
    }
    
    
}