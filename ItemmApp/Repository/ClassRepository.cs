using Flurl;
using Flurl.Http;
using ItemmApp.Helpers;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Repository;

public class ClassRepository : IClassRepository
{
    public async Task<IEnumerable<ClassResponse>> GetClassesAsync()
    {
        try
        {
            return await Constants.ApiUrl.AppendPathSegment("/Class")
                .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).GetJsonAsync<IEnumerable<ClassResponse>>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Enumerable.Empty<ClassResponse>();
    }
}