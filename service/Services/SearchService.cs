using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service.Services;

public class SearchService
{
    private readonly SearchRepository _searchRepository;

    /*
     * Constructor to initialize SearchService with a SearchRepository instance.
     */
    public SearchService(SearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }

    /*
     * Search avatars based on the given query using the injected SearchRepository.
     */
    public IEnumerable<AvatarModel> SearchAvatar(string searchQuery)
    {
        return _searchRepository.SearchAvatar(searchQuery);
    }
}
