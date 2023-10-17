using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<StudentResponse>> GetStudentsAsync();
    Task<bool> AddAsync(StudentRequest request);
    Task<bool> UpdateAsync(StudentRequest request);
    Task<bool> DeleteAsync(string cpf);
}