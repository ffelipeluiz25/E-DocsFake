using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EDocsFake.Extensions;
using EDocsFake.Request;
using EDocsFake.Util;
using EDocsFake.Infra.Repository.Interfaces;
using System;
namespace EDocsFake.Api.Controllers
{
    
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        #region Atributos

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _config;
        private readonly string _secret;

        #endregion

        #region Contrutor

        /// <summary>
        /// UsuarioController
        /// </summary>
        /// <param name="usuarioRepository"></param>
        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _config = config;

            var appSettingsSection = _config.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            _secret = appSettings.Secret.ToString();
        }

        #endregion

        #region Actions

        /// <summary>
        /// ListarTodosUsuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("usuarios")]
        [Authorize]
        public IActionResult ListarTodosUsuarios()
        {
            try
            {
                var data = _usuarioRepository.ListarTodosUsuarios();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// AutenticarUsuario
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("autentica-usuario")]
        public ActionResult<dynamic> AutenticarUsuario(UsuarioAutenticaRequest usuarioAutentica)
        {
            try
            {
                var usuario = _usuarioRepository.AutenticarUsuario(usuarioAutentica.Login, usuarioAutentica.Senha);
                if (usuario == null)
                    return NotFound(new { mensagem = "Usuario ou senha inválidos" });

                var token = TokenService.GerarToken(usuario, _secret);

                return new Response.UsuarioAutenticaResponse()
                {
                    CodUsuario = usuario.CodigoUsuario,
                    NomeUsuario = usuario.NomeUsuario,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        #endregion

    }
}