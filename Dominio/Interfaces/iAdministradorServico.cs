using Minimal.DTOs;
using minimal_api.Dominio.Entidades;
using System.Collections.Generic;

namespace minimal_api.Dominio.Interfaces
{
    public interface IAdministradorServico
    {
        Administrador? Login(LoginDTO loginDTO);
        Administrador Incluir(Administrador administrador);
        Administrador? BuscaPorId(int id);
        List<Administrador> Todos(int? pagina = 1);
    }
}