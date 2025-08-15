namespace MinimalApi.DTOs;

public class AlterarSenhaDTO
{
    public string Email { get; set; } = default!;
    public string SenhaAtual { get; set; } = default!;
    public string NovaSenha { get; set; } = default!;
}
