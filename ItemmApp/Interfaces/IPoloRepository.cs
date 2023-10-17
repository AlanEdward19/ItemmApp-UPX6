using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Interfaces;

public interface IPoloRepository
{
    Task<IEnumerable<PoloResponse>> GetPolosAsync();
}