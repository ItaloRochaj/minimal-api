using MinimalApi.Dominio.Entidades;
using MinimalApi.DTOs;

namespace MinimalApi.Dominio.Servicos;

public class AuthService
{
    public static bool ValidarCredenciais(string email, string senha)
    {
        // Implementar validação de credenciais
        return !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(senha);
    }

    public static bool ValidarEmail(string email)
    {
        return !string.IsNullOrEmpty(email) && email.Contains("@");
    }
}
