using api.Filters;
using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;


namespace api.Controllers;

public class SearchController : ControllerBase
{
    private readonly SearchService _searchService;

    public SearchController(SearchService searchService)
    {
        _searchService = searchService;
    }

    /*
     * performs a search for avatars based on a given search query.
     * It takes a searchQuery parameter from the query string and returns a ResponseDto
     * object with a success message and the search results.
     */
    [HttpGet]
    [Route("api/search")]
    public ResponseDto SearchAvatar([FromQuery] string searchQuery)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Succesfully searched for an Avatar",
            ResponseData = _searchService.SearchAvatar(searchQuery)
        };
    }
}
