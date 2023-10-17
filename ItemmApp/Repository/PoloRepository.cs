using Flurl;
using Flurl.Http;
using ItemmApp.Helpers;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Repository;

public class PoloRepository : IPoloRepository
{
    public async Task<IEnumerable<PoloResponse>> GetPolosAsync()
    {
        try
        {
            return await Constants.ApiUrl.AppendPathSegment("/Polo")
                .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).GetJsonAsync<IEnumerable<PoloResponse>>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Enumerable.Empty<PoloResponse>();
    }
}