using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Interfaces;

public interface IAssessmentRepository
{
    Task<IEnumerable<AssessmentResponse>> GetAssessmentsAsync();
    Task<bool> AddAsync(InsertAssessmentRequest request);
    Task<bool> DeleteAsync(string id);
}