using MinimalApi.Dominio.Enuns;

namespace MinimalApi.DTOs;

public class RegistroUsuarioDTO
{
    public string Nome { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
    public Perfil Perfil { get; set; } = default!;
}
