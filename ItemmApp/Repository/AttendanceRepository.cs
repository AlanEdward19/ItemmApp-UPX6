using Flurl;
using Flurl.Http;
using ItemmApp.Helpers;
using ItemmApp.Interfaces;
using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Repository;

public class AttendanceRepository : IAttendanceRepository
{
    public async Task<IEnumerable<AttendanceResponse>> GetAttendancesAsync()
    {
        return await Constants.ApiUrl.AppendPathSegment("/Attendance")
            .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).GetJsonAsync<IEnumerable<AttendanceResponse>>();
    }

    public async Task<bool> AddAsync(InsertAttendanceRequest request)
    {
        var response = await Constants.ApiUrl.AppendPathSegment($"/Attendance")
            .WithOAuthBearerToken(await SessionHelper.GetTokenAsync()).PostJsonAsync(request);

        return response.ResponseMessage.IsSuccessStatusCode;
    }
}