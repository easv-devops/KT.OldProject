using System.ComponentModel.DataAnnotations;
using System.Net;
using infrastructure.DataModels;
using infrastructure.Repositories;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace service.Services;

public class PdfService
{
    private int _order_nr = 0;
    private UserModel user;
    private readonly EmailRepository _emailRespository;
  
    public PdfService(EmailRepository emailRespository)
    {
        _emailRespository = emailRespository;
    }
    
    
    
    
    public void CreatePdf(int order_id)
    {

        try
        {
            QuestPDF.Settings.License = LicenseType.Community;
            _order_nr = order_id;
            user = _emailRespository.GetOrdersUser(order_id);
        

            Document.Create(container =>
                    {
                        container.Page(page =>
                        {
                            page.Size(PageSizes.A4);
                            page.Margin(2, Unit.Centimetre);
                            page.DefaultTextStyle(x => x.FontSize(16));


                            page.Header()
                                .Element(Header); //Element er et refraktoringsværktøj, hvor man kalder betoden fra.
                            page.Content().Element(Content);
                            page.Footer().Element(Footer);

                        });

                    }

                )
                .GeneratePdf("invoice.pdf");
        }
        catch (Exception e)
        {
            throw new ValidationException("Error in sending pdf");
        }
       


    }
    
    
void Header(IContainer container)
{
    container
        .Row(row =>
        {
            row.Spacing(25);
            row.ConstantItem(100)
                .Image(Path.Combine("webshop.png"));
            
            row.RelativeItem().Column(column =>
            {
                column.Item()
                    .Text("Invoice")
                    .FontSize(36)
                    .FontColor(Colors.Orange.Medium)
                    .SemiBold();

               
                    column.Item().Text("Invoice for " + user.full_name);
                    column.Item().Text("Getting order no: " + _order_nr);
            });
        });
}



void Content(IContainer container)
{
    container
        .PaddingVertical(25)
        .Table(table =>
        {
            table.ColumnsDefinition(coloums =>
            {
                coloums.ConstantColumn(50);
                coloums.RelativeColumn();
                coloums.ConstantColumn(50);
                coloums.ConstantColumn(75);
                
            });
            
            table.Header(header =>
            {
                header.Cell().Text("#");
                header.Cell().Text("Product Name");
                header.Cell().AlignRight().Text("Image");
                header.Cell().AlignRight().Text("Total");
                
            });

            WebClient webClient = new WebClient();

            int i = 0;
            var total = 0;
            foreach (AvatarModel avatar in _emailRespository.GetOrdersAvatars(_order_nr))
            {
                string fileName = avatar.avatar_name + ".png";
            
                webClient.DownloadFile("https://robohash.org/"+fileName, Path.Combine(fileName));
                i++;
                total = total+avatar.avatar_price;
                
                table.Cell().Text(i);
                table.Cell().Text("avatar.avatar_name " +i);
                table.Cell().Image(fileName).FitArea();
                table.Cell().AlignRight().Text($"{avatar.avatar_price:F2} $");    
            }

           
            
            table.Cell().PaddingTop(5).BorderTop(2).Text("");
            table.Cell().PaddingTop(5).BorderTop(2).Text("Total");
            table.Cell().PaddingTop(5).BorderTop(2).Text("");
            table.Cell().PaddingTop(5).BorderTop(2).AlignRight().Text($"{total:F2} $");    
        });
    
    foreach (AvatarModel avatar in _emailRespository.GetOrdersAvatars(_order_nr))
    {
        string fileName = avatar.avatar_name + ".png";
        File.Delete(Path.Combine(fileName));
    }   
    
    
    
}




 void Footer(IContainer container)
{
    container.AlignCenter().Text(text =>
    {
    text.DefaultTextStyle(x=>x.FontSize(16));
    text.Span("Page Numbner ");
    text.CurrentPageNumber();
    text.Span(" out of ");
    text.TotalPages();
    });
    
    
    
}


}