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
            .WithOAuthBearerToken(Preferences.Get("token", string.Empty)).GetJsonAsync<IEnumerable<StudentResponse>>();
    }

    public async Task<bool> AddAsync(StudentRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(StudentRequest request)
    {
        throw new NotImplementedException();
    }
}