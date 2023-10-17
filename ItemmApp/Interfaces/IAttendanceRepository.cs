using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Interfaces;

public interface IAttendanceRepository
{
    Task<IEnumerable<AttendanceResponse>> GetAttendancesAsync();
    Task<bool> AddAsync(InsertAttendanceRequest request);
}