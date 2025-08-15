using MinimalApi.DTOs;
using MinimalApi.Dominio.ModelViews;

namespace MinimalApi.Dominio.Validacoes;

public static class RegistroUsuarioValidacao
{
    public static ErrosDeValidacao ValidarRegistroUsuario(RegistroUsuarioDTO usuarioDTO)
    {
        var validacao = new ErrosDeValidacao
        {
            Mensagens = new List<string>()
        };

        if (string.IsNullOrEmpty(usuarioDTO.Nome))
            validacao.Mensagens.Add("Nome não pode ser vazio");

        if (string.IsNullOrEmpty(usuarioDTO.Email))
            validacao.Mensagens.Add("Email não pode ser vazio");

        if (string.IsNullOrEmpty(usuarioDTO.Senha))
            validacao.Mensagens.Add("Senha não pode ser vazia");

        if (usuarioDTO.Senha?.Length < 6)
            validacao.Mensagens.Add("Senha deve ter pelo menos 6 caracteres");

        if (!usuarioDTO.Email?.Contains("@") == true)
            validacao.Mensagens.Add("Email deve ter um formato válido");

        return validacao;
    }
}
