using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.ModelViews;
using System.Collections.Generic;
using System.Linq;

namespace minimal_api.Dominio.Extensions
{
    public static class AdministradorExtensions
    {
        public static AdministradorModelView ToModelView(this Administrador administrador)
        {
            return new AdministradorModelView
            {
                Id = administrador.Id,
                Email = administrador.Email,
                Perfil = administrador.Perfil
            };
        }

        public static List<AdministradorModelView> ToModelView(this List<Administrador> administradores)
        {
            return administradores.Select(a => a.ToModelView()).ToList();
        }
    }
}
