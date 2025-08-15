using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Db;

public class DbContexto : DbContext
{
    private readonly IConfiguration _configuracaoAppSettings;

    public DbContexto(IConfiguration configuracaoAppSettings)
    {
        _configuracaoAppSettings = configuracaoAppSettings;
    }

    public DbContexto(IConfiguration configuracaoAppSettings, DbContextOptions<DbContexto> options) : base(options)
    {
        _configuracaoAppSettings = configuracaoAppSettings;
    }

    public DbSet<Administrador> Administradores { get; set; } = default!;
    public DbSet<Veiculo> Veiculos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>().HasData(
            new Administrador
            {
                Id = 1,
                Email = "administrador@teste.com",
                Senha = "123456",
                Perfil = "Adm"
            }
        );

        modelBuilder.Entity<Veiculo>().HasData(
            new Veiculo
            {
                Id = 1,
                Nome = "Civic",
                Marca = "Honda",
                Ano = 2022
            },
            new Veiculo
            {
                Id = 2,
                Nome = "Corolla",
                Marca = "Toyota",
                Ano = 2023
            },
            new Veiculo
            {
                Id = 3,
                Nome = "Onix",
                Marca = "Chevrolet",
                Ano = 2021
            },
            new Veiculo
            {
                Id = 4,
                Nome = "HB20",
                Marca = "Hyundai",
                Ano = 2022
            },
            new Veiculo
            {
                Id = 5,
                Nome = "Polo",
                Marca = "Volkswagen",
                Ano = 2023
            }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var stringConexao = _configuracaoAppSettings.GetConnectionString("MySql")?.ToString();
            if (!string.IsNullOrEmpty(stringConexao))
            {
                optionsBuilder.UseMySql(
                    stringConexao,
                    ServerVersion.AutoDetect(stringConexao)
                );
            }
        }
    }
}