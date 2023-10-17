using Flurl;
using Flurl.Http;
using ItemmApp.Helpers;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Repository;

public class StudentRepository : IStudentRepository
{
    public async Task<IEnumerable<StudentResponse>> GetStudentsAsync()
    {
        return await Constants.ApiUrl.AppendPathSegment("/Student")
            .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).GetJsonAsync<IEnumerable<StudentResponse>>();
    }

    public async Task<bool> AddAsync(StudentRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(StudentRequest request, string cpf)
    {
        var response = await Constants.ApiUrl.AppendPathSegment($"/Student").SetQueryParam("cpf", cpf)
            .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).PutJsonAsync(request);

        return response.ResponseMessage.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string cpf)
    {
        var response = await Constants.ApiUrl.AppendPathSegment($"/Student").SetQueryParam("cpf", cpf)
            .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).DeleteAsync();

        return response.ResponseMessage.IsSuccessStatusCode;
    }
}