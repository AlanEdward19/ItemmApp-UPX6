using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Interfaces;

public interface ICompanyRepository
{
    Task<IEnumerable<CompanyResponse>> GetCompaniesAsync();
}