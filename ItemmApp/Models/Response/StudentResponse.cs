namespace ItemmApp.Models.Response;

public class StudentResponse
{
    public string Cpf { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public DateTime BirthDate { get; set; }
    public string CompanyId { get; set; }
    public string CompanyName { get; set; }
    public Guid FunctionId { get; set; }
    public string FunctionName { get; set; }
    public Guid ClassId { get; set; }
    public string ClassName { get; set; }
    public Guid PoloId { get; set; }
    public string PoloName { get; set; }
    public int PracticeHours { get; set; }
    public int TheoreticalHours { get; set; }
    public string Model { get; set; }
    public string ReasonForTermination { get; set; }
    public int ContractPeriod { get; set; }
    public string Status { get; set; } //Ativo ou Inativo
    public string PhoneNumber { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime AdmissionDate { get; set; }
    public int FirstDayOfTrainingIntroduction { get; set; }
    public int FinalDayTrainingIntroduction { get; set; }
    public int FirstDayOfWeeklyTraining { get; set; }
    public string DayOfTrainingWeek { get; set; }
    public string ScheduleTrainingInitialEFinal { get; set; }
}