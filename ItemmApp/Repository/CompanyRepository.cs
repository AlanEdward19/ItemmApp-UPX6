using Flurl;
using Flurl.Http;
using ItemmApp.Helpers;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Repository;

public class CompanyRepository : ICompanyRepository
{
    public async Task<IEnumerable<CompanyResponse>> GetCompaniesAsync()
    {
        try
        {
            return await Constants.ApiUrl.AppendPathSegment("/Company")
                .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).GetJsonAsync<IEnumerable<CompanyResponse>>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Enumerable.Empty<CompanyResponse>();
    }
}