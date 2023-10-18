using ItemmApp.Models.Response;

namespace ItemmApp.Interfaces;

public interface ICertificateIssuanceRepository
{
    Task<CertificateIssuanceResponse> GetCertificateIssuanceAsync(string cpf);
}