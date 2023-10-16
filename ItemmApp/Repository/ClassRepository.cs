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
        return await Constants.ApiUrl.AppendPathSegment("/Class")
            .WithOAuthBearerToken(Preferences.Get("token", string.Empty)).GetJsonAsync<IEnumerable<ClassResponse>>();
    }

    public async Task<bool> AddAsync(ClassRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(ClassRequest request)
    {
        throw new NotImplementedException();
    }
}