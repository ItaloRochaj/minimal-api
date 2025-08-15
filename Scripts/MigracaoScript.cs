using Microsoft.EntityFrameworkCore;
using MinimalApi.Infraestrutura.Db;

namespace MinimalApi.Scripts;

public static class MigracaoScript
{
    public static void ExecutarMigracoes(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DbContexto>();

        try
        {
            context.Database.Migrate();
            Console.WriteLine("Migrações aplicadas com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao aplicar migrações: {ex.Message}");
        }
    }

    public static void CriarBancoDados(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DbContexto>();

        try
        {
            context.Database.EnsureCreated();
            Console.WriteLine("Banco de dados criado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar banco de dados: {ex.Message}");
        }
    }
}
