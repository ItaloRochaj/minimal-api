using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.Db;
using Xunit;

namespace MinimalApi.UnitTests.Database;

public class DbContextoTests : IDisposable
{
    private readonly DbContexto _context;

    public DbContextoTests()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"ConnectionStrings:MySql", "InMemoryConnection"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DbContexto(configuration, options);
    }

    [Fact]
    public async Task DbContexto_DeveSalvarAdministrador_QuandoDadosValidos()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "persistencia@teste.com",
            Senha = "123456",
            Perfil = "Adm"
        };

        // Act
        _context.Administradores.Add(administrador);
        var resultado = await _context.SaveChangesAsync();

        // Assert
        resultado.Should().Be(1); // 1 registro salvo
        administrador.Id.Should().BeGreaterThan(0);

        var administradorSalvo = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Email == "persistencia@teste.com");
        administradorSalvo.Should().NotBeNull();
        administradorSalvo.Email.Should().Be(administrador.Email);
    }

    [Fact]
    public async Task DbContexto_DeveSalvarVeiculo_QuandoDadosValidos()
    {
        // Arrange
        var veiculo = new Veiculo
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2023
        };

        // Act
        _context.Veiculos.Add(veiculo);
        var resultado = await _context.SaveChangesAsync();

        // Assert
        resultado.Should().Be(1);
        veiculo.Id.Should().BeGreaterThan(0);

        var veiculoSalvo = await _context.Veiculos
            .FirstOrDefaultAsync(v => v.Nome == "Civic");
        veiculoSalvo.Should().NotBeNull();
        veiculoSalvo.Marca.Should().Be("Honda");
        veiculoSalvo.Ano.Should().Be(2023);
    }

    [Fact]
    public async Task DbContexto_DeveAtualizarAdministrador_QuandoModificado()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "atualizar@teste.com",
            Senha = "123456",
            Perfil = "Editor"
        };

        _context.Administradores.Add(administrador);
        await _context.SaveChangesAsync();

        // Act
        administrador.Perfil = "Adm";
        administrador.Senha = "novaSenha";
        var resultado = await _context.SaveChangesAsync();

        // Assert
        resultado.Should().Be(1);

        var administradorAtualizado = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Id == administrador.Id);
        administradorAtualizado.Perfil.Should().Be("Adm");
        administradorAtualizado.Senha.Should().Be("novaSenha");
    }

    [Fact]
    public async Task DbContexto_DeveRemoverAdministrador_QuandoExcluido()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "remover@teste.com",
            Senha = "123456",
            Perfil = "Editor"
        };

        _context.Administradores.Add(administrador);
        await _context.SaveChangesAsync();

        // Act
        _context.Administradores.Remove(administrador);
        var resultado = await _context.SaveChangesAsync();

        // Assert
        resultado.Should().Be(1);

        var administradorRemovido = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Id == administrador.Id);
        administradorRemovido.Should().BeNull();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
