using ItemmApp.Enums;

namespace ItemmApp.Models.Request;

public class InsertAttendanceRequest
{
    public string StudentCpf { get; private set; }
    public string Level { get; private set; }
    public string Module { get; private set; }
    public EAttendance Type { get; private set; }
    public DateTime AttendanceDate { get; private set; }

    public InsertAttendanceRequest(string studentCpf, string level, string module, EAttendance type, DateTime attendanceDate)
    {
        StudentCpf = studentCpf;
        Level = level;
        Module = module;
        Type = type;
        AttendanceDate = attendanceDate;
    }
}