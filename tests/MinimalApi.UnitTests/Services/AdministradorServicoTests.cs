using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalApi.DTOs;
using MinimalApi.Dominio.Entidades;
using MinimalApi.Infraestrutura.Db;
using Xunit;

namespace MinimalApi.UnitTests.Services;

public class AdministradorServicoTests : IDisposable
{
    private readonly DbContexto _context;

    public AdministradorServicoTests()
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
    public async Task DeveAdicionarAdministrador_QuandoDadosValidos()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "novo@admin.com",
            Senha = "123456",
            Perfil = "Editor"
        };

        // Act
        _context.Administradores.Add(administrador);
        var resultado = await _context.SaveChangesAsync();

        // Assert
        resultado.Should().Be(1);
        administrador.Id.Should().BeGreaterThan(0);

        var administradorBanco = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Email == "novo@admin.com");
        administradorBanco.Should().NotBeNull();
        administradorBanco.Email.Should().Be(administrador.Email);
        administradorBanco.Perfil.Should().Be(administrador.Perfil);
    }

    [Fact]
    public async Task DeveBuscarAdministrador_QuandoExiste()
    {
        // Arrange
        var administrador = new Administrador
        {
            Email = "teste@admin.com",
            Senha = "123456",
            Perfil = "Adm"
        };
        
        _context.Administradores.Add(administrador);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Id == administrador.Id);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Id.Should().Be(administrador.Id);
        resultado.Email.Should().Be(administrador.Email);
    }

    [Fact]
    public async Task DeveBuscarAdministradorPorEmail_QuandoCredenciaisCorretas()
    {
        // Arrange
        var email = "login@teste.com";
        var senha = "123456";

        var administrador = new Administrador
        {
            Email = email,
            Senha = senha,
            Perfil = "Adm"
        };

        _context.Administradores.Add(administrador);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _context.Administradores
            .FirstOrDefaultAsync(a => a.Email == email && a.Senha == senha);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Email.Should().Be(email);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
