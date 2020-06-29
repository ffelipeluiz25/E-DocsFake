using System.Collections.Generic;
namespace EDocsFake.Business.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        #region Assinatura dos Médotos

        Entidades.Usuario AutenticarUsuario(string login, string senha);
        IEnumerable<Entidades.Usuario> ListarTodosUsuarios();

        #endregion

    }
}