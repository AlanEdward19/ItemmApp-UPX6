using Flurl;
using Flurl.Http;
using ItemmApp.Helpers;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Repository;

public class AssessmentRepository : IAssessmentRepository
{
    public async Task<IEnumerable<AssessmentResponse>> GetAssessmentsAsync(string id)
    {
        return await Constants.ApiUrl.AppendPathSegment("/Assessment").SetQueryParam("studentId", id)
            .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).GetJsonAsync<IEnumerable<AssessmentResponse>>();
    }

    public async Task<bool> AddAsync(InsertAssessmentRequest request)
    {
        var response = await Constants.ApiUrl.AppendPathSegment($"/Assessment")
            .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).PostJsonAsync(request);

        return response.ResponseMessage.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var response = await Constants.ApiUrl.AppendPathSegment($"/Assessment").SetQueryParam("id", id)
            .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).DeleteAsync();

        return response.ResponseMessage.IsSuccessStatusCode;
    }
}