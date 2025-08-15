using FluentAssertions;
using MinimalApi.Dominio.Entidades;
using Xunit;

namespace MinimalApi.UnitTests.Models;

public class VeiculoTests
{
    [Fact]
    public void Veiculo_DeveCriarInstanciaValida_QuandoDadosCorretos()
    {
        // Arrange
        var nome = "Civic";
        var marca = "Honda";
        var ano = 2023;

        // Act
        var veiculo = new Veiculo
        {
            Nome = nome,
            Marca = marca,
            Ano = ano
        };

        // Assert
        veiculo.Nome.Should().Be(nome);
        veiculo.Marca.Should().Be(marca);
        veiculo.Ano.Should().Be(ano);
        veiculo.Id.Should().Be(0); // Novo objeto ainda não persistido
    }

    [Theory]
    [InlineData(1950)] // Ano mínimo válido
    [InlineData(2024)] // Ano atual
    [InlineData(2025)] // Ano futuro
    public void Veiculo_DevePermitirAnosValidos(int ano)
    {
        // Arrange & Act
        var veiculo = new Veiculo
        {
            Nome = "Teste",
            Marca = "Teste",
            Ano = ano
        };

        // Assert
        veiculo.Ano.Should().Be(ano);
    }

    [Fact]
    public void Veiculo_DevePermitirAlteracaoPropriedades()
    {
        // Arrange
        var veiculo = new Veiculo
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2020
        };

        // Act
        veiculo.Nome = "Corolla";
        veiculo.Marca = "Toyota";
        veiculo.Ano = 2023;

        // Assert
        veiculo.Nome.Should().Be("Corolla");
        veiculo.Marca.Should().Be("Toyota");
        veiculo.Ano.Should().Be(2023);
    }
}
