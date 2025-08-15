using Microsoft.EntityFrameworkCore;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Dominio.Servicos;

public class MigracaoServico
{
    private readonly DbContexto _context;

    public MigracaoServico(DbContexto context)
    {
        _context = context;
    }

    public void AplicarMigracoes()
    {
        _context.Database.Migrate();
    }

    public bool DatabaseExists()
    {
        return _context.Database.CanConnect();
    }
}
