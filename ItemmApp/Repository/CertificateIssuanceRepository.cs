using Flurl;
using Flurl.Http;
using ItemmApp.Helpers;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Repository;

public class CertificateIssuanceRepository : ICertificateIssuanceRepository
{
    public async Task<CertificateIssuanceResponse> GetCertificateIssuanceAsync(string cpf)
    {
        try
        {
            return await Constants.ApiUrl.AppendPathSegment("/CertificateIssuance").SetQueryParam("cpf", cpf)
                .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).GetJsonAsync<CertificateIssuanceResponse>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return new();
    }
}