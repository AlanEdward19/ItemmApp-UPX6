using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Interfaces;

public interface ILoginRepository
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
}