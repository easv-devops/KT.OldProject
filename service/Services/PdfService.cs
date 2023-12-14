using System.Net;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace service.Services;

public class PdfService
{
    public void CreatePdf(int Order_nr)
    {
        QuestPDF.Settings.License = LicenseType.Community; 

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
            .GeneratePdf();


    }
    
    
void Header(IContainer container)
{
    container
        .Row(row =>
        {
            row.Spacing(25);
            row.ConstantItem(100)
                .Image("service/Services/webshop.png");
            
            row.RelativeItem().Column(column =>
            {
                column.Item()
                    .Text("Invoice")
                    .FontSize(36)
                    .FontColor(Colors.Orange.Medium)
                    .SemiBold();

                for (int i=0;i < 3; i++)
                {
                    column.Item().Text(Placeholders.Label());
                }
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
            for (int i = 1; i < 8; i++)
            {
                string fileName = "avatar.avatar_name " +i+ ".png";
            
                webClient.DownloadFile("https://robohash.org/"+fileName, Path.Combine(fileName)); 
                var pris=12;
                
                table.Cell().Text(i);
                table.Cell().Text("avatar.avatar_name " +i);
                table.Cell().Image(fileName).FitArea();
                table.Cell().AlignRight().Text($"{pris:F2} $");    
            }

            var total = 123;
            
            table.Cell().PaddingTop(5).BorderTop(2).Text("");
            table.Cell().PaddingTop(5).BorderTop(2).Text("Total");
            table.Cell().PaddingTop(5).BorderTop(2).Text("");
            table.Cell().PaddingTop(5).BorderTop(2).AlignRight().Text($"{total:F2} $");    
        });
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