using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Interfaces;

public interface IFunctionRepository
{
    Task<IEnumerable<FunctionResponse>> GetFunctionsAsync();
}