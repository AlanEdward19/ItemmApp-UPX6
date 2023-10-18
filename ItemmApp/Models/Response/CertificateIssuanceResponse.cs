namespace ItemmApp.Models.Response;

public class CertificateIssuanceResponse
{
    public string StudentCpf { get; set; }
    public string CompanyName { get; set; }
    public string StudentFunction { get; set; }
    public DateTime InitialDate { get; set; }
    public DateTime FinalDate { get; set; }
    public bool IsQualified { get; set; }
}