using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<StudentResponse>> GetStudentsAsync();
    Task<bool> AddAsync(InsertStudentRequest request);
    Task<bool> UpdateAsync(StudentRequest request, string cpf);
    Task<bool> DeleteAsync(string cpf);
}