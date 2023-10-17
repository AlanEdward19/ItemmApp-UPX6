using ItemmApp.Enums;

namespace ItemmApp.Models.Request;

public class StudentRequest
{
    public string Name { get; private set; }
    public string City { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string CompanyCnpj { get; private set; }
    public Guid FunctionId { get; private set; }
    public Guid ClassId { get; private set; }
    public int PracticeHours { get; private set; }
    public int TheoreticalHours { get; private set; }
    public EModel Model { get; private set; }
    public EReasonForTermination ReasonForTermination { get; private set; }
    public int ContractPeriod { get; private set; }
    public bool Status { get; private set; } //Ativo ou Inativo
    public string PhoneNumber { get; private set; }
    public Guid PoloId { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime AdmissionDate { get; private set; }
    public int FirstDayOfTrainingIntroduction { get; private set; }
    public int FinalDayTrainingIntroduction { get; private set; }
    public int FirstDayOfWeeklyTraining { get; private set; }
    public EWeekDay DayOfTrainingWeek { get; private set; }
    public string ScheduleTrainingInitialEFinal { get; private set; }

    public StudentRequest(string name, string city, DateTime birthDate, string companyCnpj, Guid functionId, Guid classId, int practiceHours, int theoreticalHours, string model, string reasonForTermination, int contractPeriod, bool status, string phoneNumber, Guid poloId, DateTime endDate, DateTime admissionDate, int firstDayOfTrainingIntroduction, int finalDayTrainingIntroduction, int firstDayOfWeeklyTraining, string dayOfTrainingWeek, string scheduleTrainingInitialEFinal)
    {
        Name = name;
        City = city;
        BirthDate = birthDate;
        CompanyCnpj = companyCnpj;
        FunctionId = functionId;
        ClassId = classId;
        PracticeHours = practiceHours;
        TheoreticalHours = theoreticalHours;
        Model = Enum.Parse<EModel>(model, true);
        ReasonForTermination = Enum.Parse<EReasonForTermination>(reasonForTermination, true);
        ContractPeriod = contractPeriod;
        Status = status;
        PhoneNumber = phoneNumber;
        PoloId = poloId;
        EndDate = endDate;
        AdmissionDate = admissionDate;
        FirstDayOfTrainingIntroduction = firstDayOfTrainingIntroduction;
        FinalDayTrainingIntroduction = finalDayTrainingIntroduction;
        FirstDayOfWeeklyTraining = firstDayOfWeeklyTraining;
        DayOfTrainingWeek = Enum.Parse<EWeekDay>(dayOfTrainingWeek, true);
        ScheduleTrainingInitialEFinal = scheduleTrainingInitialEFinal;
    }
}