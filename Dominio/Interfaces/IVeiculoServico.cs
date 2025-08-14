using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Minimal.DTOs;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Dominio.Interfaces
{
    public interface IVeiculoServico
    {
        List<Veiculo>? Todos(int pagina = 1, string? nome = null, string? marca = null);
        Veiculo BuscaPorId(int id);

        Veiculo Incluir(Veiculo veiculo);

        Veiculo Atualizar(Veiculo veiculo);

        void Apagar(Veiculo veiculo);

    }
}