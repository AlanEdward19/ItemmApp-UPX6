using Flurl;
using Flurl.Http;
using ItemmApp.Helpers;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Repository;

public class FunctionRepository : IFunctionRepository
{
    public async Task<IEnumerable<FunctionResponse>> GetFunctionsAsync()
    {
        try
        {
            return await Constants.ApiUrl.AppendPathSegment("/Function")
                .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).GetJsonAsync<IEnumerable<FunctionResponse>>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Enumerable.Empty<FunctionResponse>();
    }
}