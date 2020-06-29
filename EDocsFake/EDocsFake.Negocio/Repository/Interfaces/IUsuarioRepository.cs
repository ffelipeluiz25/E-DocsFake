using System.Collections.Generic;
namespace EDocsFake.Infra.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        #region Assinatura dos Médotos

        public Data.Entidades.Usuario AutenticarUsuario(string login, string senha);
        public IEnumerable<Data.Entidades.Usuario> ListarTodosUsuarios();

        #endregion

    }
}