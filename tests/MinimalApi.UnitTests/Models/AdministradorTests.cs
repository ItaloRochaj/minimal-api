using FluentAssertions;
using MinimalApi.Dominio.Entidades;
using Xunit;

namespace MinimalApi.UnitTests.Models;

public class AdministradorTests
{
    [Fact]
    public void Administrador_DeveCriarInstanciaValida_QuandoDadosCorretos()
    {
        // Arrange
        var email = "admin@teste.com";
        var senha = "123456";
        var perfil = "Adm";

        // Act
        var administrador = new Administrador
        {
            Email = email,
            Senha = senha,
            Perfil = perfil
        };

        // Assert
        administrador.Email.Should().Be(email);
        administrador.Senha.Should().Be(senha);
        administrador.Perfil.Should().Be(perfil);
        administrador.Id.Should().Be(0); // Novo objeto ainda não persistido
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Administrador_DevePermitirEmailVazio_ParaValidacaoNaAPI(string emailInvalido)
    {
        // Arrange & Act
        var administrador = new Administrador
        {
            Email = emailInvalido,
            Senha = "123456",
            Perfil = "Editor"
        };

        // Assert - A validação acontece na API, não no modelo
        administrador.Email.Should().Be(emailInvalido);
    }

    [Fact]
    public void Administrador_DevePermitirAlteracaoPerfil()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "admin@teste.com",
            Senha = "123456",
            Perfil = "Editor"
        };

        // Act
        administrador.Perfil = "Adm";

        // Assert
        administrador.Perfil.Should().Be("Adm");
    }

    [Fact]
    public void Administrador_DevePermitirAlteracaoSenha()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "admin@teste.com",
            Senha = "senhaAntiga",
            Perfil = "Adm"
        };

        var novaSenha = "novaSenha123";

        // Act
        administrador.Senha = novaSenha;

        // Assert
        administrador.Senha.Should().Be(novaSenha);
    }
}
