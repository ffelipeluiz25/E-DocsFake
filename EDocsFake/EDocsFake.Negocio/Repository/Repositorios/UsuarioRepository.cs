using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using EDocsFake.Infra.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
namespace EDocsFake.Infra.Repository.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        
        #region Atributos

        string _connectinStringOracle = string.Empty;

        #endregion

        #region Construtor

        /// <summary>
        /// UsuarioRepository
        /// </summary>
        /// <param name="configuration"></param>
        public UsuarioRepository(IConfiguration configuration)

        {
            //_connectinStringSQL = configuration.GetConnectionString("SegisSQLServer");
            _connectinStringOracle = configuration.GetConnectionString("SegisOracle");
        }

        #endregion

        #region Metodos Usuario API

        /// <summary>
        /// Autenticação do usuário
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public Data.Entidades.Usuario AutenticarUsuario(string login, string senha)
        {
            using var connection = new OracleConnection(_connectinStringOracle);
            var query = "SELECT TU.CO_USUARIO as CodigoUsuario, TU.NO_USUARIO as NomeUsuario FROM TB_USUARIO TU WHERE TU.LO_USUARIO = :login AND TU.SE_USUARIO = :senha";
            var usuario = connection.Query<Data.Entidades.Usuario>(query, new { login = login, senha = senha }).FirstOrDefault();
            return usuario;
        }

        /// <summary>
        /// Lista Todos os Usuários
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Data.Entidades.Usuario> ListarTodosUsuarios()
        {
            using var connection = new OracleConnection(_connectinStringOracle);
            var query = "SELECT " +
                "TU.CO_USUARIO as CodigoUsuario, " +
                "TU.NO_USUARIO as NomeUsuario, " +
                "TU.LO_USUARIO as Login, " +
                "TU.SE_USUARIO as Senha " +
                "FROM TB_USUARIO TU";
            var result = connection.Query<Data.Entidades.Usuario>(query);
            return result;
        }

        #endregion

    }
}