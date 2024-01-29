using apiServicio.Models;

namespace apiServicio.Business.Contracts
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetNombreUsuario(string nombreusuario);
    }
}
