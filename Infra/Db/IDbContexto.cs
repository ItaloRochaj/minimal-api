using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Db;

public interface IDbContexto
{
    DbSet<Administrador> Administradores { get; set; }
    DbSet<Veiculo> Veiculos { get; set; }
    int SaveChanges();
}