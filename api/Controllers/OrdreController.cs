using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

public class OrdreController : Controller
{
    private readonly OrdreService _orderService;

    public OrdreController(OrdreService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Route("/ordre/all")]
    public IEnumerable<Ordre> GetAllOrdre()
    {
        try
        {
            return _orderService.GetAllOrdre();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Error when getting all Ordre", e);
        }
    }

    [HttpPost]
    [Route("/ordre")]
    public object postOrdre([FromBody] Ordre ordre)
    {
        return _orderService.CreateOrdre(ordre.user_id);
    }

    [HttpPut]
    [Route("/ordre/{ordre_id}")]
    public Object putOrdre([FromBody] int ordre_id, [FromBody] Ordre ordre)
    {
        return _orderService.UpdateOrdre(ordre_id, ordre.user_id);
    }

    [HttpDelete]
    [Route("/ordre/{ordre_id}")]
    public void deleteOrdre([FromRoute] int ordre_id)
    {
        _orderService.deleteOrdre(ordre_id);
    }
}