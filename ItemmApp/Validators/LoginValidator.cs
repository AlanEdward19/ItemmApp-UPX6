using Flunt.Validations;
using ItemmApp.Models.Request;

namespace ItemmApp.Validators;

public class LoginValidator : Contract<LoginRequest>
{
    public LoginValidator(LoginRequest request)
    {
        Requires()
            .IsNotNullOrEmpty(request.Email, "Email", "E-mail não pode ser vazio")
            .IsEmail(request.Email, "Email", "E-mail inválido")
            .IsNotNullOrWhiteSpace(request.Password, "Senha", "Senha não pode ser vazia");
    }
}