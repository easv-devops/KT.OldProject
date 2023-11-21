using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service.Services;

public class OrdreService
{
    private readonly OrdreRepository _ordreRepository;

    public OrdreService(OrdreRepository ordreRepository)
    {
        _ordreRepository = ordreRepository;
    }

    public IEnumerable<Ordre> GetAllOrdre()
    {
        return _ordreRepository.GetAllOrdre();
    }

    public Ordre CreateOrdre(int user_id)
    {
        return _ordreRepository.CreateOrdre(user_id);
    }

    public Ordre UpdateOrdre(int ordre_id, int user_id)
    {
        return _ordreRepository.UpdateOrdre(ordre_id, user_id);
    }

    public void deleteOrdre(int ordre_id)
    {
        _ordreRepository.DeleteOrdre(ordre_id);
    }
}