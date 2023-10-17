using ItemmApp.Enums;

namespace ItemmApp.Models.Request;

public class InsertStudentRequest : StudentRequest
{
    public string Cpf { get; private set; }

    public InsertStudentRequest(string name, string city, DateTime birthDate, string companyCnpj, Guid functionId, Guid classId, int practiceHours, int theoreticalHours, string model, string reasonForTermination, int contractPeriod, bool status, string phoneNumber, Guid poloId, DateTime endDate, DateTime admissionDate, int firstDayOfTrainingIntroduction, int finalDayTrainingIntroduction, int firstDayOfWeeklyTraining, string dayOfTrainingWeek, string scheduleTrainingInitialEFinal, string cpf) : base(name, city, birthDate, companyCnpj, functionId, classId, practiceHours, theoreticalHours, model, reasonForTermination, contractPeriod, status, phoneNumber, poloId, endDate, admissionDate, firstDayOfTrainingIntroduction, finalDayTrainingIntroduction, firstDayOfWeeklyTraining, dayOfTrainingWeek, scheduleTrainingInitialEFinal)
    {
        Cpf = cpf;
    }
}